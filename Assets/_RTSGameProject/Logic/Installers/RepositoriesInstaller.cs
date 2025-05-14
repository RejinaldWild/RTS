using _RTSGameProject.Logic.Common.Construction.Model;
using _RTSGameProject.Logic.Common.Selection;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.Common.View;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Installers
{
    public class RepositoriesInstaller : MonoInstaller
    {
        [SerializeField] private GroundMarker _groundMarker;
        [SerializeField] private WinLoseWindow _winLoseWindow;
        [SerializeField] private HouseBuilding[] _buildings;
        
        public override void InstallBindings()
        {
            BindPause();
            BindWinLoseWindow();
            BindSelection();
            BindUnits();
            BindActors();
            BindHealthBar();
            BindBuildings();
        }

        private void BindUnits()
        {
            Container
                .BindInterfacesAndSelfTo<UnitsRepository>()
                .AsSingle();
        }
        
        private void BindWinLoseWindow()
        {
            Container
                .Bind<WinLoseWindow>()
                .FromInstance(_winLoseWindow)
                .AsSingle();
        }
        
        private void BindPause()
        {
            Container
                .Bind<PauseGame>()
                .AsSingle();
        }
        
        private void BindBuildings()
        {
           Container
               .Bind<HouseBuilding[]>()
               .FromInstance(_buildings);
           Container
               .Bind<BuildingsRepository>()
               .AsSingle();
        }
        
        private void BindSelection()
        {
            Container
                .Bind<GroundMarker>()
                .FromInstance(_groundMarker)
                .AsSingle();
            Container
                .Bind<SelectionManager>()
                .AsSingle();
        }

        private void BindActors()
        {
            Container
                .Bind<ActorsRepository>()
                .AsSingle();
        }
        
        private void BindHealthBar()
        {
            Container
                .Bind<HealthBarRepository>()
                .AsSingle();
        }
    }
}