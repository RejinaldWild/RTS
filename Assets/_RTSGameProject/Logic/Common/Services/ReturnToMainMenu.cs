using _RTSGameProject.Logic.Common.View;
using UnityEngine.SceneManagement;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ReturnToMainMenu
    {
        private MainMenuButton _mainMenuButton;
        private int _sceneIndex = 1;

        public ReturnToMainMenu(MainMenuButton mainMenuButton)
        {
            _mainMenuButton = mainMenuButton;
            _mainMenuButton.OnClickMainMenu += MainMenuClicked;
        }

        public void Unsubscribe()
        {
            _mainMenuButton.OnClickMainMenu -= MainMenuClicked;
        }

        private void MainMenuClicked()
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
    }
}
