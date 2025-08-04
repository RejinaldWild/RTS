using System;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _RTSGameProject.Logic.Common.Services
{
    public class SceneChanger: ISceneChanger
    {
        private readonly ISaveService _saveService;
        private readonly int _firstLevelSceneIndex;
        private readonly int _mainMenuSceneIndex;
        
        public ScoreGameData ScoreGameData { get; set; }
        
        public SceneChanger(ISaveService saveService)
        {
            _saveService = saveService;
            _mainMenuSceneIndex = 0;
            _firstLevelSceneIndex = 1;
        }
        
        public async void ToStartGame()
        {
            ScoreGameData = new ScoreGameData
            {
                DateTime = DateTime.Now,
                SceneIndex = _firstLevelSceneIndex
            };
            await _saveService.SaveAsync(ScoreGameData);
            SceneManager.LoadScene(_firstLevelSceneIndex);
        }
        
        public async void ToLoadGame()
        {
            if (await _saveService.IsSaveExist())
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
        
        public void ToMainMenu()
        {
            SceneManager.LoadScene(sceneBuildIndex: _mainMenuSceneIndex);
        }
    }
}