using System;
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

        public async Task<Sprite> LoadSprite(int index)
        {
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
    }
}