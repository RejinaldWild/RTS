using _RTSGameProject.Logic.Common.Services;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Selection
{
    public class SelectionBox : MonoBehaviour
    {
        [SerializeField] private RectTransform _boxVisual;
        [SerializeField] private Canvas _canvas;
        private UnitsRepository _unitsRepository;
        private BuildingsRepository _buildingsRepository;
        private SelectionManager _selectionManager;
        private UnityEngine.Camera _mainCamera;
        private Rect _selectionBox;
        private Vector2 _startPosition;
        private Vector2 _endPosition;
        
        public void Construct(UnitsRepository unitsRepository, BuildingsRepository buildingsRepository, SelectionManager selectionManager)
        {
            _unitsRepository = unitsRepository;
            _buildingsRepository = buildingsRepository;
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
                ShowPreselect();
            }
            
            _endPosition = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }

        public void EndDrawAndSelect()
        {
            Select();
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
            Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y))/ _canvas.scaleFactor;
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
 
        private void Select() // at first must be units, if no units in box, then select buildings
        {
            foreach (var building in _buildingsRepository.AllBuildings)
            {
                if (_selectionBox.Contains(_mainCamera.WorldToScreenPoint(building.transform.position)))
                {
                    _selectionManager.DragSelect(building);
                }
            }

            foreach (var unit in _unitsRepository.AllUnits)
            {
                if (_selectionBox.Contains(_mainCamera.WorldToScreenPoint(unit.transform.position)))
                {
                    _selectionManager.DragSelect(unit);
                }
            }
        }
        
        private void ShowPreselect() // at first must be units, if no units in box, then preselect buildings
        {
            foreach (var building in _buildingsRepository.AllBuildings)
            {
                if (_selectionBox.Contains(_mainCamera.WorldToScreenPoint(building.transform.position)))
                {
                    _selectionManager.ShowDragPreselect(building);
                }
                if (!_selectionBox.Contains(_mainCamera.WorldToScreenPoint(building.transform.position)))
                {
                    _selectionManager.HidDragPreselect(building);
                }
            }

            foreach (var unit in _unitsRepository.AllUnits)
            {
                if (_selectionBox.Contains(_mainCamera.WorldToScreenPoint(unit.transform.position)))
                {
                    _selectionManager.ShowDragPreselect(unit);
                }
                if (!_selectionBox.Contains(_mainCamera.WorldToScreenPoint(unit.transform.position)))
                {
                    _selectionManager.HidDragPreselect(unit);
                }
            }
        }
    }
}
