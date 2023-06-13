using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Handlers.SystemHandler;
using UnityEngine;
using Zenject;

namespace Loaders
{
    public sealed class GalleryLoader
    {
        [Inject] private LocalGalleryStorage _localGalleryStorage;
        [Inject] private StorageHandler _storageHandler;
        [Inject] private DownloadHandler _downloadHandler;
        
        private int _maxConcurrentDownloads = 10;

        private async Task<Sprite> LoadSprite(int index)
        {
            if (index >= _localGalleryStorage.SpritesCount) return null;
            
            var sprite = _localGalleryStorage.GetSprite(index);
            if (sprite != null) return sprite;

            var texture2D = _storageHandler.ReadTextureFromPlayerPrefs(index);
            if (texture2D != null)
            {
                sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height),
                    new Vector2(0.5f, 0.5f), 100f);
                return sprite;
            }

            return await _downloadHandler.LoadImage(index);
        }

        public async Task<List<Sprite>> LoadSprites(int from, int to)
        {
            if (from >= _localGalleryStorage.SpritesCount) return null;
            if (to >= _localGalleryStorage.SpritesCount) to = _localGalleryStorage.SpritesCount;
            
            var result = new List<Sprite>();

            for (var i = from; i < to;)
            {
                var spriteBatch = new List<Task<Sprite>>();

                for (int j =0; j < _maxConcurrentDownloads && i < to; j++, i++)
                {
                    spriteBatch.Add( LoadSprite(i));
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