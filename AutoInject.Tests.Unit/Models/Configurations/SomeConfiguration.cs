// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using AutoInject.Attributes.SingletonAttributes;

namespace AutoInject.Tests.Unit.Models.Configurations
{
    [Singleton]
    public class SomeConfiguration
    {
        public string AppName { get; set; }
    }
}
