using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ChangeScene
    {
        public Action OnSceneLoad;
        
        private int _sceneIndex;
        private int _mainMenuSceneIndex;
        
        public ChangeScene()
        {
            _mainMenuSceneIndex = 0;
            _sceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
    
        public void ToMainMenu()
        {
            SceneManager.LoadScene(sceneBuildIndex: _mainMenuSceneIndex);
        }
        
        public void ToStartGame()
        {
            _sceneIndex++;
            SceneManager.LoadScene(sceneBuildIndex: _sceneIndex);
        }

        public void ToNextLevel()
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

        public void ToLoadGame()
        {
            SceneManager.LoadScene(_sceneIndex);
            OnSceneLoad?.Invoke();
        }
    
        public void ToQuitGame()
        {
            EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}
