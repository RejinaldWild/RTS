using _RTSGameProject.Logic.Common.View;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ChangeScene
    {
        private UIButton _button;
        private int _sceneIndex = 0;
    
        public ChangeScene(UIButton button)
        {
            _button = button;
        }

        public void Subscribe()
        {
            _button.OnClickStart += StartScene;
            _button.OnClickQuit += QuitGame;
        }

        public void Unsubscribe()
        {
            _button.OnClickStart -= StartScene;
            _button.OnClickQuit -= QuitGame;
        }
    
        private void StartScene()
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
