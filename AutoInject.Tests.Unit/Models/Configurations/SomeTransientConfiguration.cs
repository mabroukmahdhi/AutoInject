// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using AutoInject.Attributes.TransientAttributes;

namespace AutoInject.Tests.Unit.Models.Configurations
{
    [Transient]
    public class SomeTransientConfiguration
    {
        public string AppName { get; set; }
    }
}
