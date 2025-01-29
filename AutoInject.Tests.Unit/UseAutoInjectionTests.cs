// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

namespace AutoInject.Tests.Unit
{
    public partial class UseAutoInjectionTests
    {
        private readonly IServiceCollection serviceCollection;

        public UseAutoInjectionTests() =>
            this.serviceCollection = new ServiceCollection();
    }
}
