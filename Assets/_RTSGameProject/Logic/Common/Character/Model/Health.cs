using System;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Character.Model
{
    public class Health: MonoBehaviour
    {
        public int Current { get; private set; }
        public int Max { get; private set; }

        private void Awake()
        {
            Current = Max;
        }

        public void TakeDamage(int damage)
        {
            Current -= damage;
            if (Current <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}