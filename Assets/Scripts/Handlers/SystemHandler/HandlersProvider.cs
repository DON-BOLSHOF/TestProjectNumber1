using System;
using Zenject;
using IInitializable = Zenject.IInitializable;

namespace Handlers.SystemHandler
{
    public sealed class HandlersProvider : IInitializable, IDisposable
    {
        [Inject] private DownloadHandler _downloadHandler;
        [Inject] private ErrorHandler _errorHandler;
        [Inject] private StorageHandler _storageHandler;

        public void Initialize()
        {
            ProvideSubscribes();
        }

        private void ProvideSubscribes()
        {
            _downloadHandler.OnDownloadedImage += _storageHandler.SaveTexture;
        }

        public void Dispose()
        {
            _downloadHandler.OnDownloadedImage -= _storageHandler.SaveTexture;
        }
    }
}