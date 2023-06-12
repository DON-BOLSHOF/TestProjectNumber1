using System;

namespace Utils
{
    public class ActionDisposable : IDisposable
    {
        private Action _onDispose;

        public ActionDisposable(Action call)
        {
            _onDispose = call;
        }
        
        public void Dispose()
        {
            _onDispose?.Invoke();
            _onDispose = null;
        }
    }
}