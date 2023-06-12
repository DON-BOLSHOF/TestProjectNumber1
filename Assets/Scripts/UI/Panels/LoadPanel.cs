using UI.Widgets.MenuWidgets;
using UnityEngine;

namespace UI.Panels
{
    public class LoadPanel : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _progressBar;
        [SerializeField] private Animator _animator;

        private int _currentValue;
        private int _endValue;
        
        private static readonly int Open = Animator.StringToHash("Open");
        private static readonly int Close = Animator.StringToHash("Close");

        public void StartLoad(int endValue)
        {
            _animator.SetTrigger(Open);

            _currentValue = 0;
            _endValue = endValue;
        }

        public void OnBarValueChanged()//Такое себе конечно решение, ну да ладно
        {
            _currentValue++;
            _progressBar.SetBarValue((float)_currentValue/_endValue);
        }

        public void ClosePanel()
        {
            _animator.SetTrigger(Close);
        }
    }
}