using System;
using Xunit;
using Xunit.Abstractions;

namespace VioletIoc.UnitTests
{
    public class FactoryTests
    {
        private readonly ITestOutputHelper _output;

        public FactoryTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void GenericFactories_Work()
        {
            var root = ContainerFactory.CreateRootContainer("root", _output.WriteLine);
            root.Register<string>(c => "Hello");

            Assert.Equal(root.Resolve<string>(), "Hello");
        }

        [Fact]
        public void UntypedFactories_Work()
        {
            var root = ContainerFactory.CreateRootContainer("root", _output.WriteLine);
            root.RegisterSingleton(typeof(string), c => (object)"Hello");

            Assert.Equal(root.Resolve<string>(), "Hello");
        }
    }
}
