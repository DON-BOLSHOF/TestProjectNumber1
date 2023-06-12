using System;
using System.Collections.Generic;

namespace Utils
{
    public class DisposeHolder
    {
        private List<IDisposable> _disposables = new();

        public void Retain(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
            
            _disposables.Clear();
        }
    }
}