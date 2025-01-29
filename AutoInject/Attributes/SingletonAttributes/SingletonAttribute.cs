// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
// ---------------------------------------------------------------

using System;

namespace AutoInject.Attributes.SingletonAttributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class SingletonAttribute : Attribute
    {
        public SingletonAttribute(Type serviceType, string withKey = null)
        {
            ServiceType = serviceType;
            WithKey = withKey;
        }

        public Type ServiceType { get; }
        public string WithKey { get; }
    }
}
