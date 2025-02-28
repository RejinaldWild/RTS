using _RTSGameProject.Logic.Common.Services;
using UnityEngine;
using UnityEngine.UI;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _quitButton;
        
        private ChangeScene _changeScene;
        private int _sceneIndex;
        
        void Awake()
        {
            _sceneIndex = 0;
            _changeScene = new ChangeScene(_sceneIndex,_startButton, _quitButton);
        }

        private void OnDestroy()
        {
            _changeScene.Unsubscribe();
            StopAllCoroutines();
        }
    }
}
