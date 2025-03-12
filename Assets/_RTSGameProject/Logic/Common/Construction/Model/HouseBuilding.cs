using System.Collections.Generic;
using _RTSGameProject.Logic.Common.AI;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Services;
using UnityEngine;

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
        
        public void Construct(AiFactory aiFactory, PauseGame pauseGame, PanelController panelController)
        {
            _startSpawnPoint = SpawnPoints[0];
            _rallyPoint = _startSpawnPoint.position;
            _panelController = panelController;
            _spawner = new Spawner(aiFactory);
            _pauseGame = pauseGame;
            _pauseGame.OnPause += OnPaused;
        }
        
        private void Start()
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
            _panelController.Subscribe(this);
        }
        
        public void Unsubscribe()
        {
            _panelController.Unsubscribe(this);
        }
        
        private void OnPaused()
        {
            Unsubscribe();
        }
    }
}
