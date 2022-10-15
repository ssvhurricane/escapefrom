using System;
using UniRx;

namespace Services.Disposable
{
    public class DisposableService : IDisposable
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        public void Clear()
        {
            _disposables.Clear();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        public void AddDisposable(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }
    }
}
