// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System;

namespace AutoInject.Attributes.TransientAttributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class TransientAttribute : Attribute
    {
        public TransientAttribute(
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
