using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _RTSGameProject.Logic.Common.View
{
    public class UIButton: MonoBehaviour
    {
        public event UnityAction OnClickStart;
        public event UnityAction OnClickQuit;
    
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _quitButton;
    
        private void Start()
        {
            _startButton.onClick.AddListener(StartButtonOnClick);
            _quitButton.onClick.AddListener(QuitButtonOnClick);
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveListener(StartButtonOnClick);
            _quitButton.onClick.RemoveListener(QuitButtonOnClick);
        }

        private void StartButtonOnClick()
        {
            OnClickStart?.Invoke();
        }
    
        private void QuitButtonOnClick()
        {
            OnClickQuit?.Invoke();
        }
    }
}
