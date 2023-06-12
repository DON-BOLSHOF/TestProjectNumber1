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

        public void ChangePosition(int index, int threashold)
        {
            if (Index == index) return;

            var anchoredPosition = Image.rectTransform.anchoredPosition;
            anchoredPosition = index > Index
                ? new Vector2(anchoredPosition.x, anchoredPosition.y - threashold)
                : new Vector2(anchoredPosition.x, anchoredPosition.y + threashold);
            Image.rectTransform.anchoredPosition = anchoredPosition;

            Index = index;
        }
    }
}