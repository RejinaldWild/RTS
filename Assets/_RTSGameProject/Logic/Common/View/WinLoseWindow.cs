using _RTSGameProject.Logic.Common.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _RTSGameProject.Logic.Common.View
{
    public class WinLoseWindow : MonoBehaviour
    {
        [field:SerializeField] public GameObject WinPanel {get; private set;}
        [field:SerializeField] public GameObject LosePanel {get; private set;}
        
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button[] _mainMenuButtons;
        
        private SceneChanger _sceneChanger;

        private Button NextLevelButton => _nextLevelButton;
        private Button[] MainMenuButtons => _mainMenuButtons;
        
        public void Construct(SceneChanger sceneChanger)
        {
            _sceneChanger = sceneChanger;
        }
        
        public void Subscribe()
        {
            NextLevelButton.onClick.AddListener(_sceneChanger.ToNextLevel);
            
            foreach (Button mainMenuButton in MainMenuButtons)
            {
                mainMenuButton.onClick.AddListener(_sceneChanger.ToMainMenu);
            }
        }

        public void Unsubscribe()
        {
            NextLevelButton.onClick.RemoveListener(_sceneChanger.ToNextLevel);
            
            foreach (Button mainMenuButton in MainMenuButtons)
            {
                mainMenuButton.onClick.RemoveListener(_sceneChanger.ToMainMenu);
            }
        }
    }
}
