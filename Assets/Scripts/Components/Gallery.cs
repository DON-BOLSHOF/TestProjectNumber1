using System;
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
        private int _threasholdForImages;
        private const int MINIMUM_IMAGES = 0;
        private const int MAXIMUM_IMAGES = 66;

        private void Awake()
        {
            _threasholdForImages = (int)(_images[0].rectTransform.anchoredPosition.y -
                                               _images[^1].rectTransform.anchoredPosition.y
                                               + _images[0].rectTransform.rect.height);
            
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
            if (currentIndex is >= MAXIMUM_IMAGES or < MINIMUM_IMAGES) return;

            switch (_scrollBarWidget.BarDirection)
            {
                case BarDirection.Down:
                    var imageToChange = _scrollingImages.Aggregate((minItem, nextItem) =>
                        minItem.Index < nextItem.Index ? minItem : nextItem);

                    if (currentIndex - imageToChange.Index > 4 && imageToChange.Index + _scrollingImages.Length < MAXIMUM_IMAGES)
                    {
                        var indexToChange = imageToChange.Index + _scrollingImages.Length;
                        imageToChange.ChangePosition(indexToChange, _threasholdForImages);
                        imageToChange.Image.sprite = await _galleryLoader.LoadSprite(indexToChange);
                    }
                    break;
                case BarDirection.Up:
                    imageToChange = _scrollingImages.Aggregate((maxItem, nextItem) =>
                        maxItem.Index > nextItem.Index ? maxItem : nextItem);

                    if (imageToChange.Index - currentIndex > 4 && imageToChange.Index - _scrollingImages.Length >= MINIMUM_IMAGES)
                    {
                        var indexToChange = imageToChange.Index - _scrollingImages.Length;
                        imageToChange.ChangePosition(indexToChange, _threasholdForImages);
                        imageToChange.Image.sprite = await _galleryLoader.LoadSprite(indexToChange);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async void Start()
        {
            for (var i = 0; i < _images.Length; i++)
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