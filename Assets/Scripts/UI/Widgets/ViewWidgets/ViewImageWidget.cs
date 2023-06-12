using StaticStorage;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widgets.ViewWidgets
{
    public class ViewImageWidget : MonoBehaviour
    {
        [SerializeField] private Image _myImage;

        private void Awake()
        {
            _myImage.sprite = SpriteStorage.Sprite;
        }
    }
}