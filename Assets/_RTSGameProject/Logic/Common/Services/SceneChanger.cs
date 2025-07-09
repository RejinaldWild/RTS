using System;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _RTSGameProject.Logic.Common.Services
{
    public class SceneChanger
    {
        public event Action OnWatchAdReward;
        public event Action OnContinueToPlay;
        public event Action OnMainMenuClick;
        
        private readonly int _firstLevelSceneIndex;
        private readonly int _mainMenuSceneIndex;
        private readonly ISaveService _saveService;
        
        public ScoreGameData ScoreGameData { get; set; }
        
        public SceneChanger(ISaveService saveService)
        {
            _mainMenuSceneIndex = 0;
            _firstLevelSceneIndex = 1;
            _saveService = saveService;
        }
        
        public async void ToStartGame()
        {
            ScoreGameData = new ScoreGameData
            {
                SceneIndex = _firstLevelSceneIndex
            };
            await _saveService.SaveAsync(ScoreGameData);
            SceneManager.LoadScene(_firstLevelSceneIndex);
        }
        
        public async void ToLoadGame()
        {
            if (_saveService.IsSaveExist())
            {
                ScoreGameData = await _saveService.LoadAsync();
                SceneManager.LoadScene(ScoreGameData.SceneIndex);
            }
            else
            {
                Debug.Log("You do not have any saves to load");
            }
        }
    
        public void ToQuitGame()
        {
            #if UNITY_EDITOR 
            EditorApplication.isPlaying = false;
            #endif
            
            Application.Quit();
        }
        
        public void ToMainMenu()
        {
            OnMainMenuClick?.Invoke();
            SceneManager.LoadScene(sceneBuildIndex: _mainMenuSceneIndex);
        }
        
        public async void ToNextLevel()
        {
            ScoreGameData = await _saveService.LoadAsync();
            if (ScoreGameData.SceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(ScoreGameData.SceneIndex);
            }
            else
            {
                ScoreGameData.SceneIndex = _mainMenuSceneIndex;
                await _saveService.SaveAsync(ScoreGameData);
                SceneManager.LoadScene(ScoreGameData.SceneIndex);
            }
        }

        public void ToWatchRewardAd()
        {
            OnWatchAdReward?.Invoke();
        }

        public void ToContinueToPlay()
        {
            OnContinueToPlay?.Invoke();
        }
    }
}