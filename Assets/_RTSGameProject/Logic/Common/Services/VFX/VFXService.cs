using System;
using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Config;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Random = UnityEngine.Random;

namespace _RTSGameProject.Logic.Common.Services.VFX
{
    [ExecuteInEditMode]
    public class VFXService : MonoBehaviour, IVFX
    {
        [SerializeField] private VFXLibrary[] _vFXLibrarys;
        [SerializeField] private int _poolSize = 30;
        
        private Dictionary<ParticleSystem,ObjectPool<ParticleSystem>> _vfxPools;
        private Dictionary<ParticleSystem, ParticleSystem> _instanceToPrefabMap;
        
        [Inject]
        public void Construct()
        {
            _vfxPools = new Dictionary<ParticleSystem, ObjectPool<ParticleSystem>>();
            _instanceToPrefabMap = new Dictionary<ParticleSystem, ParticleSystem>();

            foreach (VFXLibrary vfxLibrary in _vFXLibrarys)
            {
                foreach (var effect in vfxLibrary.VFXAssets)
                {
                    ObjectPool<ParticleSystem> pool = new ObjectPool<ParticleSystem>(
                        createFunc: () => CreateNewVFX(effect),
                        actionOnGet: source => source.gameObject.SetActive(true),
                        actionOnRelease: source =>
                        {
                            source.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                            source.gameObject.SetActive(false);
                            source.transform.SetParent(transform);
                        },
                        actionOnDestroy: source => Destroy(source.gameObject),
                        maxSize: _poolSize);
                    
                    _vfxPools.Add(effect, pool);
                }
            }
        }

#if UNITY_EDITOR
        public void OnEnable()
        {
            string[] vfxAssetNames = Enum.GetNames(typeof(VFXType));
            Array.Resize(ref _vFXLibrarys, vfxAssetNames.Length);
            for (int i = 0; i < _vFXLibrarys.Length; i++)
            {
                _vFXLibrarys[i].Name = vfxAssetNames[i];
            }
        }
#endif

        public void ShowEffect(VFXType type, Vector3 position)
        {
            ParticleSystem[] effects = _vFXLibrarys[(int)type].VFXAssets;
            ParticleSystem effect = effects[Random.Range(0, effects.Length)];

            if (_vfxPools.TryGetValue(effect, out ObjectPool<ParticleSystem> pool))
            {
                ParticleSystem effectInstance = pool.Get();
                effectInstance.transform.position = position;
                effectInstance.Play(true);
                
                ReturnToPoolAfterFinish(effectInstance, pool, effectInstance.main.duration).Forget();
            }
        }

        public void ShowEffect(VFXType type, Transform parent)
        {
            ParticleSystem[] effects = _vFXLibrarys[(int)type].VFXAssets;
            ParticleSystem effect = effects[Random.Range(0, effects.Length)];

            if (_vfxPools.TryGetValue(effect, out ObjectPool<ParticleSystem> pool))
            {
                ParticleSystem effectInstance = pool.Get();
                effectInstance.transform.SetParent(parent);
                effectInstance.transform.localPosition = Vector3.zero;
                effectInstance.transform.rotation = Quaternion.identity;
                effectInstance.Play(true);
                
                ReturnToPoolAfterFinish(effectInstance, pool, effectInstance.main.duration).Forget();
            }
        }

        private async UniTaskVoid ReturnToPoolAfterFinish(ParticleSystem effect, ObjectPool<ParticleSystem> pool, float duration)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            if (effect != null)
            {
                pool.Release(effect);
            }
        }

        public void StopShowing(ParticleSystem effect)
        {
            if (effect != null && _instanceToPrefabMap.TryGetValue(effect, out ParticleSystem prefab))
            {
                if (_vfxPools.TryGetValue(prefab, out ObjectPool<ParticleSystem> pool))
                {
                    pool.Release(effect);
                }
            }
        }
        
        private ParticleSystem CreateNewVFX(ParticleSystem effect)
        {
            ParticleSystem instance = Instantiate(effect, transform);
            instance.gameObject.SetActive(false);
            _instanceToPrefabMap.Add(instance,effect);
            return instance;
        }
    }
}