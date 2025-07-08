using System.Collections;
using _RTSGameProject.Logic.Common.Config;
using UniRx;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Character.Model
{
    public class Health: MonoBehaviour
    {
        public float Max;

        public IReadOnlyReactiveProperty<bool> IsAlive;
        public IReadOnlyReactiveProperty<float> Current => _current;
        public IReadOnlyReactiveProperty<Vector3> Position => _position;
        
        private ReactiveProperty<float> _current;
        [SerializeField] private ReactiveProperty<Vector3> _position;

        public void Construct(ParamConfig paramConfig)
        {
            Max = paramConfig.MaxHealth;
        }
        
        private void Awake()
        {
            _current = new ReactiveProperty<float>(Max);
            _position = new ReactiveProperty<Vector3>(transform.position);
            IsAlive = Current.Select(x => x > 0f).ToReadOnlyReactiveProperty();
        }

        public void TakeDamage(float damage)
        {
            _current.Value = Mathf.Max(_current.Value - damage, 0f);
        }
    }
}