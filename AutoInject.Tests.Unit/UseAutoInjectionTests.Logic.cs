// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using AutoInject.Tests.Unit.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AutoInject.Tests.Unit
{
    public partial class UseAutoInjectionTests
    {
        [Fact]
        public void ShouldRegisterSingletonService()
        {
            // given
            var assembly = typeof(TestSingletonService).Assembly;

            // when
            this.serviceCollection.UseAutoInjection(assembly);
            var provider = this.serviceCollection.BuildServiceProvider();

            // then
            var instance1 = provider.GetService<ITestSingletonService>();
            var instance2 = provider.GetService<ITestSingletonService>();

            Assert.NotNull(instance1);
            Assert.Same(instance1, instance2);
        }
    }
}
