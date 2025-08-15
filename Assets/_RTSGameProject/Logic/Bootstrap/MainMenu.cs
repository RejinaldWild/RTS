using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _deleteSavesButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private Button _noAdsButton;
        [SerializeField] private ScoreMenuUI _scoreMenuUI;

        private MainMenuService _mainMenuService;
        private ISaveService _saveService;

        [Inject]
        public void Construct(MainMenuService mainMenuService, ISaveService saveService)
        {
            _mainMenuService = mainMenuService;
            _saveService = saveService;
        }
        
        private async void Start()
        {
            await _saveService.Initialize();
            await _mainMenuService.Initialize();
            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _startButton.onClick.AddListener(_mainMenuService.OnStartButtonClick);
            _loadButton.onClick.AddListener(_mainMenuService.OnLoadButtonClick);
            _deleteSavesButton.onClick.AddListener(_mainMenuService.OnDeleteButtonClick);
            _quitButton.onClick.AddListener(_mainMenuService.OnQuitButtonClick);
            _noAdsButton.onClick.AddListener(_mainMenuService.OnNoAdsButtonClick);
        }

        private void Unsubscribe()
        {
            _startButton.onClick.RemoveListener(_mainMenuService.OnStartButtonClick);
            _loadButton.onClick.RemoveListener(_mainMenuService.OnLoadButtonClick);
            _deleteSavesButton.onClick.RemoveListener(_mainMenuService.OnDeleteButtonClick);
            _quitButton.onClick.RemoveListener(_mainMenuService.OnQuitButtonClick);
            _noAdsButton.onClick.RemoveListener(_mainMenuService.OnNoAdsButtonClick);
        }

    }
}
