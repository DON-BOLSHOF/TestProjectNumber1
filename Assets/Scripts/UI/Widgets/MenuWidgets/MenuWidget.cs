using UnityEngine;
using Utils;

namespace UI.Widgets.MenuWidgets
{
    public sealed class MenuWidget : MonoBehaviour
    {
        public readonly ReactiveEvent OnGalleryButtonClicked = new();

        public void OpenGallery()
        {
            OnGalleryButtonClicked?.Execute();
        }
    }
}
