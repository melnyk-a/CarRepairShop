using System;

namespace CarRepairShop
{
    public abstract class DisposableObject : IDisposable
    {
        private bool isDisposed;

        ~DisposableObject()
        {
            TryDispose(disposing: false);
        }

        public void Dispose()
        {
            TryDispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(bool disposing);

        private void TryDispose(bool disposing)
        {
            if (!isDisposed)
            {
                Dispose(disposing);

                isDisposed = true;
            }
        }
    }
}