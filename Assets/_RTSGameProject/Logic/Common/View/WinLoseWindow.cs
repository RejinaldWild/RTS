using System;
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
        
        private IWinLoseActions _action;

        public Button ContinueButton => _continueButton;
        public Button WatchAdButton => _watchAdButton;
        private Button NextLevelButton => _nextLevelButton;
        private Button[] MainMenuButtons => _mainMenuButtons;
        
        private Action _toNextLevel;
        private Action _toWatchRewardAd;
        private Action _toContinueToPlay;
        private Action _toMainMenu;
        
        public void Construct(IWinLoseActions action)
        {
            _action = action;
        }
        
        public void Subscribe()
        {
            NextLevelButton.onClick.AddListener(_action.ToNextLevel);
            WatchAdButton.onClick.AddListener(_action.ToWatchRewardAd);
            ContinueButton.onClick.AddListener(_action.ToContinueToPlay); 
            foreach (Button mainMenuButton in MainMenuButtons)
            {
                mainMenuButton.onClick.AddListener(_action.ToMainMenu);
            }
        }

        public void Unsubscribe()
        {
            NextLevelButton.onClick.RemoveListener(_action.ToNextLevel);
            WatchAdButton.onClick.RemoveListener(_action.ToWatchRewardAd);
            ContinueButton.onClick.RemoveListener(_action.ToContinueToPlay);
            foreach (Button mainMenuButton in MainMenuButtons)
            {
                mainMenuButton.onClick.RemoveListener(_action.ToMainMenu);
            }
        }
    }
}
