using System.Threading.Tasks;
using Handlers;
using Handlers.SystemHandler;
using UnityEngine;
using UnityEngine.Networking;

namespace Utils
{
    public sealed class WebRequester
    {
        private ErrorHandler _errorHandler;

        public WebRequester(ErrorHandler errorHandler)
        {
            _errorHandler = errorHandler;
        }

        public async Task<Texture2D> GetRemoteTexture (string url)
        {
            using( UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(url) )
            {
                var asyncOp = unityWebRequest.SendWebRequest();

                while( asyncOp.isDone==false )
                    await Task.Delay( 1000/30 );//30 hertz
        
                if( unityWebRequest.isNetworkError || unityWebRequest.isHttpError )
                {
                    _errorHandler.CatchWebError(unityWebRequest);
                    
                    return null;
                }
                else
                {
                    return DownloadHandlerTexture.GetContent(unityWebRequest);
                }
            }
        }
    }
}