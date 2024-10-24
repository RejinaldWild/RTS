using UnityEngine;

namespace RTS.Scripts
{
    public class UnitSelectionBox : MonoBehaviour
    {
        [SerializeField] private RectTransform _boxVisual;
        [SerializeField] private UnitSelectionManager _unitSelectionManager;
 
        private Camera _mainCamera;
        private Rect _selectionBox;
        private Vector2 _startPosition;
        private Vector2 _endPosition;
 
        private void Start()
        {
            _mainCamera = Camera.main;
            _startPosition = Vector2.zero;
            _endPosition = Vector2.zero;
            DrawVisual();
        }
 
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startPosition = Input.mousePosition;
                _selectionBox = new Rect();
            }
 
            if (Input.GetMouseButton(0))
            {
                if (Input.GetKey(KeyCode.LeftShift)==false &&(_boxVisual.rect.width > 0 || _boxVisual.rect.height > 0))
                {
                    _unitSelectionManager.DeselectAll();
                    SelectUnits();
                }
                else
                {
                    SelectUnits();
                }
                
                _endPosition = Input.mousePosition;
                DrawVisual();
                DrawSelection();
            }
        
            if (Input.GetMouseButtonUp(0))
            {
                SelectUnits();
                _startPosition = Vector2.zero;
                _endPosition = Vector2.zero;
                DrawVisual();
            }
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
            if (Input.mousePosition.x < _startPosition.x)
            {
                _selectionBox.xMin = Input.mousePosition.x;
                _selectionBox.xMax = _startPosition.x;
            }
            else
            {
                _selectionBox.xMin = _startPosition.x;
                _selectionBox.xMax = Input.mousePosition.x;
            }
 
            if (Input.mousePosition.y < _startPosition.y)
            {
                _selectionBox.yMin = Input.mousePosition.y;
                _selectionBox.yMax = _startPosition.y;
            }
            else
            {
                _selectionBox.yMin = _startPosition.y;
                _selectionBox.yMax = Input.mousePosition.y;
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
