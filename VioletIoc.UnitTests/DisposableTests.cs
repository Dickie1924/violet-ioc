using System;
using Xunit;
using Xunit.Abstractions;

namespace VioletIoc.UnitTests
{
    public class DisposableTests
    {
        private readonly ITestOutputHelper _output;

        public DisposableTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ChildContainer_OnlyDisposesOwnObjects()
        {
            var root = ContainerFactory.CreateRootContainer("root", _output.WriteLine);
            root.Register<ITestService, DisposableTestService>();

            DisposableTestService instance = null;

            using (var child = root.CreateChildContainer("child"))
            {
                instance = child.Resolve<ITestService>() as DisposableTestService;
            }

            Assert.False(instance.Disposed);
        }
    }
}
