using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using _RTSGameProject.Logic.Common.Character.Model;

namespace _RTSGameProject.Logic.Common.Building.View
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

        private void HandleOnClick()
        {
            OnClick?.Invoke();
        }
        
        public void ToggleUI(bool isShow)
        {
            _canvasGroup.alpha = isShow ? 1 : 0;
            _canvasGroup.interactable = isShow;
            _canvasGroup.blocksRaycasts = isShow;
        }
    }
}