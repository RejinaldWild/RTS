using System.Threading.Tasks;
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
        private readonly int _firstLevelSceneIndex;
        private readonly int _mainMenuSceneIndex;
        private readonly SaveScoreService _saveScoreService;
        
        public ScoreGameData ScoreGameData { get; private set; }
        
        public int MainMenuSceneIndex => _mainMenuSceneIndex;
        
        public SceneChanger(ScoreGameData scoreGameData, SaveScoreService saveScoreService)
        {
            _mainMenuSceneIndex = 0;
            _firstLevelSceneIndex = 1;
            ScoreGameData = scoreGameData;
            _saveScoreService = saveScoreService;
        }
        
        public async UniTask Initialize()
        {
            ScoreGameData = await _saveScoreService.LoadAsync();
            //_scoreGameData.OnScoreGameDataChange += OnScoreGameDataChanged;
        }
        
        public void Dispose()
        {
            //_scoreGameData.OnScoreGameDataChange -= OnScoreGameDataChanged;
        }
        
        public async void ToStartGame()
        {
            ScoreGameData.ChangeScoreGameData(_firstLevelSceneIndex);
            await _saveScoreService.SaveAsync(ScoreGameData);
            SceneManager.LoadScene(sceneBuildIndex: ScoreGameData.SceneIndex);
        }
        
        public void ToLoadGame()
        {
            SceneManager.LoadScene(ScoreGameData.SceneIndex);
        }
    
        public void ToQuitGame()
        {
            #if UNITY_EDITOR 
            EditorApplication.isPlaying = false;
            #endif
            
            Application.Quit();
        }
        
        public async void ToMainMenu()
        {
            await _saveScoreService.SaveAsync(ScoreGameData);
            ScoreGameData.ChangeScoreGameData(_mainMenuSceneIndex);
            SceneManager.LoadScene(sceneBuildIndex: ScoreGameData.SceneIndex);
        }
        
        public async void ToNextLevel()
        {
            if (ScoreGameData.SceneIndex < SceneManager.sceneCountInBuildSettings-1)
            {
                await _saveScoreService.SaveAsync(ScoreGameData);
                SceneManager.LoadScene(ScoreGameData.SceneIndex);
            }
            else
            {
                ScoreGameData.ChangeScoreGameData(MainMenuSceneIndex);
                SceneManager.LoadScene(ScoreGameData.SceneIndex);
            }
        }
        
        private void OnScoreGameDataChanged(ScoreGameData scoreGameData)
        {
            ScoreGameData = scoreGameData;
        }
    }
}