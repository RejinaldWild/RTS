using System;
using System.Linq;
using _RTSGameProject.Logic.Common.AI;
using _RTSGameProject.Logic.Common.Config;
using _RTSGameProject.Logic.Common.Construction.Model;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.View;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.Common.Services.SoundFX;
using _RTSGameProject.Logic.LoadingAssets.Local;
using _RTSGameProject.Logic.LoadingAssets.Remote;
using Zenject;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class EntryPointCore : IInitializable, ITickable, IDisposable
    {
        private readonly ActorsRepository _actorsRepository;
        private readonly EnvironmentProvider _environmentProvider;
        private readonly ScoreGameUIProvider _scoreGameUIProvider;
        private readonly ProductionPanelProvider _productionPanelProvider;
        private readonly BuildingProvider _buildingProvider;
        private readonly BuildingEnemyProvider _buildingEnemyProvider; 
        private readonly UnitExpProvider _unitExpProvider;
        private readonly ScoreGameController _scoreGameController;
        private readonly ISaveService _saveService;
        private readonly UnitsFactory _unitsFactory;
        private readonly BuildingsRepository _buildingsRepository;
        private readonly IAudio _audioService;
        private readonly IVFX _vfxService;
        
        private EnemyBuildingsPosConfig _enemyBuildingsPosConfig;
        private HouseBuilding[] _buildings;
        private HouseBuilding[] _buildingsEnemy;
        private ScoreGameUI _scoreGameUI;

        public EntryPointCore(ActorsRepository actorsRepository,
                            ScoreGameUIProvider scoreGameUIProvider,
                            ScoreGameController scoreGameController,
                            ProductionPanelProvider productionPanelProvider,
                            EnvironmentProvider environmentProvider,
                            BuildingProvider buildingProvider,
                            BuildingEnemyProvider buildingEnemyProvider,
                            BuildingsRepository buildingsRepository,
                            UnitExpProvider unitExpProvider,
                            UnitsFactory unitsFactory,
                            EnemyBuildingsPosConfig enemyBuildingsPosConfig,
                            ISaveService saveService,
                            IAudio audioService,
                            IVFX vfxService)
        {
            _actorsRepository = actorsRepository;
            _scoreGameUIProvider = scoreGameUIProvider;
            _scoreGameController = scoreGameController;
            _productionPanelProvider = productionPanelProvider;
            _environmentProvider = environmentProvider;
            _buildingProvider = buildingProvider;
            _buildingEnemyProvider = buildingEnemyProvider;
            _buildingsRepository = buildingsRepository;
            _unitExpProvider = unitExpProvider;
            _saveService = saveService;
            _unitsFactory = unitsFactory;
            _audioService = audioService;
            _vfxService = vfxService;
            _enemyBuildingsPosConfig = enemyBuildingsPosConfig;
        }

        public async void Initialize()
        {
            await _saveService.Initialize();
            await _environmentProvider.Load();
            await _productionPanelProvider.Load();
            await _unitsFactory.Initialize();
            
            _buildings = await _buildingProvider.Load();
            
            _buildingEnemyProvider.Initialize(_enemyBuildingsPosConfig);
            _buildingsEnemy = await _buildingEnemyProvider.Load();

            foreach (HouseBuilding building in _buildingsEnemy)
            {
                building.Initialize();
                _buildingsRepository.Register(building);
                building.Subscribe();
            }

            foreach (HouseBuilding building in _buildings)
            {
                building.Initialize();
                _buildingsRepository.Register(building);
                building.Subscribe();
            }
            
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

            _audioService.StartMusicPlaylist();
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
            _buildingProvider.Unload();
            _buildingEnemyProvider.Unload();
            _unitExpProvider.Unload();
            UnsubscribeBuildings();
            _audioService.StopMusicFX();
        }
        
        private void UnsubscribeBuildings()
        {
            foreach (HouseBuilding building in _buildings)
            {
                _buildingsRepository.Unregister(building);
                building.Unsubscribe();
            }

            foreach (HouseBuilding building in _buildingsEnemy)
            {
                _buildingsRepository.Unregister(building);
                building.Unsubscribe();
            }
        }
    }
}