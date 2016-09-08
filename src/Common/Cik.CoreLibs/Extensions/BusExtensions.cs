using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Cik.CoreLibs.Bus;
using Cik.CoreLibs.Bus.Amqp;
using Microsoft.Extensions.Configuration;

namespace Cik.CoreLibs.Extensions
{
    public static class BusExtensions
    {
        public static ContainerBuilder AddCqrsBus(
            this ContainerBuilder builder,
            Assembly registerAssembly,
            IConfiguration configuration,
            string commandExchange,
            string eventExchange,
            string commandQueue,
            string eventQueue)
        {
            var uri = configuration.GetValue<string>("rabbitmq");
            builder.Register(x => new RabbitMqPublisher(x.Resolve<IServiceProvider>(), uri, commandExchange))
                .As<ICommandBus>()
                .SingleInstance();

            builder.Register(x => new RabbitMqPublisher(x.Resolve<IServiceProvider>(), uri, eventExchange))
                .As<IEventBus>()
                .SingleInstance();

            builder.RegisterAssemblyTypes(registerAssembly)
                .AsImplementedInterfaces();

            builder.RegisterInstance(new RabbitMqSubscriber(uri, commandExchange, commandQueue))
                .Named<IMessageSubscriber>("CommandSubscriber");

            builder.RegisterInstance(new RabbitMqSubscriber(uri, eventExchange, eventQueue))
                .Named<IMessageSubscriber>("EventSubscriber");

            builder.Register(x =>
                new CommandConsumer(
                    x.ResolveNamed<IMessageSubscriber>("CommandSubscriber"),
                    (IEnumerable<ICommandHandler>) x.Resolve(typeof (IEnumerable<ICommandHandler>))
                    )
                ).As<ICommandConsumer>();

            builder.Register(x =>
                new EventConsumer(
                    x.ResolveNamed<IMessageSubscriber>("EventSubscriber"),
                    (IEnumerable<IEventHandler>) x.Resolve(typeof (IEnumerable<IEventHandler>))
                    )
                ).As<IEventConsumer>();
            return builder;
        }
    }
}