using _RTSGameProject.Logic.Common.Services;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Selection
{
    public class UnitSelectionBox : MonoBehaviour
    {
        [SerializeField] private RectTransform _boxVisual;
        [SerializeField] private Canvas _canvas;
        private UnitsRepository _unitsRepository;
        private SelectionManager _selectionManager;
        private UnityEngine.Camera _mainCamera;
        private Rect _selectionBox;
        private Vector2 _startPosition;
        private Vector2 _endPosition;

        public void Construct(UnitsRepository unitsRepository, SelectionManager selectionManager)
        {
            _unitsRepository = unitsRepository;
            _selectionManager = selectionManager;
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
            _startPosition = Input.mousePosition;
            _selectionBox = new Rect();
        }

        public void StartDrawAndSelect()
        {
            if (_boxVisual.rect.width > 0 || _boxVisual.rect.height > 0)
            {
                SelectUnits();
            }
                
            _endPosition = Input.mousePosition;
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
        
        private Vector2 GetMousePositionInCanvas()
        {
            Vector2 mousePosition = Input.mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.GetComponent<RectTransform>(),
                mousePosition, _canvas.worldCamera, out Vector2 localPoint);
            localPoint /= _canvas.scaleFactor;

            return localPoint;
        }
        
        private void DrawVisual()
        {
            Vector2 boxStart = _startPosition;
            Vector2 boxEnd = _endPosition;
            Vector2 boxCenter = (boxStart + boxEnd) / 2;
            _boxVisual.position = boxCenter;
            Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
            //_boxVisual.anchoredPosition = (_startPosition + _endPosition)*0.5f;
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
            foreach (var unit in _unitsRepository.AllUnits)
            {
                if (_selectionBox.Contains(_mainCamera.WorldToScreenPoint(unit.transform.position)))
                {
                    _selectionManager.DragSelect(unit);
                }
            }
        }
    }
}
