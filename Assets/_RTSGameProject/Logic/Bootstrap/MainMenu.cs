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
        [SerializeField] private ScoreMenuUI _scoreMenuUI;
        
        private SceneChanger _sceneChanger;
        private ScoreMenuController _scoreMenuController;
        private SaveScoreService _saveScoreService;

        [Inject]
        public void Construct(ScoreMenuController scoreMenuController, SceneChanger sceneChanger, SaveScoreService saveScoreService)
        {
            _saveScoreService = saveScoreService;
            _sceneChanger = sceneChanger;
            _scoreMenuController = scoreMenuController;
        }
        
        private void Awake()
        {
            Subscribe();
            if (_saveScoreService.IsSaveExist())
            {
                _sceneChanger.Initialize();
                _scoreMenuController.GetDataToShowLoadedScore(_sceneChanger.ScoreGameData);
            }
            else
            {
                _scoreMenuController.GetDataToShowStartScore();
            }
        }

        private void OnDestroy()
        {
            _sceneChanger.Dispose();
            Unsubscribe();
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
            if (_saveScoreService.IsSaveExist())
            {
                _sceneChanger.ToLoadGame();
            }
            else
            {
                Debug.Log("You do not have any saves to load");
            }
        }
        
        private async void OnDeleteButtonClick()
        {
            await _saveScoreService.DeleteAsync();
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
