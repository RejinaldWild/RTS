using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Construction.Model;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _RTSGameProject.Logic.Common.Construction.View
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ProductionPanel : MonoBehaviour
    {
        public event UnityAction OnClickUnit;
        public event UnityAction OnClickExpUnit;
        
        [field: SerializeField] public int Team { get; set; }

        [SerializeField] private Unit _unitPrefabs;
        [SerializeField] private Unit _unitExpPrefab;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _buttonUnit;
        [SerializeField] private Button _buttonExpUnit;
        
        private void Start()
        {
            ToggleUI(false);
            _buttonExpUnit.onClick.AddListener(SpawnUnitClick);
            _buttonUnit.onClick.AddListener(HandleOnClick);
        }

        private void OnDestroy()
        {
            _buttonExpUnit.onClick.RemoveListener(SpawnUnitClick);
            _buttonUnit.onClick.RemoveListener(HandleOnClick);
        }
        
        public void ToggleUI(bool isShow)
        {
            _canvasGroup.alpha = isShow ? 1 : 0;
            _canvasGroup.interactable = isShow;
            _canvasGroup.blocksRaycasts = isShow;
        }
        
        private void HandleOnClick()
        {
            OnClickUnit?.Invoke();
        }
        
        private void SpawnUnitClick()
        {
            OnClickExpUnit?.Invoke();
        }
    }
}