using System;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Character.Model
{
    public class Health: MonoBehaviour
    {
        public event Action OnDie;

        public bool IsAlive;
        
        [field:SerializeField] public int Max { get; private set; }
        [field:SerializeField] public int Current { get; private set; }
        
        private void Awake()
        {
            Current = Max;
            IsAlive = true;
        }

        public void TakeDamage(int damage)
        {
            Current -= damage;
            if (Current <= 0)
            {
                IsAlive = false;
                OnDie?.Invoke();
            }
        }
    }
}