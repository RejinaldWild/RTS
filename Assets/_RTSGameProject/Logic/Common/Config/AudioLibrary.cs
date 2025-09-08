using System;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Config
{
    [Serializable]
    public struct AudioLibrary
    {
        public AudioClip[] AudioClips{ get => _sounds; }
        [HideInInspector] public string Name;
        [SerializeField] private AudioClip[] _sounds;
    }
}