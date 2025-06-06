using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _RTSGameProject.Logic.Common.Services
{
    public class SceneChanger
    {
        private readonly int _firstLevelSceneIndex;
        private readonly int _mainMenuSceneIndex;
        private readonly SaveScoreService _saveScoreService;
        public ScoreGameData ScoreGameData { get; set; }
        
        public SceneChanger(SaveScoreService saveScoreService)
        {
            _mainMenuSceneIndex = 0;
            _firstLevelSceneIndex = 1;
            _saveScoreService = saveScoreService;
        }
        
        public void Dispose()
        {
            //_scoreGameData.OnScoreGameDataChange -= OnScoreGameDataChanged;
        }
        
        public async void ToStartGame()
        {
            ScoreGameData = new ScoreGameData
            {
                SceneIndex = _firstLevelSceneIndex
            };
            await _saveScoreService.SaveAsync(ScoreGameData);
            SceneManager.LoadScene(_firstLevelSceneIndex);
        }
        
        public async void ToLoadGame()
        {
            if (_saveScoreService.IsSaveExist())
            {
                ScoreGameData = await _saveScoreService.LoadAsync();
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
            // ScoreGameData = await _saveScoreService.LoadAsync();
            // _scoreGameController.ScoreGameData.ChangeScoreGameData(_scoreGameController.ScoreGameData);
            SceneManager.LoadScene(sceneBuildIndex: _mainMenuSceneIndex);
        }
        
        public async void ToNextLevel()
        {
            ScoreGameData = await _saveScoreService.LoadAsync();
            if (ScoreGameData.SceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(ScoreGameData.SceneIndex);
            }
            else
            {
                ScoreGameData.SceneIndex = _mainMenuSceneIndex;
                await _saveScoreService.SaveAsync(ScoreGameData);
                SceneManager.LoadScene(ScoreGameData.SceneIndex);
            }
        }
    }
}