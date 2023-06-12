using Components;
using StaticStorage;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widgets.GalleryWidgets
{
    public class ImageButtonWidget : MonoBehaviour
    {
        [SerializeField] private Image _myImage;
        [SerializeField] private SceneLoaderComponent _sceneLoader;
        
        public void Click()
        {
            SpriteStorage.Sprite = _myImage.sprite;
            
            _sceneLoader.SceneLoad();
        }
    }
}