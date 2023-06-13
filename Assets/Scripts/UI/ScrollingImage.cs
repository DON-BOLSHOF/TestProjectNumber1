using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScrollingImage
    {
        public Image Image { get; }
        public int Index { get; private set; }
        
        public ScrollingImage(Image image, int currentIndex)
        {
            Image = image;
            Index = currentIndex;
        }
    }
}