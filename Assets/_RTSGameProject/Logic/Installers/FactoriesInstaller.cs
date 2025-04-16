using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.Common.View;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Installers
{
    public class FactoriesInstaller : MonoInstaller
    {
        [SerializeField] private Unit _unit;
        [SerializeField] private HealthView _healthView;
        
        public override void InstallBindings()
        {
            BindUnitFactory();
        }

        private void BindUnitFactory()
        {
            Container.BindFactory<HealthView, HealthBarFactory>().FromComponentInNewPrefab(_healthView);
            Container.BindFactory<Unit, HealthView, UnitsFactory>().FromComponentInNewPrefab(_unit);
        }
    }
}