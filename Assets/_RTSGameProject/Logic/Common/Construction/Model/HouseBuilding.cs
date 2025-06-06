using System.Collections.Generic;
using _RTSGameProject.Logic.Common.AI;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Construction.View;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.LoadingAssets.Local;
using _RTSGameProject.Logic.StateMachine.Implementation;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Common.Construction.Model
{
    public class HouseBuilding : MonoBehaviour, ISelectable
    {
        [SerializeField] public List<Transform> SpawnPoints;
        
        private PanelController _panelController;
        private Vector3 _rallyPoint;
        private Spawner _spawner;
        private Transform _startSpawnPoint;
        private PauseGame _pauseGame;
        
        [field: SerializeField] public int Team { get; set; }
        
        [Inject]
        public void Construct(PanelController panelController, StateMachineAiFactory aiFactory, PauseGame pauseGame)
        {
            _startSpawnPoint = SpawnPoints[0];
            _rallyPoint = _startSpawnPoint.position;
            _panelController = panelController;
            _spawner = new Spawner(aiFactory);
            _pauseGame = pauseGame;
        }
        
        public void Initialize()
        {
            _panelController.ShowUIPanel(false);
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
            if (_rallyPoint != _startSpawnPoint.position)
            {
                _spawner.Spawn(Team, _rallyPoint);
                _spawner.Spawn(1, new Vector3(34.8f,0.5f,20.75f));
            }
            else
            {
                _spawner.Spawn(Team, _startSpawnPoint.position);
                _spawner.Spawn(1, new Vector3(34.8f,0.5f,20.75f));
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
