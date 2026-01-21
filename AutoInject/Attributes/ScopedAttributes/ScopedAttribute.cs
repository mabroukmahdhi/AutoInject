// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System;

namespace AutoInject.Attributes.ScopedAttributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ScopedAttribute : Attribute
    {
        public ScopedAttribute(
            Type serviceType = null,
            string withKey = null)
        {
            ServiceType = serviceType;
            WithKey = withKey;
        }

        public Type ServiceType { get; }
        public string WithKey { get; }
    }
}
