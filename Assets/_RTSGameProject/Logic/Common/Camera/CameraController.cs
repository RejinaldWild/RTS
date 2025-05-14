using UnityEngine;
using UnityEngine.EventSystems;

namespace _RTSGameProject.Logic.Common.Camera
{
    public class CameraController : MonoBehaviour
    {
        [Header("General")] 
        [SerializeField] private Transform _cameraTransform;
        private Transform _followTransform;
        private Vector3 _newPosition;
        private Vector3 _dragStartPosition;
        private Vector3 _dragCurrentPosition;
 
        [Header("Optional Functionality")]
        [SerializeField] private bool _moveWithKeyboad;
        [SerializeField] private bool _moveWithEdgeScrolling;
        [SerializeField] private bool _moveWithMouseDrag;
 
        [Header("Keyboard Movement")]
        [SerializeField] private float _fastSpeed;
        [SerializeField] private float _normalSpeed;
 
        [Header("Edge Scrolling Movement")]
        public Texture2D CursorArrowUp;
        public Texture2D CursorArrowDown;
        public Texture2D CursorArrowLeft;
        public Texture2D CursorArrowRight;
        [SerializeField] private float _edgeSize = 50f;

        [Header("Attack Mouse Arrow")] 
        public Texture2D CursorArrowAttack;
        
        private float _movementSpeed;
        private LayerMask _clickable;
        private bool _isCursorSet = false;
        private CursorArrow _currentCursor = CursorArrow.DEFAULT;
    
        private enum CursorArrow
        {
            UP,
            DOWN,
            LEFT,
            RIGHT,
            ATTACK,
            DEFAULT
        }

        public void Construct(LayerMask clickable)
        {
            _clickable = clickable;
        }
        
        private void Start()
        {
            _newPosition = transform.position;
            _movementSpeed = _normalSpeed;
        }
 
        public void Update()
        {
            if (_followTransform != null)
            {
                transform.position = _followTransform.position;
            }
            else
            {
                HandleCameraMovement();
            }
 
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _followTransform = null;
            }
        }
 
        private void HandleCameraMovement()
        {
            // Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            // RaycastHit hit;
            // Physics.Raycast(ray, out hit, Mathf.Infinity, _clickable);
            // if (hit.collider.TryGetComponent(out Unit unit) && unit.Team!=0)
            // {
            //     ChangeCursor(CursorArrow.ATTACK);
            // }
            
            if (_moveWithMouseDrag)
            {
                HandleMouseDragInput();
            }
 
            if (_moveWithKeyboad)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    _movementSpeed = _fastSpeed;
                }
                else
                {
                    _movementSpeed = _normalSpeed;
                }
 
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    _newPosition += (transform.forward * _movementSpeed);
                }
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    _newPosition += (transform.forward * -_movementSpeed);
                }

                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    _newPosition += (transform.right * _movementSpeed);
                }
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    _newPosition += (transform.right * -_movementSpeed);
                }
            }
        
            if (_moveWithEdgeScrolling)
            {
                if (Input.mousePosition.x > Screen.width - _edgeSize)
                {
                    _newPosition += (transform.right * _movementSpeed);
                    ChangeCursor(CursorArrow.RIGHT);
                    _isCursorSet = true;
                }
            
                else if (Input.mousePosition.x < _edgeSize)
                {
                    _newPosition += (transform.right * -_movementSpeed);
                    ChangeCursor(CursorArrow.LEFT);
                    _isCursorSet = true;
                }
            
                else if (Input.mousePosition.y > Screen.height - _edgeSize)
                {
                    _newPosition += (transform.forward * _movementSpeed);
                    ChangeCursor(CursorArrow.UP);
                    _isCursorSet = true;
                }
            
                else if (Input.mousePosition.y < _edgeSize)
                {
                    _newPosition += (transform.forward * -_movementSpeed);
                    ChangeCursor(CursorArrow.DOWN);
                    _isCursorSet = true;
                }
                else
                {
                    if (_isCursorSet)
                    {
                        ChangeCursor(CursorArrow.DEFAULT);
                        _isCursorSet = false;
                    }
                }
            }

            transform.position = _newPosition;
            
            //Smooth move camera
            //Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * _movementSpeed);
            
            //Edges is locked for mouse
            //Cursor.lockState = CursorLockMode.Confined;
        }
 
        private void ChangeCursor(CursorArrow newCursor)
        {
            if (_currentCursor != newCursor)
            {
                switch (newCursor)
                {
                    case CursorArrow.UP:
                        Cursor.SetCursor(CursorArrowUp, Vector2.zero, CursorMode.Auto);
                        break;
                    case CursorArrow.DOWN:
                        Cursor.SetCursor(CursorArrowDown, new Vector2(CursorArrowDown.width, CursorArrowDown.height), CursorMode.Auto);
                        break;
                    case CursorArrow.LEFT:
                        Cursor.SetCursor(CursorArrowLeft, Vector2.zero, CursorMode.Auto);
                        break;
                    case CursorArrow.RIGHT:
                        Cursor.SetCursor(CursorArrowRight, new Vector2(CursorArrowRight.width, CursorArrowRight.height), CursorMode.Auto);
                        break;
                    case CursorArrow.ATTACK:
                        Cursor.SetCursor(CursorArrowAttack, new Vector2(CursorArrowRight.width, CursorArrowRight.height), CursorMode.Auto);
                        break;
                    case CursorArrow.DEFAULT:
                        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                        break;
                }
 
                _currentCursor = newCursor;
            }
        }
    
        private void HandleMouseDragInput()
        {
            if (Input.GetMouseButtonDown(2) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
 
                float entry;
 
                if (plane.Raycast(ray, out entry))
                {
                    _dragStartPosition = ray.GetPoint(entry);
                }
            }
            if (Input.GetMouseButton(2) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
 
                float entry;
 
                if (plane.Raycast(ray, out entry))
                {
                    _dragCurrentPosition = ray.GetPoint(entry);
 
                    _newPosition = transform.position + _dragStartPosition - _dragCurrentPosition;
                }
            }
        }
    }
}