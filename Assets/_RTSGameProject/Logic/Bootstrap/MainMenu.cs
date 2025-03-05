using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.Common.View;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private ScoreUI _scoreView;
        
        private ChangeScene _changeScene;
        private int _sceneIndex;
        private PlayerPrefsDataStorage _playerDataStorage;
        private SaveSystem _saveSystem;
        private JsonConverter _jsonConverter;
        private ScoreController _scoreController;
        private ScoreData _scoreData;

        void Awake()
        {
            _sceneIndex = SceneManager.GetActiveScene().buildIndex;
            _playerDataStorage = new PlayerPrefsDataStorage();
            _jsonConverter = new JsonConverter();
            _saveSystem = new SaveSystem(_jsonConverter, _playerDataStorage);
            _changeScene = new ChangeScene(_sceneIndex, _startButton, _quitButton, _loadButton);
            _scoreData = new ScoreData();
            _scoreController = new ScoreController(_scoreView, _scoreData, _changeScene, _saveSystem);
            _scoreView.Construct(_scoreController);
        }

        private void OnDestroy()
        {
            _changeScene.Unsubscribe();
            StopAllCoroutines();
        }
    }
}
