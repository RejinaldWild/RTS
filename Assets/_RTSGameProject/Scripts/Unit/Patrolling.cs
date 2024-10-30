using System.Collections.Generic;
using UnityEngine;

namespace RTS.Scripts
{
    internal class Patrolling
    {
        private List<Enemy> _enemies;
        private Vector3 delta = new(0.1f, 0, 0.1f);

        public Patrolling(List<Enemy> enemies)
        {
            _enemies = enemies;
        }
        
        private void Move()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                Vector3 currentPosition = _enemies[i].transform.position;
                Vector3 targetPosition = _enemies[i].Positions[_enemies[i].CurrentPositionIndex].transform.position;
        
                if (Vector3.Distance(currentPosition, targetPosition) > delta.magnitude)
                {
                    _enemies[i].Move(targetPosition);
                }
                else
                {
                    _enemies[i].CurrentPositionIndex++;
                    
                    if (_enemies[i].CurrentPositionIndex >= _enemies[i].Positions.Count)
                    {
                        _enemies[i].CurrentPositionIndex = 0;
                    }
                }
            }
        }

        public void FixedUpdate()
        {
            Move();
        }
    }
}