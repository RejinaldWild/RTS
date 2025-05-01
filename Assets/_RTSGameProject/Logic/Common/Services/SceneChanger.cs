using System;
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
        
        public SceneChanger()
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
            #if UNITY_EDITOR 
            EditorApplication.isPlaying = false;
            #endif
            
            Application.Quit();
        }
    }
}
