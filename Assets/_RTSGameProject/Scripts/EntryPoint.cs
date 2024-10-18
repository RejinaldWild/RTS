using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemys;
    
    private EnemyController _enemyController;
    
    private void Awake()
    {
        _enemyController = new EnemyController(_enemys);
    }
    
    void Update()
    {
        _enemyController.FixedUpdate();
    }
}
