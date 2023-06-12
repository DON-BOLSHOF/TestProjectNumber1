using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.Widgets.GalleryWidgets
{
    public sealed class ScrollBarWidget : MonoBehaviour
    {
        [SerializeField] private Scrollbar _scrollBarWidget;
        [SerializeField] private bool _isInversed;

        private float _lastValue;
        
        public BarDirection BarDirection { get; private set; }
        public ReactiveEvent<float> OnScrollChanged = new();

        private void Awake()
        {
            _scrollBarWidget.onValueChanged.AddListener(ScrollChangedValue);
        }

        private void ScrollChangedValue(float value)
        {
            var temp = _isInversed? 1-value: value;
            BarDirection = temp > _lastValue ? BarDirection.Down : BarDirection.Up;

            OnScrollChanged?.Execute(temp);
            _lastValue = temp;
        }

        private void OnDestroy()
        {
            _scrollBarWidget.onValueChanged.RemoveListener(ScrollChangedValue);
        }
    }

    public enum BarDirection
    {
        Down,
        Up
    }
}