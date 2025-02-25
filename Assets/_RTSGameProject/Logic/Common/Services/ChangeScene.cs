using _RTSGameProject.Logic.Common.View;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ChangeScene
    {
        private UIButton _button;
        private readonly int _sceneIndex = 1;
    
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
            SceneManager.LoadScene(sceneBuildIndex: _sceneIndex);
        }
    
        private void QuitGame()
        {
            EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}
