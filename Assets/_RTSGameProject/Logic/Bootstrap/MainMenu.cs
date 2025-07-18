using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.View;
using _RTSGameProject.Logic.Common.Services;
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
        
        private MainMenuSceneChanger _mainMenuSceneChanger;
        private ScoreMenuController _scoreMenuController;
        private PurchaseService _purchaseService;
        private ISaveService _saveService;

        [Inject]
        public void Construct(ISaveService saveService,PurchaseService purchaseService, ScoreMenuController scoreMenuController, MainMenuSceneChanger mainMenuSceneChanger)
        {
            _mainMenuSceneChanger = mainMenuSceneChanger;
            _scoreMenuController = scoreMenuController;
            _saveService = saveService;
            _purchaseService = purchaseService;
        }
        
        private async void Awake()
        {
            Subscribe();
            if (_saveService.IsSaveExist())
            {
                await _scoreMenuController.LoadDataAsync();
                _scoreMenuController.GetDataToShowScore(_scoreMenuController.ScoreGameData);
            }
            else
            {
                _scoreMenuController.InitializeScoreGameData();
                _scoreMenuController.GetDataToShowScore(_scoreMenuController.ScoreGameData);
            }
            
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
            _loadButton.onClick.AddListener(OnLoadButtonClick);
            _deleteSavesButton.onClick.AddListener(OnDeleteButtonClick);
            _quitButton.onClick.AddListener(OnQuitButtonClick);
            _noAdsButton.onClick.AddListener(OnNoAdsButtonClick);
        }
        
        private void OnStartButtonClick()
        {
            _mainMenuSceneChanger.ToStartGame();
        }

        private void OnLoadButtonClick()
        {
            _mainMenuSceneChanger.ToLoadGame();
        }
        
        private async void OnDeleteButtonClick()
        {
            await _scoreMenuController.DeleteSaves();
        }

        private void OnQuitButtonClick()
        {
            _mainMenuSceneChanger.ToQuitGame();
        }
        
        private void OnNoAdsButtonClick()
        {
            _purchaseService.Payment();
        }

        private void Unsubscribe()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClick);
            _loadButton.onClick.RemoveListener(OnLoadButtonClick);
            _deleteSavesButton.onClick.RemoveListener(OnDeleteButtonClick);
            _quitButton.onClick.RemoveListener(OnQuitButtonClick);
            _noAdsButton.onClick.RemoveListener(OnNoAdsButtonClick);
        }

    }
}
