using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnitSelectionManager.Instance.allUnits.Add(gameObject);
    }

    private void OnDestroy()
    {
        UnitSelectionManager.Instance.allUnits.Remove(gameObject);
    }
}
