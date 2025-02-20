using System.Collections.Generic;
using _RTSGameProject.Logic.Common.AI;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Construction.View;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.Common.View;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Construction.Model
{
    public class HouseBuilding : MonoBehaviour, ISelectable
    {
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private BuildPanel _buildPanel;
        
        private TeamColor _teamColor;
        private Spawner _spawner;
        private AiFactory _aiFactory;
        private Transform _startSpawnPoint;
        private Vector3 _rallyPoint;

        [field: SerializeField] public int Team { get; set; }
        
        public void Construct(AiFactory aiFactory)
        {
            _startSpawnPoint = _spawnPoints[0];
            _rallyPoint = _startSpawnPoint.position;
            _aiFactory = aiFactory;
            _spawner = new Spawner(_aiFactory);
        }
        
        private void Start()
        {
            _buildPanel.ToggleUI(false);
        }

        public void SetRallyPoint(Vector3 rallyPoint)
        {
            _rallyPoint = rallyPoint;
        }

        public void ShowUIPanel(bool show)
        {
            _buildPanel.ToggleUI(show);
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
            _buildPanel.OnClick += SpawnUnit;
        }
        
        public void Unsubscribe()
        {
            _buildPanel.OnClick -= SpawnUnit;
        }
    }
}
