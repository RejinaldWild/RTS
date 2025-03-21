using System;
using UniRx;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Character.Model
{
    public class Health: MonoBehaviour
    {
        public float Max;

        public IReadOnlyReactiveProperty<bool> IsAlive;
        public IReadOnlyReactiveProperty<float> Current => _current;
        private ReactiveProperty<float> _current;

        private void Awake()
        {
            _current = new ReactiveProperty<float>(Max);
            IsAlive = Current.Select(x => x > 0f).ToReadOnlyReactiveProperty();
        }

        public void TakeDamage(float damage)
        {
            _current.Value = Mathf.Max(_current.Value - damage, 0f);
        }
    }
}