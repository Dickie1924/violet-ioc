using Xunit;
using VioletIoc;

namespace VioletIoc.UnitTests
{
    public class ContainerTests
    {
        [Fact]
        public void Container_ResolvesToInstance()
        {
            var service = new TestService();
            var locator = ContainerFactory.CreateRootContainer();
            locator.RegisterSingleton<ITestService>(service);

            Assert.Equal(service, locator.Resolve<ITestService>());
        }

        [Fact]
        public void Container_ResolvesToMappedType()
        {
            var locator = ContainerFactory.CreateRootContainer();            
            locator.Register<ITestService, TestService>();

            Assert.Equal(locator.Resolve<ITestService>().GetType(), typeof(TestService));
        }

        [Fact]
        public void Container_DefersToParent()
        {
            var parent = ContainerFactory.CreateRootContainer();
            var child = parent.CreateChildContainer();
            parent.Register<ITestService, TestService>();

            Assert.Equal(child.Resolve<ITestService>().GetType(), typeof(TestService));
        }

        [Fact]
        public void Container_MappedTypesCreateInstances()
        {
            var locator = ContainerFactory.CreateRootContainer(); 
            locator.Register<ITestService, TestService>();

            var ref1 = locator.Resolve<ITestService>();
            var ref2 = locator.Resolve<ITestService>();

            Assert.NotEqual(ref1, ref2);
        }

        [Fact]
        public void Container_ResolvesDependencies()
        {
            var locator = ContainerFactory.CreateRootContainer();  
            locator.Register<ITestService, TestService>();

            Assert.Equal(locator.Resolve<TestOtherService>().dependantService.GetType(), typeof(TestService));
        }
    }
}
