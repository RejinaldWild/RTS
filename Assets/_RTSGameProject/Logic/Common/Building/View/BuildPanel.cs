using _RTSGameProject.Logic.Common.Character.Model;
using UnityEngine;
using UnityEngine.UI;

namespace _RTSGameProject.Logic.Common.Building.View
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BuildPanel : MonoBehaviour
    {
        [field: SerializeField] public int Team { get; set; }
        
        [SerializeField] private Unit[] _unitsPrefabs;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private Button[] _uiButtons;

        private void Start()
        {
            ToggleUI(false);
        }
        
        public void ToggleUI(bool isShow)
        {
            _canvasGroup.alpha = isShow ? 1 : 0;
            _canvasGroup.interactable = isShow;
            _canvasGroup.blocksRaycasts = isShow;
        }
    }
}