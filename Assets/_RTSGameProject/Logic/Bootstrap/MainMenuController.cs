using _RTSGameProject.Logic.Common.Services;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class MainMenuController
    {
        private MainMenu _mainMenu;
        private ChangeScene _changeScene;

        public MainMenuController(MainMenu mainMenu, ChangeScene changeScene)
        {
            _mainMenu = mainMenu;
            _changeScene = changeScene;
            _mainMenu.StartButton.onClick.AddListener(OnStartButtonClick);
            _mainMenu.LoadButton.onClick.AddListener(OnLoadButtonClick);
            _mainMenu.QuitButton.onClick.AddListener(OnQuitButtonClick);
        }

        public void OnStartButtonClick()
        {
            _changeScene.ToStartGame();
        }

        public void OnLoadButtonClick()
        {
            _changeScene.ToLoadGame();
        }

        public void OnQuitButtonClick()
        {
            _changeScene.ToQuitGame();
        }

        public void Unsubscribe()
        {
            _mainMenu.StartButton.onClick.RemoveListener(OnStartButtonClick);
            _mainMenu.LoadButton.onClick.RemoveListener(OnLoadButtonClick);
            _mainMenu.QuitButton.onClick.RemoveListener(OnQuitButtonClick);
        }
    }
}