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
        private readonly ScoreGameController _scoreGameController;
        private readonly SaveScoreService _saveScoreService;
        private readonly ScoreGameUIProvider _scoreGameUIProvider;
        
        private IInstantiator _diContainer;
        private ScoreGameUI _scoreGameUI;

        public EntryPointCore(SceneChanger sceneChanger,
            ScoreGameUI scoreGameUI,
            ScoreGameController scoreGameController,
            ActorsRepository actorsRepository,
            HouseBuilding[] buildings, SaveScoreService saveScoreService,
            ILocalAssetLoader localAssetLoader)
        {
            _scoreGameController = scoreGameController;
            _actorsRepository = actorsRepository;
            _buildings = buildings;
            _saveScoreService = saveScoreService;
            _sceneChanger = sceneChanger;
            _scoreGameUI = scoreGameUI;
            _scoreGameUIProvider = localAssetLoader as ScoreGameUIProvider;
        }

        public async void Initialize()
        {
            Subscribe();
            if (_saveScoreService.IsSaveExist())
            {
                _scoreGameUI = await _scoreGameUIProvider.Load();
                await _scoreGameController.LoadData(_scoreGameUI);
                //_scoreGameUI.ScoreGameData = await _saveScoreService.LoadAsync();
            }
            else
            {
                await _sceneChanger.LoadStartData();
                await _scoreGameUIProvider.Load();
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
            _scoreGameUIProvider.Unload();
            Unsubscribe();
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