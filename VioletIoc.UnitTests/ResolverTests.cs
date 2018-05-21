using Xunit;
using Xunit.Abstractions;

namespace VioletIoc.UnitTests
{
    public class ResolverTests
    {
        private readonly ITestOutputHelper _output;

        public ResolverTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Resolver_ResolvesToInstance()
        {
            var service = new TestService();
            var locator = ContainerFactory.CreateRootContainer("root", _output.WriteLine);
            locator.RegisterSingleton<ITestService>(service);

            var instance = locator.ResolverFor<ITestService>().Resolve();

            Assert.Equal(service, instance);
        }
    }
}
