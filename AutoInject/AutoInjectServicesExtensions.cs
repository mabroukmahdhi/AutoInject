// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System;
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
                    services.RegisterSingleton(singletonAttr, type);
                    continue;
                }

                if (scopedAttr != null)
                {
                    services.RegisterScoped(scopedAttr, type);
                    continue;
                }

                if (transientAttr != null)
                {
                    services.RegisterTransient(transientAttr, type);
                    continue;
                }
            }

            return services;
        }

        private static void RegisterSingleton(
            this IServiceCollection services,
            SingletonAttribute singletonAttribute,
            Type implementationType)
        {
            if (singletonAttribute.ServiceType == null)
            {
                services.AddSingleton(implementationType);
                return;
            }

            if (string.IsNullOrEmpty(singletonAttribute.WithKey))
                services.AddSingleton(
                    singletonAttribute.ServiceType,
                    implementationType);
            else
                services.AddKeyedSingleton(
                    singletonAttribute.ServiceType,
                    singletonAttribute.WithKey, implementationType);
        }

        private static void RegisterTransient(
            this IServiceCollection services,
            TransientAttribute transientAttribute,
            Type implementationType)
        {
            if (transientAttribute.ServiceType == null)
            {
                services.AddTransient(implementationType);
                return;
            }

            if (string.IsNullOrEmpty(transientAttribute.WithKey))
                services.AddTransient(
                    transientAttribute.ServiceType,
                    implementationType);
            else
                services.AddKeyedTransient(
                    transientAttribute.ServiceType,
                    transientAttribute.WithKey, implementationType);
        }

        private static void RegisterScoped(
            this IServiceCollection services,
            ScopedAttribute scopedAttribute,
            Type implementationType)
        {
            if (scopedAttribute.ServiceType == null)
            {
                services.AddScoped(implementationType);
                return;
            }

            if (string.IsNullOrEmpty(scopedAttribute.WithKey))
                services.AddScoped(
                    scopedAttribute.ServiceType,
                    implementationType);
            else
                services.AddKeyedScoped(
                    scopedAttribute.ServiceType,
                    scopedAttribute.WithKey, implementationType);
        }
    }
}
