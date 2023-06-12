using UnityEngine;
using UnityEngine.Networking;
using Utils;

namespace Handlers.SystemHandler
{
    public sealed class ErrorHandler
    {
        public readonly ReactiveEvent<string> OnCatchedSomething = new();
        
        public void CatchWebError(UnityWebRequest value)
        {
            if (value.isHttpError)
            {
                Debug.Log("Проблемы с сервером");
                OnCatchedSomething?.Execute("Проблемы с сервером");
            }

            if (value.isNetworkError)
            {
                Debug.Log("Проблемы со связью, проверьте подключение к интернету");
                OnCatchedSomething?.Execute("Проблемы со связью, проверьте подключение к интернету");
            }
        }
    }
}