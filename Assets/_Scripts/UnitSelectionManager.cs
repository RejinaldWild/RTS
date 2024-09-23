using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance { get; set; }
    public List<GameObject> allUnits = new List<GameObject>();
    public List<GameObject> selectedUnits = new List<GameObject>();
    public LayerMask clickable;
    public LayerMask ground;
    public GameObject groundMarker;
    
    private Camera _camera;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _camera = Camera.main;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    MultiSelect(hit.collider.gameObject);
                }
                else
                {
                    SelectByClicking(hit.collider.gameObject);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift) == false)
                {
                    DeselectAll();
                }
            }
        }
        
        if (Input.GetMouseButtonDown(1) && selectedUnits.Count > 0)
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                groundMarker.transform.position = hit.point;
                groundMarker.SetActive(false);
                groundMarker.SetActive(true);
            }
        }
    }

    public void DragSelect(GameObject unit)
    {
        if (selectedUnits.Contains(unit) == false)
        {
            selectedUnits.Add(unit);
            SelectUnit(unit, true);
        }
    }
    
    public void DeselectAll()
    {
        foreach (var unit in selectedUnits)
        {
            SelectUnit(unit, false);
        }
        
        groundMarker.SetActive(false);
        selectedUnits.Clear();
    }
    
    private void MultiSelect(GameObject unit)
    {
        if (selectedUnits.Contains(unit) == false)
        {
            selectedUnits.Add((unit));
            SelectUnit(unit, true);
        }
        else
        {
            selectedUnits.Remove(unit);
            SelectUnit(unit, false);
        }
    }

    private void SelectByClicking(GameObject unit)
    {
        DeselectAll();
        selectedUnits.Add(unit);
        SelectUnit(unit, true);
    }

    private void EnableUnitMovement(GameObject unit, bool isMove)
    {
        unit.GetComponent<UnitMovement>().enabled = isMove;
    }

    private void TriggerSectionIndicator(GameObject unit, bool isVisible)
    {
        unit.transform.GetChild(0).gameObject.SetActive(isVisible);
    }

    private void SelectUnit(GameObject unit, bool IsSelected)
    {
        TriggerSectionIndicator(unit, IsSelected);
        EnableUnitMovement(unit, IsSelected);
    }

}
