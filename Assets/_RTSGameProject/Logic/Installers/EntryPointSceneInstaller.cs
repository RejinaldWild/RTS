using _RTSGameProject.Logic.Bootstrap;
using _RTSGameProject.Logic.Common.Config;
using _RTSGameProject.Logic.Common.Services.VFX;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Installers
{
    public class EntryPointSceneInstaller : MonoInstaller
    {
        [SerializeField] private VFXService _vfxService;
        
        public override void InstallBindings()
        {
            BindEntryPointCore();
            BindVFXService();
        }

        private void BindEntryPointCore()
        {
            Container
                .BindInterfacesAndSelfTo<EntryPointCore>()
                .AsSingle();
        }

        private void BindVFXService()
        {
            Container
                .Bind<IVFX>()
                .To<VFXService>()
                .FromComponentInNewPrefab(_vfxService)
                .AsSingle();
        }
    }
}