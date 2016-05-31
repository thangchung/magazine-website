using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cik.Infrastruture
{
    public class ExtensionManager
    {
        private static IEnumerable<Assembly> _assemblies;
        private static IEnumerable<IExtension> _extensions;

        public static IEnumerable<Assembly> Assemblies
        {
            get
            {
                if (_assemblies == null)
                    throw new InvalidOperationException("Assemblies not set");

                return _assemblies;
            }
        }

        public static IEnumerable<IExtension> Extensions
        {
            get
            {
                if (_extensions == null)
                    _extensions = GetInstances<IExtension>();

                return _extensions;
            }
        }

        public static void SetAssemblies(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        public static Type GetImplementation<T>()
        {
            return GetImplementation<T>(null);
        }

        public static Type GetImplementation<T>(Func<Assembly, bool> predicate)
        {
            var implementations = GetImplementations<T>(predicate);

            if (!implementations.Any())
                throw new ArgumentException("Implementation of " + typeof (T) + " not found");

            return implementations.FirstOrDefault();
        }

        public static IEnumerable<Type> GetImplementations<T>()
        {
            return GetImplementations<T>(null);
        }

        public static IEnumerable<Type> GetImplementations<T>(Func<Assembly, bool> predicate)
        {
            var implementations = new List<Type>();

            foreach (var assembly in GetAssemblies(predicate))
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (typeof (T).GetTypeInfo().IsAssignableFrom(type)
                        && type.GetTypeInfo().IsClass)
                        implementations.Add(type);
                }
            }

            return implementations;
        }

        public static T GetInstance<T>()
        {
            return GetInstance<T>(null);
        }

        public static T GetInstance<T>(Func<Assembly, bool> predicate)
        {
            var instances = GetInstances<T>(predicate);

            if (!instances.Any())
                throw new ArgumentException("Instance of " + typeof (T) + " can't be created");

            return instances.FirstOrDefault();
        }

        public static IEnumerable<T> GetInstances<T>()
        {
            return GetInstances<T>(null);
        }

        public static IEnumerable<T> GetInstances<T>(Func<Assembly, bool> predicate)
        {
            var instances = new List<T>();

            foreach (var implementation in GetImplementations<T>())
            {
                var instance = (T) Activator.CreateInstance(implementation);

                instances.Add(instance);
            }

            return instances;
        }

        private static IEnumerable<Assembly> GetAssemblies(Func<Assembly, bool> predicate)
        {
            return predicate == null ? Assemblies : Assemblies.Where(predicate);
        }
    }
}