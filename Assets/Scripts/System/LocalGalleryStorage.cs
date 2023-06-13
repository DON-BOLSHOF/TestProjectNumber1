using Handlers.SystemHandler;
using UnityEngine;
using Utils;
using Zenject;
using IInitializable = Unity.VisualScripting.IInitializable;

namespace System
{
    public class LocalGalleryStorage : IInitializable, IDisposable
    {
        [Inject] private StorageHandler _storageHandler;
        [Inject] private DownloadHandler _downloadHandler;

        private Sprite[] _sprites = new Sprite[66];

        public void Initialize()
        {
            _downloadHandler.OnDownloadedImage += LoadedTexture;
            _storageHandler.OnReadTexture += LoadedTexture;
        }

        private void LoadedTexture(Texture2D texture, int index)
        {
            _sprites[index] = TextureConverter.ConvertTexture(texture);
        }

        public Sprite GetSprite(int index)
        {
            return _sprites[index];
        }

        public void Dispose()
        {
            _downloadHandler.OnDownloadedImage -= LoadedTexture;
            _storageHandler.OnReadTexture -= LoadedTexture;
        }
    }
}