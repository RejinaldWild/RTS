using System;
using System.Linq;
using _RTSGameProject.Logic.Common.AI;
using _RTSGameProject.Logic.Common.Construction.Model;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.View;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.LoadingAssets.Local;
using Zenject;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class EntryPointCore : IInitializable, ITickable, IDisposable
    {
        private readonly HouseBuilding[] _buildings;
        private readonly ActorsRepository _actorsRepository;
        private readonly EnvironmentProvider _environmentProvider;
        private readonly ScoreGameUIProvider _scoreGameUIProvider;
        private readonly ProductionPanelProvider _productionPanelProvider;
        private readonly ScoreGameController _scoreGameController;
        private readonly ISaveService _saveService;
        
        private ScoreGameUI _scoreGameUI;

        public EntryPointCore(ActorsRepository actorsRepository,
                            HouseBuilding[] buildings,
                            ScoreGameUIProvider scoreGameUIProvider,
                            ScoreGameController scoreGameController,
                            ProductionPanelProvider productionPanelProvider,
                            EnvironmentProvider environmentProvider,
                            ISaveService saveService)
        {
            _actorsRepository = actorsRepository;
            _buildings = buildings;
            _scoreGameUIProvider = scoreGameUIProvider;
            _scoreGameController = scoreGameController;
            _productionPanelProvider = productionPanelProvider;
            _environmentProvider = environmentProvider;
            _saveService = saveService;
        }

        public async void Initialize()
        {
            await _environmentProvider.Load();
            await _productionPanelProvider.Load();
            InitializeAndSubscribeBuildings();
            if (await _saveService.IsSaveExist())
            {
                await _scoreGameController.InitializeLoadDataAsync();
                _scoreGameController.GetDataToShowScore(_scoreGameController.ScoreGameData);
            }
            else
            {
                _scoreGameController.InitializeScoreGameData();
                _scoreGameController.GetDataToShowScore(_scoreGameController.ScoreGameData);
            }
        }

        public void Tick()
        {
            foreach (IAiActor aiActor in _actorsRepository.All.ToArray())
            {
                aiActor.Update();
            }
        }
        
        public void Dispose()
        {
            _productionPanelProvider.Unload();
            _scoreGameUIProvider.Unload();
            _environmentProvider.Unload();
            UnsubscribeBuildings();
        }
        
        private void InitializeAndSubscribeBuildings()
        {
            foreach (HouseBuilding building in _buildings)
            {
                building.Initialize();
                building.Subscribe();
            }
        }
        
        private void UnsubscribeBuildings()
        {
            foreach (HouseBuilding building in _buildings)
            {
                building.Unsubscribe();
            }
        }
    }
}