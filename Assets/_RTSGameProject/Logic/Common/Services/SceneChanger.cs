using System;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _RTSGameProject.Logic.Common.Services
{
    public class SceneChanger
    {
        public event Action OnSceneLoad;
        
        private int _sceneIndex;
        private int _mainMenuSceneIndex;
        private ScoreGameData _scoreGameData;
        private SaveSystem _saveSystem;
        
        public int MainMenuSceneIndex => _mainMenuSceneIndex;
        
        public SceneChanger(ScoreGameData scoreGameData, SaveSystem saveSystem)
        {
            _mainMenuSceneIndex = 0;
            _scoreGameData = scoreGameData;
            _saveSystem = saveSystem;
            _sceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
        
        public async UniTask LoadStartData()
        {
            if (_saveSystem.IsSaveExist<ScoreGameData>())
            {
                _scoreGameData = await _saveSystem.LoadAsync<ScoreGameData>();
            }
            _scoreGameData.SceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
        
        public void ToMainMenu()
        {
            _scoreGameData.SceneIndex = _mainMenuSceneIndex;
            SceneManager.LoadScene(sceneBuildIndex: _scoreGameData.SceneIndex);
        }
        
        public void ToStartGame()
        {
            _scoreGameData.SceneIndex = _sceneIndex+1;
            SceneManager.LoadScene(sceneBuildIndex: _scoreGameData.SceneIndex);
        }

        public void ToNextLevel()
        {
            _scoreGameData.SceneIndex = _sceneIndex+1;
            if (_scoreGameData.SceneIndex <= SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(_scoreGameData.SceneIndex);
            }
            else
            {
                _scoreGameData.SceneIndex = _mainMenuSceneIndex;
                SceneManager.LoadScene(_scoreGameData.SceneIndex);
            }
        }

        public async void ToLoadGame()
        {
            _scoreGameData = await _saveSystem.LoadAsync<ScoreGameData>();
            SceneManager.LoadScene(_scoreGameData.SceneIndex);
            OnSceneLoad?.Invoke();
        }
    
        public void ToQuitGame()
        {
            #if UNITY_EDITOR 
            EditorApplication.isPlaying = false;
            #endif
            
            Application.Quit();
        }
    }
}