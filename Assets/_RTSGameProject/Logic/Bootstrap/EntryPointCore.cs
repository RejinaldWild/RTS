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
        private readonly SceneChanger _sceneChanger;
        private readonly SaveScoreService _saveScoreService;
        private readonly EnvironmentProvider _environmentProvider;
        private readonly ScoreGameUIProvider _scoreGameUIProvider;
        
        private ScoreGameUI _scoreGameUI;
        private ScoreGameController _scoreGameController;

        public EntryPointCore(SceneChanger sceneChanger,
                            ActorsRepository actorsRepository,
                            HouseBuilding[] buildings, 
                            SaveScoreService saveScoreService,
                            ScoreGameUIProvider scoreGameUIProvider,
                            ScoreGameController scoreGameController,
                            EnvironmentProvider environmentProvider)
        {
            _actorsRepository = actorsRepository;
            _buildings = buildings;
            _sceneChanger = sceneChanger;
            _saveScoreService = saveScoreService;
            _scoreGameUIProvider = scoreGameUIProvider;
            _scoreGameController = scoreGameController;
            _environmentProvider = environmentProvider;
        }

        public async void Initialize()
        {
            await _environmentProvider.Load();
            _scoreGameController.Initialize();
            if (_saveScoreService.IsSaveExist())
            {
                await _sceneChanger.Initialize();
                _scoreGameController.LoadData(_sceneChanger.ScoreGameData);
            }
            else
            {
                _scoreGameController.LoadStartData();
            }
            Subscribe();
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
            _sceneChanger.Dispose();
            Unsubscribe();
            _scoreGameUIProvider.Unload();
            _environmentProvider.Unload();
        }
        
        private void Subscribe()
        {
            foreach (HouseBuilding building in _buildings)
            {
                building.Subscribe();
            }
        }
        
        private void Unsubscribe()
        {
            foreach (HouseBuilding building in _buildings)
            {
                building.Unsubscribe();
            }
        }
    }
}