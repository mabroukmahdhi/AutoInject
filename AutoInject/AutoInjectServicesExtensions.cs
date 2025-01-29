// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System.Linq;
using System.Reflection;
using AutoInject.Attributes.ScopedAttributes;
using AutoInject.Attributes.SingletonAttributes;
using AutoInject.Attributes.TransientAttributes;
using Microsoft.Extensions.DependencyInjection;

namespace AutoInject
{
    public static class AutoInjectServicesExtensions
    {
        /// <summary>
        /// Add services with AutoInject attributes to the service collection
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>The service collection</returns>
        public static IServiceCollection UseAutoInjection(this IServiceCollection services) =>
            UseAutoInjection(services, Assembly.GetCallingAssembly());

        /// <summary>
        /// Add services with AutoInject attributes to the service collection
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="assembly">The prefered services assembly</param>
        /// <returns>The service collection</returns>
        public static IServiceCollection UseAutoInjection(this IServiceCollection services, Assembly assembly)
        {
            var typesWithAttributes = assembly.GetTypes()
                .Where(type => type.GetCustomAttributes().Any(attr =>
                    attr is SingletonAttribute ||
                    attr is ScopedAttribute ||
                    attr is TransientAttribute));

            foreach (var type in typesWithAttributes)
            {
                var singletonAttr = type.GetCustomAttribute<SingletonAttribute>();
                var scopedAttr = type.GetCustomAttribute<ScopedAttribute>();
                var transientAttr = type.GetCustomAttribute<TransientAttribute>();

                if (singletonAttr != null)
                {
                    if (string.IsNullOrEmpty(singletonAttr.WithKey))
                        services.AddSingleton(singletonAttr.ServiceType, type);
                    else
                        services.AddKeyedSingleton(singletonAttr.ServiceType, singletonAttr.WithKey, type);

                    continue;
                }

                if (scopedAttr != null)
                {
                    if (string.IsNullOrEmpty(scopedAttr.WithKey))
                        services.AddScoped(scopedAttr.ServiceType, type);
                    else
                        services.AddKeyedScoped(scopedAttr.ServiceType, scopedAttr.WithKey, type);

                    continue;
                }

                if (transientAttr != null)
                {
                    if (string.IsNullOrEmpty(transientAttr.WithKey))
                        services.AddTransient(transientAttr.ServiceType, type);
                    else
                        services.AddKeyedTransient(transientAttr.ServiceType, transientAttr.WithKey, type);
                    continue;
                }
            }

            return services;
        }
    }
}
