using Common.Constants;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection Service;
        private static readonly object _sInjectorLock = new object();

        /// <summary>
        /// AutoRegisterDI
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="services">services</param>
        /// <returns></returns>
        public static IServiceCollection AutoRegisterDI(this IServiceCollection services)
        {
            Service = services;
            var assemblyNames = DependencyContext.Default.GetRuntimeAssemblyNames(Environment.OSVersion.Platform.ToString());
            var assemblys = assemblyNames
                        .Select(Assembly.Load)
                        .SelectMany(a => a.ExportedTypes)
                        .Where(x => x.IsClass && x.Namespace != null
                           && (
                                (x.Namespace.StartsWith(KeyConstants.AssemblyServices, StringComparison.InvariantCultureIgnoreCase))
                                || (x.Namespace.StartsWith(KeyConstants.AssemblyAction, StringComparison.InvariantCultureIgnoreCase))
                            ));

            var dicTypeMapiing = new Dictionary<Type, HashSet<Type>>();

            foreach (var assemType in assemblys)
            {
                assemType.GetInterfaces()
                     .Select(i => i.IsGenericType ? i.GetGenericTypeDefinition() : i)
                     .Where(i => i.Name.Contains(assemType.Name)).ToList()
                     .ForEach(iOfType =>
                     {
                         if (!dicTypeMapiing.ContainsKey(iOfType))
                         {
                             dicTypeMapiing[iOfType] = new HashSet<Type>();
                         }

                         dicTypeMapiing[iOfType].Add(assemType);
                     });
            }

            foreach (var typeMap in dicTypeMapiing)
            {
                if (typeMap.Value.Count == 1)
                {
                    services.AddScoped(typeMap.Key, typeMap.Value.First());
                }
            }

            return services;
        }

        /// <summary>
        /// RegisterOneInterfaceDI
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="type">type</param>
        /// <returns></returns>
        public static IServiceCollection RegisterOneInterfaceDI(this IServiceCollection services, Type type)
        {
            Service = services;
            var assemblyNames = DependencyContext.Default.GetRuntimeAssemblyNames(Environment.OSVersion.Platform.ToString());
            var assembly = assemblyNames

                        .Select(Assembly.Load)
                        .SelectMany(a => a.ExportedTypes)
                        .FirstOrDefault(x => x.IsClass && x.Namespace != null
                                    && x.Namespace.StartsWith(type.Namespace, StringComparison.InvariantCultureIgnoreCase));

            if (assembly == null)
            {
                throw new Exception();
            }

            var dicTypeMapiing = new Dictionary<Type, HashSet<Type>>();

            assembly.GetInterfaces()
                 .Select(i => i.IsGenericType ? i.GetGenericTypeDefinition() : i)
                 .Where(i => i.Name.Contains(assembly.Name)).ToList()
                 .ForEach(iOfType =>
                 {
                     if (!dicTypeMapiing.ContainsKey(iOfType))
                     {
                         dicTypeMapiing[iOfType] = new HashSet<Type>();
                     }
                     dicTypeMapiing[iOfType].Add(assembly);
                 });

            foreach (var typeMap in dicTypeMapiing)
            {
                if (typeMap.Value.Count == 1)
                {
                    services.AddScoped(typeMap.Key, typeMap.Value.First());
                }
            }

            return services;
        }

        /// <summary>
        /// Configure
        /// </summary>
        public static void Configure() { }

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>T</returns>
        public static T Resolve<T>(params object[] args)
            where T : class
        {
            lock (_sInjectorLock)
            {
                T ret = default(T);
                Type type = typeof(T);

                try
                {
                    if (args != null && args.Length > 0)
                    {
                        var assembly = type.LoadAssembly(type.Namespace);
                        if (assembly == null)
                        {
                            throw new Exception($"Can't load assembly {type.Name}.");
                        }

                        ret = (T)assembly.CreateGenericInstance(assembly.GenericTypeArguments, args);
                    }
                    else
                    {
                        //ret = (T)Service.BuildServiceProvider().GetService(type);
                    }
                }
                catch (Exception ex)
                {
                   // throw new Exception($"Can't Resolve :{ExceptionUtilities.GetFullExceptionMessage(ex)}");
                }

                if (ret == null)
                {
                    throw new InvalidOperationException(string.Format("Type {0} not registered in service", type.Name));
                }

                return ret;
            }
        }

        /// <summary>
        /// LoadAssembly
        /// </summary>
        /// <param name="type">type</param>
        /// <returns>Type</returns>
        public static Type LoadAssembly(this Type type, string nameSpace)
        {
            var assemblyNames = DependencyContext.Default.GetRuntimeAssemblyNames(Environment.OSVersion.Platform.ToString());
            var assembly = assemblyNames
                        .Select(Assembly.Load)
                        .SelectMany(a => a.ExportedTypes)
                        .FirstOrDefault(x => x.IsClass && x.Namespace != null
                                    && x.Namespace.StartsWith(nameSpace, StringComparison.InvariantCultureIgnoreCase));
            return assembly;
        }
    }
}
