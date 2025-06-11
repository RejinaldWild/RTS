using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.View;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.SDK;
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
        [SerializeField] private ScoreMenuUI _scoreMenuUI;
        
        private SceneChanger _sceneChanger;
        private ScoreMenuController _scoreMenuController;
        private ISaveService _saveService;
        private ISDK _analyticService;

        [Inject]
        public void Construct(ISDK analyticService, ISaveService saveService, ScoreMenuController scoreMenuController, SceneChanger sceneChanger)
        {
            _sceneChanger = sceneChanger;
            _scoreMenuController = scoreMenuController;
            _saveService = saveService;
            _analyticService = analyticService;
        }
        
        private async void Awake()
        {
            Subscribe();
            if (_saveService.IsSaveExist())
            {
                await _scoreMenuController.LoadDataAsync();
                _scoreMenuController.GetDataToShowScore(_scoreMenuController.ScoreGameData);
                _analyticService.Initialize();
            }
            else
            {
                _scoreMenuController.InitializeScoreGameData();
                _scoreMenuController.GetDataToShowScore(_scoreMenuController.ScoreGameData);
                _analyticService.Initialize();
            }
            
        }

        private void OnDestroy()
        {
            Unsubscribe();
            _analyticService.Dispose();
        }

        private void Subscribe()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
            _loadButton.onClick.AddListener(OnLoadButtonClick);
            _deleteSavesButton.onClick.AddListener(OnDeleteButtonClick);
            _quitButton.onClick.AddListener(OnQuitButtonClick);
        }
        
        private void OnStartButtonClick()
        {
            _sceneChanger.ToStartGame();
        }

        private void OnLoadButtonClick()
        {
            _sceneChanger.ToLoadGame();
        }
        
        private async void OnDeleteButtonClick()
        {
            await _scoreMenuController.DeleteSaves();
        }

        private void OnQuitButtonClick()
        {
            _sceneChanger.ToQuitGame();
        }

        private void Unsubscribe()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClick);
            _loadButton.onClick.RemoveListener(OnLoadButtonClick);
            _deleteSavesButton.onClick.RemoveListener(OnDeleteButtonClick);
            _quitButton.onClick.RemoveListener(OnQuitButtonClick);
        }
    }
}
