// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using AutoInject.Tests.Unit.Models.Configurations;
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

        [Fact]
        public void ShouldRegisterScopedService()
        {
            // given
            var assembly = typeof(TestSingletonService).Assembly;

            // when
            this.serviceCollection.UseAutoInjection(assembly);
            var provider = this.serviceCollection.BuildServiceProvider();

            // then
            using var scope1 = provider.CreateScope();
            using var scope2 = provider.CreateScope();

            var instance1 = scope1.ServiceProvider.GetService<ITestScopedService>();
            var instance2 = scope2.ServiceProvider.GetService<ITestScopedService>();

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.NotSame(instance1, instance2);
        }

        [Fact]
        public void ShouldRegisterTransientService()
        {
            // given
            var assembly = typeof(TestTransientService).Assembly;

            // when
            this.serviceCollection.UseAutoInjection(assembly);
            var provider = this.serviceCollection.BuildServiceProvider();

            // then
            var instance1 = provider.GetService<ITestTransientService>();
            var instance2 = provider.GetService<ITestTransientService>();

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.NotSame(instance1, instance2);
        }

        [Fact]
        public void ShouldRegisterKeyedSingletonService()
        {
            // given
            var assembly = typeof(TestKeyedSingletonService).Assembly;

            // when
            this.serviceCollection.UseAutoInjection(assembly);
            var provider = this.serviceCollection.BuildServiceProvider();

            // then
            var instance1 = provider.GetRequiredKeyedService<ITestService>("MyKey");
            var instance2 = provider.GetRequiredKeyedService<ITestService>("MyKey");

            Assert.NotNull(instance1);
            Assert.Same(instance1, instance2);
        }

        [Fact]
        public void ShouldNotRegisterUnmarkedTypes()
        {
            // given
            var assembly = typeof(UnmarkedService).Assembly;

            // when
            this.serviceCollection.UseAutoInjection(assembly);
            var provider = this.serviceCollection.BuildServiceProvider();

            // then
            var instance = provider.GetService<UnmarkedService>();
            Assert.Null(instance);
        }

        [Fact]
        public void ShouldRegisterSingletonInstanceWhenNoServiceTypeProvided()
        {
            // given
            var assembly = typeof(SomeConfiguration).Assembly;

            // when
            this.serviceCollection.UseAutoInjection(assembly);

            ServiceProvider serviceProvider =
                this.serviceCollection.BuildServiceProvider();

            // then
            var instance1 =
                serviceProvider.GetService<SomeConfiguration>();

            var instance2 =
                serviceProvider.GetService<SomeConfiguration>();

            Assert.NotNull(instance1);
            Assert.Same(instance1, instance2);
        }

        [Fact]
        public void ShouldRegisterScopedInstanceWhenNoServiceTypeProvided()
        {
            // given
            var assembly = typeof(SomeScopedConfiguration).Assembly;

            // when
            this.serviceCollection.UseAutoInjection(assembly);
            var provider = this.serviceCollection.BuildServiceProvider();

            using var scope1 = provider.CreateScope();
            using var scope2 = provider.CreateScope();

            // then

            var instance1 =
                scope1.ServiceProvider.GetService<SomeScopedConfiguration>();

            var instance2 =
                scope2.ServiceProvider.GetService<SomeScopedConfiguration>();

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.NotSame(instance1, instance2);
        }
    }
}
