using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.Common.View;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private ScoreMenuUI _scoreMenuView;
        
        private ChangeScene _changeScene;
        private PlayerPrefsDataStorage _playerDataStorage;
        private SaveSystem _saveSystem;
        private JsonConverter _jsonConverter;
        private ScoreMenuController _scoreMenuController;
        private ScoreData _scoreData;

        void Awake()
        {
            _playerDataStorage = new PlayerPrefsDataStorage();
            _jsonConverter = new JsonConverter();
            _saveSystem = new SaveSystem(_jsonConverter, _playerDataStorage);
            _changeScene = new ChangeScene(_startButton, _quitButton, _loadButton);
            _scoreData = new ScoreData();
            _scoreMenuController = new ScoreMenuController(_scoreMenuView, _scoreData, _changeScene, _saveSystem);
            _scoreMenuView.Construct(_scoreMenuController);
        }

        private void OnDestroy()
        {
            _changeScene.Unsubscribe();
            _scoreMenuController.Unsubscribe();
            StopAllCoroutines();
        }
    }
}
