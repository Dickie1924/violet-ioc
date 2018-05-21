using System;

namespace VioletIoc.UnitTests
{
    class DisposableTestService : ITestService, IDisposable
    {
        public bool Disposed { get; private set; }

        public void Dispose()
        {
            Disposed = true;
        }
    }
}
