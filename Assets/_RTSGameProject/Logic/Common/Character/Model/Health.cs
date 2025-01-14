using System;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Character.Model
{
    public class Health: MonoBehaviour
    {
        public event Action OnDie;
        
        private Unit _unit;
        
        [field:SerializeField] public int Max { get; private set; }
        [field:SerializeField] public int Current { get; private set; }
        
        private void Awake()
        {
            Current = Max;
            _unit = GetComponent<Unit>();
        }

        public void TakeDamage(int damage)
        {
            Current -= damage;
            if (Current <= 0)
            {
                OnDie?.Invoke();
            }
        }
    }
}