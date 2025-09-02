using System.Collections.Generic;
using _RTSGameProject.Logic.Analytic;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Config;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.Common.Services.SoundFX;
using _RTSGameProject.Logic.StateMachine.Implementation;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Common.Construction.Model
{
    public class HouseBuilding : MonoBehaviour, ISelectable
    {
        [SerializeField] public List<Transform> SpawnPoints;
        [SerializeField] private Vector3 _enemySpawnPoint;
        
        private PanelController _panelController;
        private Vector3 _rallyPoint;
        private Spawner _spawner;
        private PauseGame _pauseGame;
        private IAudio _audioService;
        private IVFX _vfxService;
        
        [field: SerializeField] public int Team { get; set; }
        
        [Inject]
        public void Construct(IAnalyticService analyticService,
                                PanelController panelController, 
                                StateMachineAiFactory aiFactory,
                                PauseGame pauseGame,
                                IAudio audioService,
                                IVFX vfxService)
        {
            _rallyPoint = SpawnPoints[0].position;
            _panelController = panelController;
            _spawner = new Spawner(aiFactory, analyticService, vfxService);
            _pauseGame = pauseGame;
            _audioService = audioService;
            _vfxService = vfxService;
        }
        
        public void Initialize()
        {
            _panelController.ShowUIPanel(false);
            _enemySpawnPoint = new Vector3(34.5f, 0.5f, 21.5f);
        }

        public void SetRallyPoint(Vector3 rallyPoint)
        {
            _rallyPoint = rallyPoint;
        }

        public void ShowUIPanel(bool flagShow)
        {
            _panelController.ShowUIPanel(flagShow);
        }

        public void SpawnUnit()
        {
            _audioService.PlayRandomSoundFX(SoundType.CREATE);
            if (_rallyPoint != SpawnPoints[0].position)
            {
                _spawner.Spawn(Team, _rallyPoint);
                _spawner.Spawn(1, _enemySpawnPoint);
            }
            else
            {
                _spawner.Spawn(Team, SpawnPoints);
                _spawner.Spawn(1, _enemySpawnPoint);
            }
        }
        
        public void SpawnExpUnit()
        {
            _audioService.PlayRandomSoundFX(SoundType.CREATE);
            if (_rallyPoint != SpawnPoints[0].position)
            {
                _spawner.SpawnExpensiveUnit(Team, _rallyPoint);
            }
            else
            {
                _spawner.SpawnExpensiveUnit(Team, SpawnPoints[0].position);
            }
        }

        public void Subscribe()
        {
            _pauseGame.OnPause += OnPaused;
        }
        
        public void Unsubscribe()
        {
            _pauseGame.OnPause -= OnPaused;
        }
        
        public void SubscribeClickPanel()
        {
            _panelController.Subscribe(this);
        }
        
        public void UnsubscribeClickPanel()
        {
            _panelController.Unsubscribe(this);
        }
        
        private void OnPaused()
        {
            UnsubscribeClickPanel();
        }
    }
}
