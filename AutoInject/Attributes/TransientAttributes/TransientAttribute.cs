// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
// ---------------------------------------------------------------

using System;

namespace AutoInject.Attributes.TransientAttributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class TransientAttribute : Attribute
    {
        public TransientAttribute(Type serviceType, string withKey = null)
        {
            ServiceType = serviceType;
            WithKey = withKey;
        }

        public Type ServiceType { get; }
        public string WithKey { get; }
    }
}
