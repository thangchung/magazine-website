using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Cik.CoreLibs.Domain;

namespace Cik.CoreLibs.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static Type MakeAGenericType(Type generic, Type message)
        {
            return generic.MakeGenericType(message);
        }

        public static void RegisterCommandHandlers(this ContainerBuilder builder)
        {
            var asm =
                Assembly.Load(new AssemblyName(typeof (ContainerBuilderExtensions).GetTypeInfo().Assembly.FullName));
            var messageType = typeof (Command);

            var types = asm.GetExportedTypes().Where(p => messageType.IsAssignableFrom(p));
            foreach (var type in types)
            {
                var commandHandlerType = MakeAGenericType(typeof (IHandleCommand<>), type);
                var implementedCommandHandlerType =
                    asm.GetExportedTypes().FirstOrDefault(
                        t => commandHandlerType.IsAssignableFrom(t));

                if (implementedCommandHandlerType == null) continue;
                if (!type.GetTypeInfo().IsInterface)
                {
                    builder.RegisterType(implementedCommandHandlerType).AsSelf();
                }
            }
        }

        public static void RegisterHandlers(this IContainer container, params Type[] typesFromAssemblyContainingMessages)
        {
            var bus = container.Resolve<IHandlerRegistrar>();
            foreach (var typesFromAssemblyContainingMessage in typesFromAssemblyContainingMessages)
            {
                var executorsAssembly = typesFromAssemblyContainingMessage.GetTypeInfo().Assembly;
                var executorTypes = executorsAssembly
                    .GetTypes()
                    .Select(t => new {Type = t, Interfaces = ResolveMessageHandlerInterface(t)})
                    .Where(e => e.Interfaces != null && e.Interfaces.Any());

                foreach (var executorType in executorTypes)
                {
                    foreach (var @interface in executorType.Interfaces)
                    {
                        InvokeHandler(container, @interface, bus, executorType.Type);
                    }
                }
            }
        }

        private static void InvokeHandler(IContainer container, Type @interface, IHandlerRegistrar bus,
            Type executorType)
        {
            var commandType = @interface.GetGenericArguments()[0];

            var registerExecutorMethod = bus
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(mi => mi.Name == "RegisterHandler")
                .Where(mi => mi.IsGenericMethod)
                .Where(mi => mi.GetGenericArguments().Count() == 1)
                .Single(mi => mi.GetParameters().Count() == 1)
                .MakeGenericMethod(commandType);

            var del = new Action<dynamic>(x =>
            {
                dynamic handler;
                container.TryResolve(executorType, out handler);
                handler.Handle(x);
            });

            registerExecutorMethod.Invoke(bus, new object[] {del});
        }

        private static IEnumerable<Type> ResolveMessageHandlerInterface(Type type)
        {
            foreach (var i in type.GetInterfaces())
            {
                if (i.GetTypeInfo().IsGenericType
                    && (i.GetGenericTypeDefinition() == typeof (IHandleCommand<>)))
                    yield return i;
            }
        }
    }
}