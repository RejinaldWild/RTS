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
        private ScoreGameData _scoreGameData;
        
        private readonly int _firstLevelSceneIndex;
        private readonly int _mainMenuSceneIndex;
        private readonly SaveScoreService _saveScoreService;
        
        public int MainMenuSceneIndex => _mainMenuSceneIndex;
        
        public SceneChanger(ScoreGameData scoreGameData, SaveScoreService saveScoreService)
        {
            _mainMenuSceneIndex = 0;
            _firstLevelSceneIndex = 1;
            _scoreGameData = scoreGameData;
            _saveScoreService = saveScoreService;
            _sceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
        
        public async UniTask LoadStartData()
        {
            if (_saveScoreService.IsSaveExist())
            {
                _scoreGameData = await _saveScoreService.LoadAsync();
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
            _scoreGameData.SceneIndex = _firstLevelSceneIndex;
            SceneManager.LoadScene(sceneBuildIndex: _scoreGameData.SceneIndex);
        }

        public void ToNextLevel()
        {
            if (_scoreGameData.SceneIndex < SceneManager.sceneCountInBuildSettings-1)
            {
                _scoreGameData.SceneIndex = _sceneIndex+1;
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
            _scoreGameData = await _saveScoreService.LoadAsync();
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