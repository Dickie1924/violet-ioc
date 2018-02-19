namespace VioletIoc.UnitTests
{
    class TestOtherService
    {
        public ITestService dependantService;

        public TestOtherService(ITestService dependantService)
        {
            this.dependantService = dependantService;
        }
    }
}
