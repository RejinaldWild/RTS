using _RTSGameProject.Logic.Common.Services;
using Zenject;

namespace _RTSGameProject.Logic.Installers
{
    public class SceneManagmentInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneChanger();
        }

        private void BindSceneChanger()
        {
            Container
                .Bind<SceneChanger>()
                .AsSingle();
        }
    }
}