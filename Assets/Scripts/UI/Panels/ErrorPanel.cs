using Handlers.SystemHandler;
using TMPro;
using UnityEngine;
using Utils;
using Zenject;

namespace UI.Panels
{
    public class ErrorPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _errorDescription;
        [SerializeField] private Animator _animator;
        
        [Inject] private ErrorHandler _errorHandler;

        private readonly DisposeHolder _trash = new();
        private static readonly int Open = Animator.StringToHash("Open");
        private static readonly int Close = Animator.StringToHash("Close");

        private void Awake()
        {
            _trash.Retain(_errorHandler.OnCatchedSomething.Subscribe(CatchError));
            
            DontDestroyOnLoad(gameObject);
        }

        private void CatchError(string error)
        {
            _errorDescription.text = error;
            
            _animator.SetTrigger(Open);
        }
        
        private void ClosePanel()
        {
            _animator.SetTrigger(Close);
        }
    }
}