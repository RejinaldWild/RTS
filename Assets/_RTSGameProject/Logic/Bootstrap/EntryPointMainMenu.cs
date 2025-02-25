using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.Common.View;
using UnityEngine;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class EntryPointMainMenu : MonoBehaviour
    {
        [SerializeField] private UIButton _uiButton;
        
        private ChangeScene _changeScene;
        
        void Awake()
        {
            _changeScene = new ChangeScene(_uiButton);
            _changeScene.Subscribe();
        }

        private void OnDestroy()
        {
            _changeScene.Unsubscribe();
            StopAllCoroutines();
        }
    }
}
