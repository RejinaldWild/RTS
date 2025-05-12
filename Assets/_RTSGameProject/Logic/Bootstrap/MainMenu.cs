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
        
        public Button StartButton => _startButton;
        public Button LoadButton => _loadButton;
        public Button DeleteSavesButton => _deleteSavesButton;
        public Button QuitButton => _quitButton;

        [Inject]
        public void Construct(IInstantiator diContainer, SaveSystem saveSystem)
        {
            _diContainer = diContainer;
            _saveSystem = saveSystem;
            _sceneChanger = _diContainer.Instantiate<SceneChanger>();
            _scoreMenuController = _diContainer.Instantiate<ScoreMenuController>();
        }
        
        void Awake()
        {
            Subscribe();
            if (_saveSystem.IsSaveExist<ScoreGameData>().ToString() != "False")
            {
                _scoreMenuController.LoadData();
            }
            else
            {
                _sceneChanger.LoadStartData();
            }
        }

        private void Update()
        {
            _scoreMenuController.Update();
        }

        private void OnDestroy()
        {
            Unsubscribe();
            StopAllCoroutines();
        }

        private void Subscribe()
        {
            _scoreMenuController.Subscribe();
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
            if (_saveSystem.IsSaveExist<ScoreGameData>().ToString() != "False")
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
            _scoreMenuController.Unsubscribe();
            StartButton.onClick.RemoveListener(OnStartButtonClick);
            LoadButton.onClick.RemoveListener(OnLoadButtonClick);
            DeleteSavesButton.onClick.RemoveListener(OnDeleteButtonClick);
            QuitButton.onClick.RemoveListener(OnQuitButtonClick);
        }
    }
}
