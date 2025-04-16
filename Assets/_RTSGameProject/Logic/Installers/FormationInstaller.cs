using _RTSGameProject.Logic.Common.Services;
using Zenject;

namespace _RTSGameProject.Logic.Installers
{
    public class FormationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<FormationController>().AsCached();
            BoxFormationBind();
        }

        private void BoxFormationBind()
        {
            Container.Bind<IFormationPositionGenerator>().To<BoxGenerator>().AsCached();
        }
    }
}