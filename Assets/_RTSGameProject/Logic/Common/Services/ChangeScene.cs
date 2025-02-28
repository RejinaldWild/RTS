using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ChangeScene
    {
        private int _sceneIndex;
        private Button _startButton;
        private Button _quitButton;
        private Button[] _mainMenuButtons;
        private readonly int _mainMenuSceneIndex;
        
        public ChangeScene(Button[] mainMenuButtons)
        {
            _mainMenuSceneIndex = 0;
            _mainMenuButtons = mainMenuButtons;
            foreach (Button button in _mainMenuButtons)
            {
                button.onClick.AddListener(ToMainMenu);
            }
        }
        
        public ChangeScene(int sceneIndex, Button startButton, Button quitButton)
        {
            _mainMenuSceneIndex = 0;
            _sceneIndex = sceneIndex;
            _startButton = startButton;
            _quitButton = quitButton;
            _startButton.onClick.AddListener(ToNextLevel);
            _quitButton.onClick.AddListener(QuitGame);
        }

        public void Unsubscribe()
        {
            if (_startButton != null && _quitButton != null)
            {
                _startButton.onClick.RemoveListener(ToNextLevel);
                _quitButton.onClick.RemoveListener(QuitGame);
            }
            
            if (_mainMenuButtons != null)
            {
                foreach (Button button in _mainMenuButtons)
                {
                    button.onClick.RemoveListener(ToMainMenu);
                }
            }
        }
    
        public void ToMainMenu()
        {
            SceneManager.LoadScene(sceneBuildIndex: _mainMenuSceneIndex);
        }
        
        private void ToNextLevel()
        {
            if (_sceneIndex + 1 <= SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(sceneBuildIndex: _sceneIndex + 1);
            }
            else
            {
                _sceneIndex = 0;
                SceneManager.LoadScene(sceneBuildIndex: _sceneIndex);
            }
        }
    
        private void QuitGame()
        {
            EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}
