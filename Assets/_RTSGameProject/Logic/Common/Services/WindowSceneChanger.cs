using System;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using UnityEngine.SceneManagement;
using Zenject;

namespace _RTSGameProject.Logic.Common.Services
{
    public class WindowSceneChanger: IInitializable, IDisposable
    {
        private readonly int _mainMenuSceneIndex;
        private readonly ISaveService _saveService;
        private readonly WinLoseGame _winLoseGame;
        
        public ScoreGameData ScoreGameData { get; set; }
        
        public WindowSceneChanger(ISaveService saveService, WinLoseGame winLoseGame)
        {
            _mainMenuSceneIndex = 0;
            _saveService = saveService;
            _winLoseGame = winLoseGame;
        }
        
        public void Initialize()
        {
            _winLoseGame.OnMainMenuClick += OnMainMenuClicked;
            _winLoseGame.OnNextLevel += OnNextLevelClicked;
        }
        
        public void Dispose()
        {
            _winLoseGame.OnMainMenuClick -= OnMainMenuClicked;
            _winLoseGame.OnNextLevel -= OnNextLevelClicked;
        }
        
        private void OnMainMenuClicked()
        {
            SceneManager.LoadScene(sceneBuildIndex: _mainMenuSceneIndex);
        }
        
        private async void OnNextLevelClicked()
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
    }
}