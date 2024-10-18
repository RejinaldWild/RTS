using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public List<GameObject> Positions { get; private set; }

    private int _currentPosition;

    private void Start()
    {
        _currentPosition = 0;
    }

    public void Move()
    {
        if(Positions.Count == 0) return;

        Vector3 targetPosition = Positions[_currentPosition].transform.position;
        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
        
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            _currentPosition++;
            if (_currentPosition >= Positions.Count)
            {
                _currentPosition = 0;
            }
        }
    }
}
