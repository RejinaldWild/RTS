using UnityEngine;

namespace _RTSGameProject.Logic.Common.Selection
{
    public class UnitSelectionBox : MonoBehaviour
    {
        [SerializeField] private RectTransform _boxVisual;
        private UnitSelectionManager _unitSelectionManager;
 
        private UnityEngine.Camera _mainCamera;
        private Rect _selectionBox;
        private Vector2 _startPosition;
        private Vector2 _endPosition;

        public void Construct(UnitSelectionManager unitSelectionManager)
        {
            _unitSelectionManager = unitSelectionManager;
        }
        
        private void Awake()
        {
            _mainCamera = UnityEngine.Camera.main;
            _startPosition = Vector2.zero;
            _endPosition = Vector2.zero;
            DrawVisual();
        }
 
        public void StartPositionSelectionBox()
        {
            _startPosition = UnityEngine.Input.mousePosition;
            _selectionBox = new Rect();
        }

        public void StartDrawAndSelect()
        {
            if (_boxVisual.rect.width > 0 || _boxVisual.rect.height > 0)
            {
                SelectUnits();
            }
                
            _endPosition = UnityEngine.Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }

        public void EndDrawAndSelect()
        {
            SelectUnits();
            _startPosition = Vector2.zero;
            _endPosition = Vector2.zero;
            DrawVisual();
        }
        
        private void DrawVisual()
        {
            Vector2 boxStart = _startPosition;
            Vector2 boxEnd = _endPosition;
            Vector2 boxCenter = (boxStart + boxEnd) / 2;
            _boxVisual.position = boxCenter;
            Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
            _boxVisual.sizeDelta = boxSize;
        }
 
        private void DrawSelection()
        {
            if (UnityEngine.Input.mousePosition.x < _startPosition.x)
            {
                _selectionBox.xMin = UnityEngine.Input.mousePosition.x;
                _selectionBox.xMax = _startPosition.x;
            }
            else
            {
                _selectionBox.xMin = _startPosition.x;
                _selectionBox.xMax = UnityEngine.Input.mousePosition.x;
            }
 
            if (UnityEngine.Input.mousePosition.y < _startPosition.y)
            {
                _selectionBox.yMin = UnityEngine.Input.mousePosition.y;
                _selectionBox.yMax = _startPosition.y;
            }
            else
            {
                _selectionBox.yMin = _startPosition.y;
                _selectionBox.yMax = UnityEngine.Input.mousePosition.y;
            }
        }
 
        private void SelectUnits()
        {
            foreach (var unit in _unitSelectionManager.AllUnits)
            {
                if (_selectionBox.Contains(_mainCamera.WorldToScreenPoint(unit.transform.position)))
                {
                    _unitSelectionManager.DragSelect(unit);
                }
            }
        }
    }
}
