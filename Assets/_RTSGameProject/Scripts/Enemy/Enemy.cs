using System.Collections.Generic;
using RTS.Scripts;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private UnitMovement _unitMovement;
    
    [field: SerializeField] public List<GameObject> Positions { get; private set; }
    [field: SerializeField] public int CurrentPositionIndex { get; set; }

    private void Start()
    {
        _unitMovement = GetComponent<UnitMovement>();
    }

    public void Move(Vector3 point)
    {
        _unitMovement.Move(point);
    }
}
