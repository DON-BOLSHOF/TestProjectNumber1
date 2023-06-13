using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;
using Zenject;

namespace Handlers.SystemHandler
{
    public sealed class DownloadHandler : IInitializable
    {
        [Inject] private ErrorHandler _errorHandler;
        
        private const string IMAGES_URL = "http://data.ikppbb.com/test-task-unity-data/pics/";

        private WebRequester _webRequester;

        public Action<Texture2D, int> OnDownloadedImage;

        public void Initialize()
        {
            _webRequester = new WebRequester(_errorHandler);
        }

        public async Task<Sprite> LoadImage(int index)
        {
            var texture2D =  await _webRequester.GetRemoteTexture(IMAGES_URL + (index+1) + ".jpg");//Некоторые интеллектуалы отсчет начинают с 1...

            if (texture2D == null)
            {
                Debug.Log("Неполадки с загрузкой");
                return null;
            }  
            
            OnDownloadedImage?.Invoke(texture2D, index);
            return TextureConverter.ConvertTexture(texture2D);
        }

        public async Task<List<Sprite>> LoadImages(int limit)
        {
            var result = new List<Sprite>();
            
            for (var i = 0; i < limit; i++)
            {
                var image = await LoadImage(i);

                if (image == null) return null;
                
                result.Add(image);
            }

            return result;
        }
    }
}