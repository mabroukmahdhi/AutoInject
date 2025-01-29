// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using AutoInject.Attributes.SingletonAttributes;

namespace AutoInject.Tests.Unit.Services
{
    public interface ITestSingletonService { }

    [Singleton(typeof(ITestSingletonService))]
    public class TestSingletonService : ITestSingletonService { }

    // Keyed Singleton Service
    [Singleton(serviceType: typeof(ITestService), withKey: "MyKey")]
    public class TestKeyedSingletonService : ITestService { }

}
