using System.Threading.Tasks;
using Handlers.SystemHandler;
using UnityEngine;
using Zenject;

namespace Loaders
{
    public sealed class GalleryLoader
    {
        [Inject] private StorageHandler _storageHandler;
        [Inject] private DownloadHandler _downloadHandler;
        
        private Sprite[] _sprites = new Sprite[66];

        public async Task<Sprite> LoadSprite(int index)
        {
            if (_sprites[index] != null) return _sprites[index];

            var texture2D = _storageHandler.ReadTextureFromPlayerPrefs(index.ToString());
            if (texture2D != null)
            {
                _sprites[index] = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height),
                    new Vector2(0.5f, 0.5f), 100f);
                return _sprites[index];
            }

            return _sprites[index] = await _downloadHandler.LoadImage(index);
        }
    }
}