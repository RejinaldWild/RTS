using System;
using _RTSGameProject.Logic.Common.Config;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Random = UnityEngine.Random;

namespace _RTSGameProject.Logic.Common.Services.SoundFX
{
    [ExecuteInEditMode]
    public class AudioService : MonoBehaviour, IAudio
    {
        [SerializeField] private AudioLibrary[] _audioLibrary;
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private int _poolSize = 30;
        
        private ObjectPool<AudioSource> _audioSourcePool;
        private bool _isMusicPlaying = false;

        [Inject]
        public void Construct()
        {
            _audioSourcePool = new ObjectPool<AudioSource>(
                createFunc: () => CreateNewSoundAudioSource(),
                actionOnGet: source => source.gameObject.SetActive(true),
                actionOnRelease: source => source.gameObject.SetActive(false),
                actionOnDestroy: source => Destroy(source.gameObject),
                maxSize: _poolSize);
            _musicAudioSource = GetComponentsInChildren<AudioSource>()[0];
        }


#if UNITY_EDITOR
        public void OnEnable()
        {
            string[] audioClipNames = Enum.GetNames(typeof(SoundType));
            Array.Resize(ref _audioLibrary, audioClipNames.Length);
            for (int i = 0; i < _audioLibrary.Length; i++)
            {
                _audioLibrary[i].Name = audioClipNames[i];
            }
        }
#endif
        private AudioSource CreateNewSoundAudioSource()
        {
            var source = new GameObject("AudioSource").AddComponent<AudioSource>();
            source.transform.SetParent(transform);
            source.playOnAwake = false;
            source.spatialBlend = 1f;
            return source;
        }

        public void StartMusicPlaylist()
        {
            _isMusicPlaying = true;
            PlayRandomMusicFX();
        }

        public void StopMusicFX()
        {
            _isMusicPlaying = false;
            if (_musicAudioSource != null)
            {
                CancelInvoke(nameof(PlayRandomMusicFX));
                _musicAudioSource.Stop();
            }
        }
        
        public void PlayRandomSoundFX(SoundType soundFX)
        {
            AudioClip[] audioClips = _audioLibrary[(int)soundFX].AudioClips;
            AudioClip clip = audioClips[Random.Range(0, audioClips.Length)];
            AudioSource source = _audioSourcePool.Get();
            source.clip = clip;
            source.Play();
            ReleaseSource(source, source.clip.length).Forget();
        }
        
        private void PlayRandomMusicFX()
        {
            if (!_isMusicPlaying || _audioLibrary.Length == 0) return;
            
            AudioClip[] audioClips = _audioLibrary[(int)SoundType.BACKGROUND].AudioClips;
            AudioClip clip = audioClips[Random.Range(0, audioClips.Length)];
            _musicAudioSource.clip = clip;
            _musicAudioSource.Play();
            Invoke(nameof(PlayRandomMusicFX), _musicAudioSource.clip.length);
        }

        private async UniTaskVoid ReleaseSource(AudioSource source, float duration)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            source.Stop();
            _audioSourcePool.Release(source);
        }
    }
}