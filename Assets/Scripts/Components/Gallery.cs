using System.Linq;
using Loaders;
using UI;
using UI.Widgets.GalleryWidgets;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zenject;

namespace Components
{
    public class Gallery : MonoBehaviour
    {
        [SerializeField] private Image[] _images;
        [SerializeField] private ScrollBarWidget _scrollBarWidget;

        [Inject] private GalleryLoader _galleryLoader;

        private ScrollingImage[] _scrollingImages;

        private readonly DisposeHolder _trash = new();
        private int _currentIndex;
        private const int MAXIMUM_IMAGES = 66;

        private void Awake()
        {
            _scrollingImages = new ScrollingImage[_images.Length];
            for (var i = 0; i < _scrollingImages.Length; i++)
            {
                _scrollingImages[i] = new ScrollingImage(_images[i], i);
            }

            _trash.Retain(_scrollBarWidget.OnScrollChanged.Subscribe(ChangeImages));
        }

        private async void ChangeImages(float scrollPerCent)
        {
            var currentIndex = (int)(MAXIMUM_IMAGES * scrollPerCent);
            if(currentIndex == _currentIndex) return;

            _currentIndex = currentIndex;
            
            foreach (var _scrollImage in _scrollingImages.Skip(currentIndex).Take(8).Where(i => i.Image.sprite == null))
            {
                _scrollImage.Image.sprite = await _galleryLoader.LoadSprite(_scrollImage.Index);
                currentIndex++;
            }
        }

        private async void Start()
        {
            for (var i = 0; i < 10; i++)
            {
                _images[i].sprite = await _galleryLoader.LoadSprite(i);
            }
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}