using _RTSGameProject.Logic.Common.Services;
using UnityEngine.UI;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class EntryPointCoreController
    {
        private EntryPointCore _entryPointCore;
        private ChangeScene _changeScene;
        
        public EntryPointCoreController(EntryPointCore entryPointCore, ChangeScene changeScene)
        {
            _entryPointCore = entryPointCore;
            _changeScene = changeScene;
            _entryPointCore.NextLevelButton.onClick.AddListener(_changeScene.ToNextLevel);
            foreach (Button mainMenuButton in _entryPointCore.MainMenuButtons)
            {
                mainMenuButton.onClick.AddListener(_changeScene.ToMainMenu);
            }
        }

        public void Unsubscribe()
        {
            _entryPointCore.NextLevelButton.onClick.RemoveListener(_changeScene.ToNextLevel);
            foreach (Button mainMenuButton in _entryPointCore.MainMenuButtons)
            {
                mainMenuButton.onClick.RemoveListener(_changeScene.ToMainMenu);
            }
        }
    }
}