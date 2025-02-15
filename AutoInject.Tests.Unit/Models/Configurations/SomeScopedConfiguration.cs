// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using AutoInject.Attributes.ScopedAttributes;

namespace AutoInject.Tests.Unit.Models.Configurations
{
    [Scoped]
    public class SomeScopedConfiguration
    {
        public string AppName { get; set; }
    }
}
