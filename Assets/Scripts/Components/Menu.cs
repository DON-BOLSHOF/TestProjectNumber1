using System;
using Handlers.SystemHandler;
using UI.Panels;
using UI.Widgets.MenuWidgets;
using UnityEngine;
using Utils;
using Zenject;

namespace Components
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private MenuWidget _menuWidget;
        [SerializeField] private LoadPanel _loadPanel;
        [SerializeField] private SceneLoaderComponent _sceneLoader;

        [Inject] private DownloadHandler _downloadHandler;

        private ReactiveEvent OnDownloadedImage = new();

        private const int LIMIT_FIRST_DOWNLOAD =10; //Символическая условность
        private static readonly DisposeHolder _trash = new();

        private void Awake()
        {
            _trash.Retain(OnDownloadedImage.Subscribe(_loadPanel.OnBarValueChanged));
            _trash.Retain(new Func<IDisposable>(() =>
            {
                _downloadHandler.OnDownloadedImage += LoadNewImage;
                return new ActionDisposable(() =>
                    _downloadHandler.OnDownloadedImage -= LoadNewImage);
            })());
            _trash.Retain(_menuWidget.OnGalleryButtonClicked.Subscribe(OpenGallery));
        }

        private void LoadNewImage(Texture2D arg1, int arg2)
        {
            OnDownloadedImage?.Execute();
        }

        private async void OpenGallery()
        {
            _loadPanel.StartLoad(LIMIT_FIRST_DOWNLOAD);
            var images = await _downloadHandler.LoadImages(0, LIMIT_FIRST_DOWNLOAD);

            if (images != null)
                _sceneLoader.SceneLoad();
            else
                _loadPanel.ClosePanel();
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}