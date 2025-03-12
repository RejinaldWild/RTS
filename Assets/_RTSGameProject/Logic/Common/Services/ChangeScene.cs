using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ChangeScene
    {
        public Action OnSceneLoad;
        
        private readonly int _mainMenuSceneIndex;
        private int _sceneIndex;
        private Button _startButton;
        private Button _loadButton;
        private Button _quitButton;
        private Button _nextLevelButton;
        private Button[] _mainMenuButtons;
        
        public ChangeScene(Button[] mainMenuButtons, Button nextLevelButton)
        {
            _mainMenuSceneIndex = 0;
            _sceneIndex = SceneManager.GetActiveScene().buildIndex;
            _mainMenuButtons = mainMenuButtons;
            _nextLevelButton = nextLevelButton;
            _nextLevelButton.onClick.AddListener(ToNextLevel);
            foreach (Button button in _mainMenuButtons)
            {
                button.onClick.AddListener(ToMainMenu);
            }
        }
        
        public ChangeScene(Button startButton, Button quitButton, Button loadButton)
        {
            _mainMenuSceneIndex = 0;
            _sceneIndex = _mainMenuSceneIndex;
            _startButton = startButton;
            _quitButton = quitButton;
            _loadButton = loadButton;
            _startButton.onClick.AddListener(ToStartGame);
            _loadButton.onClick.AddListener(ToLoadGame);
            _quitButton.onClick.AddListener(QuitGame);
        }

        public void Unsubscribe()
        {
            if (_startButton != null && _quitButton != null)
            {
                _startButton.onClick.RemoveListener(ToStartGame);
                _loadButton.onClick.RemoveListener(ToLoadGame);
                _quitButton.onClick.RemoveListener(QuitGame);
            }
            
            if (_mainMenuButtons != null)
            {
                _nextLevelButton.onClick.RemoveListener(ToNextLevel);
                foreach (Button button in _mainMenuButtons)
                {
                    button.onClick.RemoveListener(ToMainMenu);
                }
            }
        }
    
        private void ToMainMenu()
        {
            SceneManager.LoadScene(sceneBuildIndex: _mainMenuSceneIndex);
        }
        
        private void ToStartGame()
        {
            _sceneIndex++;
            SceneManager.LoadScene(sceneBuildIndex: _sceneIndex);
        }

        private void ToNextLevel()
        {
            _sceneIndex++;
            if (_sceneIndex <= SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(_sceneIndex);
            }
            else
            {
                _sceneIndex = _mainMenuSceneIndex;
                SceneManager.LoadScene(_sceneIndex);
            }
        }

        private void ToLoadGame()
        {
            OnSceneLoad?.Invoke();
            SceneManager.LoadScene(_sceneIndex);
        }
    
        private void QuitGame()
        {
            EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}
