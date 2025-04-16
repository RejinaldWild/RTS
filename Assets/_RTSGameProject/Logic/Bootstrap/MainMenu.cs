using _RTSGameProject.Logic.Common.Score.View;
using _RTSGameProject.Logic.Common.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _quitButton;
        
        [SerializeField] private ScoreMenuUI _scoreMenuUI;
        
        private ChangeScene _changeScene;
        private IInstantiator _diContainer;
        private MainMenuController _mainMenuController;
        private ScoreMenuController _scoreMenuController;
        
        public Button StartButton => _startButton;
        public Button LoadButton => _loadButton;
        public Button QuitButton => _quitButton;

        [Inject]
        public void Construct(IInstantiator diContainer)
        {
            _diContainer = diContainer;
            _changeScene = _diContainer.Instantiate<ChangeScene>();
            _scoreMenuController = _diContainer.Instantiate<ScoreMenuController>();
        }
        
        void Awake()
        {
            _mainMenuController = new MainMenuController(this, _changeScene);
            _scoreMenuUI.Construct(_scoreMenuController);
        }

        private void OnDestroy()
        {
            _scoreMenuController.Unsubscribe();
            _mainMenuController.Unsubscribe();
            StopAllCoroutines();
        }
    }
}
