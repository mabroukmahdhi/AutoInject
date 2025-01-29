// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using AutoInject.Attributes.ScopedAttributes;

namespace AutoInject.Tests.Unit.Services
{
    public interface ITestScopedService { }

    [Scoped(typeof(ITestScopedService))]
    public class TestScopedService : ITestScopedService { }

}
