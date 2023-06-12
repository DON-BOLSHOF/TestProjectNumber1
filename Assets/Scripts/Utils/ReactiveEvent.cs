using System;
using UniRx;

namespace Utils
{
    public sealed class ReactiveEvent<T>//Прослойка - нам не нужен весь функционал ReactiveCommand
    {
        private ReactiveCommand<T> _event = new();
        
        public void Execute(T data)
        {
            _event.Execute(data);
        }

        public IDisposable Subscribe(Action<T> action)
        {
            return _event.Subscribe(action);
        }
    }  
    
    public sealed class ReactiveEvent//Прослойка - нам не нужен весь функционал ReactiveCommand
    {
        private ReactiveCommand _event = new();

        public void Execute()
        {
            _event.Execute();
        }

        public IDisposable Subscribe(Action action)
        {
            return _event.Subscribe(_ => action());
        }
    }
}