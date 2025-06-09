using System;
using System.Linq;
using _RTSGameProject.Logic.Common.AI;
using _RTSGameProject.Logic.Common.Construction.Model;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.View;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.LoadingAssets.Local;
using Cysharp.Threading.Tasks.Triggers;
using Zenject;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class EntryPointCore : IInitializable, ITickable, IDisposable
    {
        private readonly HouseBuilding[] _buildings;
        private readonly ActorsRepository _actorsRepository;
        private readonly SceneChanger _sceneChanger;
        private readonly SaveService _saveService;
        private readonly EnvironmentProvider _environmentProvider;
        private readonly ScoreGameUIProvider _scoreGameUIProvider;
        private readonly ProductionPanelProvider _productionPanelProvider;
        
        private ScoreGameUI _scoreGameUI;
        private ScoreGameController _scoreGameController;

        public EntryPointCore(SceneChanger sceneChanger,
                            ActorsRepository actorsRepository,
                            HouseBuilding[] buildings, 
                            SaveService saveService,
                            ScoreGameUIProvider scoreGameUIProvider,
                            ScoreGameController scoreGameController,
                            ProductionPanelProvider productionPanelProvider,
                            EnvironmentProvider environmentProvider)
        {
            _actorsRepository = actorsRepository;
            _buildings = buildings;
            _sceneChanger = sceneChanger;
            _saveService = saveService;
            _scoreGameUIProvider = scoreGameUIProvider;
            _scoreGameController = scoreGameController;
            _productionPanelProvider = productionPanelProvider;
            _environmentProvider = environmentProvider;
        }

        public async void Initialize()
        {
            await _environmentProvider.Load();
            await _productionPanelProvider.Load();
            InitializeAndSubscribeBuildings();
            if (_saveService.IsSaveExist())
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