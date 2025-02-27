using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _RTSGameProject.Logic.Common.View
{
    public class MainMenuButton : MonoBehaviour
    {
        public event UnityAction OnClickMainMenu;
    
        [SerializeField] private Button[] _mainMenuButtons;
    
        private void Start()
        {
            foreach (Button button in _mainMenuButtons)
            {
                button.onClick.AddListener(MainMenuButtonClicked);
            }
        }

        private void OnDestroy()
        {
            foreach (Button button in _mainMenuButtons)
            {
                button.onClick.RemoveListener(MainMenuButtonClicked);
            }
        
        }

        private void MainMenuButtonClicked()
        {
            OnClickMainMenu?.Invoke();
        }
    }
}
