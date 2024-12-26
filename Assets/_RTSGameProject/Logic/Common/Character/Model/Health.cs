using UnityEngine;

namespace _RTSGameProject.Logic.Common.Character.Model
{
    public class Health: MonoBehaviour
    {
        [field:SerializeField] public int Max { get; private set; }
        [field:SerializeField] public int Current { get; private set; }
        
        private void Awake()
        {
            Current = Max;
        }

        public void TakeDamage(int damage)
        {
            Current -= damage;
        }
    }
}