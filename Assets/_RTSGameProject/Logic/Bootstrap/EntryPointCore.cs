using System.Linq;
using _RTSGameProject.Logic.Common.AI;
using _RTSGameProject.Logic.Common.Construction.Model;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Services;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class EntryPointCore : MonoBehaviour
    {
        private HouseBuilding[] _buildings;
        private ActorsRepository _actorsRepository;
        private SceneChanger _sceneChanger;
        private ScoreGameController _scoreGameController;
        private IInstantiator _diContainer;
        private SaveSystem _saveSystem;

        [Inject]
        public void Construct(SceneChanger sceneChanger,
            ScoreGameController scoreGameController,
            ActorsRepository actorsRepository,
            HouseBuilding[] buildings, SaveSystem saveSystem)
        {
            _scoreGameController = scoreGameController;
            _actorsRepository = actorsRepository;
            _buildings = buildings;
            _saveSystem = saveSystem;
            _sceneChanger = sceneChanger;
        }

        private void Awake()
        {
            Subscribe();
            if (_saveSystem.IsSaveExist<ScoreGameData>())
            {
                _scoreGameController.LoadData();
            }
            else
            {
                _sceneChanger.LoadStartData();
            }
        }

        private void Update()
        {
            foreach (IAiActor aiActor in _actorsRepository.All.ToArray())
            {
                aiActor.Update();
            }
        }

        private void OnDestroy()
        {
            Unsubscribe();
            StopAllCoroutines();
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