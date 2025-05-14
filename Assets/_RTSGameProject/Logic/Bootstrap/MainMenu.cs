using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
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
        private IInstantiator _diContainer;
        private ScoreMenuController _scoreMenuController;
        private SaveSystem _saveSystem;
        
        private Button StartButton => _startButton;
        private Button LoadButton => _loadButton;
        private Button DeleteSavesButton => _deleteSavesButton;
        private Button QuitButton => _quitButton;

        [Inject]
        public void Construct(ScoreMenuController scoreMenuController, SceneChanger sceneChanger, SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _sceneChanger = sceneChanger;
            _scoreMenuController = scoreMenuController;
        }
        
        void Awake()
        {
            Subscribe();
            if (_saveSystem.IsSaveExist<ScoreGameData>())
            {
                _scoreMenuController.LoadData();
            }
            else
            {
                _sceneChanger.LoadStartData();
            }
        }

        private void OnDestroy()
        {
            Unsubscribe();
            StopAllCoroutines();
        }

        private void Subscribe()
        {
            StartButton.onClick.AddListener(OnStartButtonClick);
            LoadButton.onClick.AddListener(OnLoadButtonClick);
            DeleteSavesButton.onClick.AddListener(OnDeleteButtonClick);
            QuitButton.onClick.AddListener(OnQuitButtonClick);
        }
        
        private void OnStartButtonClick()
        {
            _sceneChanger.ToStartGame();
        }

        private void OnLoadButtonClick()
        {
            if (_saveSystem.IsSaveExist<ScoreGameData>())
            {
                _sceneChanger.ToLoadGame();
            }
            else
            {
                Debug.Log("You do not have any saves to load");
            }
        }
        
        private void OnDeleteButtonClick()
        {
            _saveSystem.DeleteAsync<ScoreGameData>();
        }

        private void OnQuitButtonClick()
        {
            _sceneChanger.ToQuitGame();
        }

        private void Unsubscribe()
        {
            StartButton.onClick.RemoveListener(OnStartButtonClick);
            LoadButton.onClick.RemoveListener(OnLoadButtonClick);
            DeleteSavesButton.onClick.RemoveListener(OnDeleteButtonClick);
            QuitButton.onClick.RemoveListener(OnQuitButtonClick);
        }
    }
}
