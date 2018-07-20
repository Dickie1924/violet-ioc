using System;
using Xunit;
using Xunit.Abstractions;

namespace VioletIoc.UnitTests
{
    public class LazyTests
    {
        private readonly ITestOutputHelper _output;

        public LazyTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Container_ResolvesLazyInstance()
        {
            var locator = ContainerFactory.CreateRootContainer("root", _output.WriteLine);
            locator.Register<ITestService, TestService>();

            var lazyService = locator.Resolve<Lazy<ITestService>>();

            _output.WriteLine("No test service resolved yet");

            Assert.Equal("Woof!", lazyService.Value.Bark());
        }

    }
}
