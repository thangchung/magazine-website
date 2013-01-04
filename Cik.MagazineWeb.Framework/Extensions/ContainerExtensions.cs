namespace Cik.MagazineWeb.Framework.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Autofac;
    using Autofac.Core;

    public static class ContainerExtensions
    {
        public static void RegisterAssemblyModules(this ContainerBuilder builder, Assembly assembly)
        {
            var scanningBuilder = new ContainerBuilder();

            scanningBuilder.RegisterAssemblyTypes(assembly)
                .Where(t => typeof(IModule).IsAssignableFrom(t) && t.FullName.ToLower().Contains("btl"))
                .As<IModule>();

            using (var scanningContainer = scanningBuilder.Build())
            {
                foreach (var module in scanningContainer.Resolve<IEnumerable<IModule>>())
                    builder.RegisterModule(module);
            }
        }

        /// <summary>
        ///   http://stackoverflow.com/questions/4959148/is-it-possible-in-autofac-to-resolve-all-services-for-a-type-even-if-they-were
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="container"> </param>
        /// <returns> </returns>
        public static IEnumerable<T> ResolveAll<T>(this IContainer container)
        {
            var allKeys = new List<object>();
            foreach (var componentRegistration in container.ComponentRegistry.Registrations)
            {
                var typedServices = componentRegistration.Services.Where(x => x is KeyedService).Cast<KeyedService>();
                allKeys.AddRange(typedServices.Where(y => y.ServiceType == typeof(T)).Select(x => x.ServiceKey));
            }

            var allUnKeyedServices = new List<T>(container.Resolve<IEnumerable<T>>());
            allUnKeyedServices.AddRange(allKeys.Select(key => container.ResolveKeyed<T>(key)));
            
            return allUnKeyedServices;
        }

        public static void RegisterModules(this ContainerBuilder containerBuilder, IEnumerable<IModule> modules)
        {
            foreach (var module in modules)
            {
                containerBuilder.RegisterModule(module);
            }
        }
    }
}