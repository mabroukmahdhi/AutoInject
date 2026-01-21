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
                var singletonAttrs = type.GetCustomAttributes<SingletonAttribute>().ToArray();
                var scopedAttrs = type.GetCustomAttributes<ScopedAttribute>().ToArray();
                var transientAttrs = type.GetCustomAttributes<TransientAttribute>().ToArray();

                // Validate and register singleton attributes
                ValidateUniqueServiceTypes(type, singletonAttrs, attr => attr.ServiceType, "Singleton");
                foreach (var attr in singletonAttrs)
                {
                    services.RegisterSingleton(attr, type);
                }

                // Validate and register scoped attributes
                ValidateUniqueServiceTypes(type, scopedAttrs, attr => attr.ServiceType, "Scoped");
                foreach (var attr in scopedAttrs)
                {
                    services.RegisterScoped(attr, type);
                }

                // Validate and register transient attributes
                ValidateUniqueServiceTypes(type, transientAttrs, attr => attr.ServiceType, "Transient");
                foreach (var attr in transientAttrs)
                {
                    services.RegisterTransient(attr, type);
                }
            }

            return services;
        }

        private static void ValidateUniqueServiceTypes<TAttribute>(
            Type implementationType,
            TAttribute[] attributes,
            Func<TAttribute, Type> getServiceType,
            string lifetimeName)
        {
            if (attributes.Length <= 1)
                return;

            var serviceTypes = new System.Collections.Generic.HashSet<Type>();

            foreach (var attr in attributes)
            {
                var serviceType = getServiceType(attr) ?? implementationType;

                if (!serviceTypes.Add(serviceType))
                {
                    throw new InvalidOperationException(
                        $"Type '{implementationType.FullName}' has multiple {lifetimeName} attributes with the same service type '{serviceType.FullName}'. " +
                        $"Multiple attributes of the same lifetime are only allowed with different service types.");
                }
            }
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
