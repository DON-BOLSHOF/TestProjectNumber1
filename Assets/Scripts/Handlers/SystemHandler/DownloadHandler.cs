using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Utils;
using Zenject;

namespace Handlers.SystemHandler
{
    public sealed class DownloadHandler : IInitializable
    {
        [Inject] private ErrorHandler _errorHandler;
        private WebRequester _webRequester;
        
        private int _maxConcurrentDownloads = 4;

        public Action<Texture2D, int> OnDownloadedImage;

        private const string IMAGES_URL = "http://data.ikppbb.com/test-task-unity-data/pics/";
        public void Initialize()
        {
            _webRequester = new WebRequester(_errorHandler);
        }

        public async Task<Sprite> LoadImage(int index)
        {
            var texture2D =  await _webRequester.GetRemoteTexture(IMAGES_URL + (index+1) + ".jpg");

            if (texture2D == null)
            {
                Debug.Log("Неполадки с загрузкой");
                return null;
            }  
            
            OnDownloadedImage?.Invoke(texture2D, index);
            return TextureConverter.ConvertTexture(texture2D);
        }

        public async Task<List<Sprite>> LoadImages(int from, int to)
        {
            var result = new List<Sprite>();
            
            for (var i = from; i < to;)
            {
                var spriteBatch = new List<Task<Sprite>>();
                for (int j = 0; j < _maxConcurrentDownloads && i< to; j++,i++)
                {
                    spriteBatch.Add( LoadImage(i));
                }
                
                var loadedSprites =await Task.WhenAll(spriteBatch);

                if (loadedSprites.ToList().Where(t => t!= null).ToArray().Length != loadedSprites.Length) //Все хорошо подгрузили
                    return null;
                
                result.AddRange(loadedSprites);
            }

            return result;
        }
    }
}