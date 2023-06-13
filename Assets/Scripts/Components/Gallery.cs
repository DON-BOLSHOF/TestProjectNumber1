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
        private int _currentLoadedIndex;
        
        private const int LOADINGBATCH = 8;
        private const int LOADINGTHREASHOLD = 10;
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
            if(currentIndex + LOADINGTHREASHOLD <= _currentLoadedIndex) return;
            if(_currentLoadedIndex >= MAXIMUM_IMAGES) return;

            var loadedIndex = _currentLoadedIndex;
            _currentLoadedIndex += LOADINGBATCH;

            var sprites = await _galleryLoader.LoadSprites(loadedIndex, _currentLoadedIndex);//Пачками грузим
            var spriteIndex = 0;
            foreach (var scrollImage in _scrollingImages.Skip(loadedIndex).Take(sprites.Count).Where(i => i.Image.sprite == null))
            {
                scrollImage.Image.sprite = sprites[spriteIndex];
                spriteIndex++;
            }
        }

        private async void Start()
        {
            var sprites = await _galleryLoader.LoadSprites(0, 10);

            for (var i = 0; i < LOADINGTHREASHOLD; i++)
            {
                _images[i].sprite = sprites[i];
            }

            _currentLoadedIndex = LOADINGTHREASHOLD;
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}