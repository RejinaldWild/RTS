using System;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Config
{
    [Serializable]
    public struct VFXLibrary
    {
        public ParticleSystem[] VFXAssets{ get => _vfxAssets; }
        [HideInInspector] public string Name;
        [SerializeField] private ParticleSystem[] _vfxAssets;
    }
}