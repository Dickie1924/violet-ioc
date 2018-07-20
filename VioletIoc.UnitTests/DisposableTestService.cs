using System;

namespace VioletIoc.UnitTests
{
    class DisposableTestService : TestService, IDisposable
    {
        public bool Disposed { get; private set; }

        public void Dispose()
        {
            Disposed = true;
        }
    }
}
