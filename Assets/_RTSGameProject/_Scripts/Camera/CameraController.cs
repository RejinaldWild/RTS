using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [Header("General")] 
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform followTransform;
    public float panSpeed = 20f;
    private Vector3 newPosition;
    private Vector3 dragStartPosition;
    private Vector3 dragCurrentPosition;
 
    [Header("Optional Functionality")]
    [SerializeField] private bool moveWithKeyboad;
    [SerializeField] private bool moveWithEdgeScrolling;
    [SerializeField] private bool moveWithMouseDrag;
 
    [Header("Keyboard Movement")]
    [SerializeField] private float fastSpeed = 0.05f;
    [SerializeField] private float normalSpeed = 0.01f;
 
    [Header("Edge Scrolling Movement")]
    [SerializeField] private float edgeSize = 50f;
    public Texture2D cursorArrowUp;
    public Texture2D cursorArrowDown;
    public Texture2D cursorArrowLeft;
    public Texture2D cursorArrowRight;
    
    private float movementSpeed;
    private bool isCursorSet = false;
 
    CursorArrow currentCursor = CursorArrow.DEFAULT;
    enum CursorArrow
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        DEFAULT
    }
 
    private void Start()
    {
        newPosition = transform.position;
        movementSpeed = normalSpeed;
    }
 
    private void Update()
    {
        if (followTransform != null)
        {
            transform.position = followTransform.position;
        }
        else
        {
            HandleCameraMovement();
        }
 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            followTransform = null;
        }
    }
 
    private void HandleCameraMovement()
    {
        if (moveWithMouseDrag)
        {
            HandleMouseDragInput();
        }
 
        if (moveWithKeyboad)
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                movementSpeed = fastSpeed;
            }
            else
            {
                movementSpeed = normalSpeed;
            }
 
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                newPosition += (transform.forward * movementSpeed);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                newPosition += (transform.forward * -movementSpeed);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                newPosition += (transform.right * movementSpeed);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                newPosition += (transform.right * -movementSpeed);
            }
        }
        
        if (moveWithEdgeScrolling)
        {
            if (Input.mousePosition.x > Screen.width - edgeSize)
            {
                newPosition += (transform.right * movementSpeed);
                ChangeCursor(CursorArrow.RIGHT);
                isCursorSet = true;
            }
            
            else if (Input.mousePosition.x < edgeSize)
            {
                newPosition += (transform.right * -movementSpeed);
                ChangeCursor(CursorArrow.LEFT);
                isCursorSet = true;
            }
            
            else if (Input.mousePosition.y > Screen.height - edgeSize)
            {
                newPosition += (transform.forward * movementSpeed);
                ChangeCursor(CursorArrow.UP);
                isCursorSet = true;
            }
            
            else if (Input.mousePosition.y < edgeSize)
            {
                newPosition += (transform.forward * -movementSpeed);
                ChangeCursor(CursorArrow.DOWN);
                isCursorSet = true;
            }
            else
            {
                if (isCursorSet)
                {
                    ChangeCursor(CursorArrow.DEFAULT);
                    isCursorSet = false;
                }
            }
        }

        transform.position = newPosition;
        //Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementSensitivity);
 
        Cursor.lockState = CursorLockMode.Confined;
    }
 
    private void ChangeCursor(CursorArrow newCursor)
    {
        if (currentCursor != newCursor)
        {
            switch (newCursor)
            {
                case CursorArrow.UP:
                    Cursor.SetCursor(cursorArrowUp, Vector2.zero, CursorMode.Auto);
                    break;
                case CursorArrow.DOWN:
                    Cursor.SetCursor(cursorArrowDown, new Vector2(cursorArrowDown.width, cursorArrowDown.height), CursorMode.Auto);
                    break;
                case CursorArrow.LEFT:
                    Cursor.SetCursor(cursorArrowLeft, Vector2.zero, CursorMode.Auto);
                    break;
                case CursorArrow.RIGHT:
                    Cursor.SetCursor(cursorArrowRight, new Vector2(cursorArrowRight.width, cursorArrowRight.height), CursorMode.Auto);
                    break;
                case CursorArrow.DEFAULT:
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    break;
            }
 
            currentCursor = newCursor;
        }
    }
    
    private void HandleMouseDragInput()
    {
        if (Input.GetMouseButtonDown(2) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
 
            float entry;
 
            if (plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(2) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
 
            float entry;
 
            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);
 
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }
    }
}