// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using AutoInject.Attributes.TransientAttributes;

namespace AutoInject.Tests.Unit.Services
{
    public interface ITestTransientService { }

    [Transient(typeof(ITestTransientService))]
    public class TestTransientService : ITestTransientService { }

}
