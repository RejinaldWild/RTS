using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance { get; set; }
    
    public event Action<Unit> OnSelectedUnits;
    public event Action<Unit> OnDeselectedUnits;
    
    public List<Unit> allUnits = new List<Unit>();
    public List<Unit> selectedUnits = new List<Unit>();
    public LayerMask clickable;
    public LayerMask ground;
    public GameObject groundMarker;
    
    private Camera _camera;
    private FormationController _formationController;
    
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
        foreach (var unit in allUnits)
        {
            unit.OnUnitAwaked += AddUnit;
        }
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
                    hit.collider.TryGetComponent(out Unit unit);
                    MultiSelect(unit);
                }
                else
                {
                    hit.collider.TryGetComponent(out Unit unit);
                    SelectByClicking(unit);
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

    private void OnDestroy()
    {
        foreach (var unit in allUnits)
        {
            unit.OnUnitAwaked -= AddUnit;
        }
    }
    
    public void DragSelect(Unit unit)
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
            OnDeselectedUnits?.Invoke(unit);
        }
        groundMarker.SetActive(false);
        selectedUnits.Clear();
    }
    
    private void AddUnit(Unit unit)
    {
        allUnits.Add(unit);
    }
    
    private void SelectUnit(Unit unit, bool isSelected)
    {
        TriggerSectionIndicator(unit, isSelected);
        EnableUnitMovement(unit, isSelected);
        if (isSelected)
        {
            OnSelectedUnits?.Invoke(unit);
        }
        else
        {
            OnDeselectedUnits?.Invoke(unit);
        }
    }
    
    private void MultiSelect(Unit unit)
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

    private void SelectByClicking(Unit unit)
    {
        DeselectAll();
        selectedUnits.Add(unit);
        SelectUnit(unit, true);
    }

    private void EnableUnitMovement(Unit unit, bool isMove)
    {
        unit.GetComponent<UnitMovement>().enabled = isMove;
    }

    private void TriggerSectionIndicator(Unit unit, bool isVisible)
    {
        unit.transform.GetChild(0).gameObject.SetActive(isVisible);
    }
}
