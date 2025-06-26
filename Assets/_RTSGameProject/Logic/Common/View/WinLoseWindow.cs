using _RTSGameProject.Logic.Common.Services;
using UnityEngine;
using UnityEngine.UI;

namespace _RTSGameProject.Logic.Common.View
{
    public class WinLoseWindow : MonoBehaviour
    {
        [field:SerializeField] public GameObject WinPanel {get; private set;}
        [field:SerializeField] public GameObject LosePanel {get; private set;}
        
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _watchAdButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button[] _mainMenuButtons;
        
        private SceneChanger _sceneChanger;

        public Button ContinueButton => _continueButton;
        public Button WatchAdButton => _watchAdButton;
        private Button NextLevelButton => _nextLevelButton;
        private Button[] MainMenuButtons => _mainMenuButtons;
        
        public void Construct(SceneChanger sceneChanger)
        {
            _sceneChanger = sceneChanger;
        }
        
        public void Subscribe()
        {
            NextLevelButton.onClick.AddListener(_sceneChanger.ToNextLevel);
            WatchAdButton.onClick.AddListener(_sceneChanger.ToReward);
            ContinueButton.onClick.AddListener(_sceneChanger.ToContinueToPlay); 
            foreach (Button mainMenuButton in MainMenuButtons)
            {
                mainMenuButton.onClick.AddListener(_sceneChanger.ToMainMenu);
            }
        }

        public void Unsubscribe()
        {
            NextLevelButton.onClick.RemoveListener(_sceneChanger.ToNextLevel);
            WatchAdButton.onClick.RemoveListener(_sceneChanger.ToReward);
            ContinueButton.onClick.RemoveListener(_sceneChanger.ToContinueToPlay);
            foreach (Button mainMenuButton in MainMenuButtons)
            {
                mainMenuButton.onClick.RemoveListener(_sceneChanger.ToMainMenu);
            }
        }
    }
}
