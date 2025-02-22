using _RTSGameProject.Logic.Common.Character.Model;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _RTSGameProject.Logic.Common.Construction.View
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BuildPanel : MonoBehaviour
    {
        public event UnityAction OnClick;
        
        [field: SerializeField] public int Team { get; set; }
        
        [SerializeField] private Unit[] _unitsPrefabs;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button[] _buttons;
        
        private void Start()
        {
            ToggleUI(false);
            foreach (var button in _buttons)
            {
                button.onClick.AddListener(HandleOnClick);
            }
        }

        private void OnDestroy()
        {
            foreach (var button in _buttons)
            {
                button.onClick.RemoveListener(HandleOnClick);
            }
        }
        
        public void ToggleUI(bool isShow)
        {
            _canvasGroup.alpha = isShow ? 1 : 0;
            _canvasGroup.interactable = isShow;
            _canvasGroup.blocksRaycasts = isShow;
        }
        private void HandleOnClick()
        {
            OnClick?.Invoke();
        }
    }
}