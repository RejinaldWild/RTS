using _RTSGameProject.Logic.Common.AI;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Services;
using UnityEngine;
using UnityEngine.UI;

namespace _RTSGameProject.Logic.Common.Building
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Building : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Unit[] _unitsPrefabs;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private Button[] _uiButtons;
        private Transform _startSpawnPoint;
        private Transform _spawnPoint;
        private Spawner _spawner;
        private AiFactory _aiFactory;
        private Vector3 _rallyPoint;
        
        [field: SerializeField] public int Team { get; set; }
        
        public void Construct(AiFactory aiFactory)
        {
            _aiFactory = aiFactory;
            _spawner = new Spawner(_aiFactory);
        }
        
        private void Start()
        {
            ToggleUI(false);
        }
        
        public void SetRallyPoint(Vector3 rallyPoint)
        {
            _rallyPoint = rallyPoint;
        }

        public void ToggleUI(bool isShow)
        {
            _canvasGroup.alpha = isShow ? 1 : 0;
            _canvasGroup.interactable = isShow;
            _canvasGroup.blocksRaycasts = isShow;
        }

        public void SpawnUnit()
        {
            _spawner.Spawn(Team);
        }
    }
}
