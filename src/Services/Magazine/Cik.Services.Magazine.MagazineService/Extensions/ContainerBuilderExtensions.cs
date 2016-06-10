using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Cik.Services.Magazine.MagazineService.CommandHandlers;

namespace Cik.Services.Magazine.MagazineService.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static Type MakeAGenericType(Type generic, Type message)
        {
            return generic.MakeGenericType(message);
        }

        public static void RegisterCommandHandlerWith(this ContainerBuilder builder, Type message)
        {
            var asm = Assembly.Load(new AssemblyName(message.GetTypeInfo().Assembly.FullName));
            var commandHandlerType = MakeAGenericType(typeof (IHandleCommand<>), message);

            foreach (var type in asm.ExportedTypes.Where(t => commandHandlerType.IsAssignableFrom(t)))
            {
                if (!type.GetTypeInfo().IsInterface)
                {
                    builder.RegisterType(type).As(commandHandlerType).InstancePerLifetimeScope();
                }
            }
        }
    }
}