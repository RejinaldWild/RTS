using System.Collections.Generic;
using System.Linq;
using _RTSGameProject.Logic.Common.AI;
using _RTSGameProject.Logic.Common.Camera;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Selection;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.StateMachine.Core;
using _RTSGameProject.Logic.StateMachine.Implementation;
using UnityEngine;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Transform _unitListParent;
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _groundMarker;
        [SerializeField] private UnitSelectionBox _unitSelectionBox;
        [SerializeField] private LayerMask _clickable;
        [SerializeField] private LayerMask _ground;
        
        private UnitSelectionManager _unitSelectionManager;
        private FormationController _formationController;
        private InputController _inputController;
        private InputCatchKeyClick _inputCatchKeyClick;
        private PatrollMovement _patrollMovement;
        private BoxGenerator _generator;
        private ActorsRepository _actorsRepository;
        private StateMachineAiFactory _stateMachineAiFactory;
        private UnitsFactory _unitsFactory;
        private List<StateMachineActor> _stateMachines;
        private CameraController _cameraController;
        private UnitsRepository _unitsRepository;
        private Health _health;
        private AiFactory _aiFactory;
        private Spawner _spawner;

        private void Awake()
        {
            _unitsRepository = new UnitsRepository();
            _unitSelectionManager = new UnitSelectionManager(_groundMarker);
            _unitSelectionBox.Construct(_unitsRepository, _unitSelectionManager);
            _generator = new BoxGenerator();
            _formationController = new FormationController(_generator);
            _inputCatchKeyClick = new InputCatchKeyClick(_camera);
            _actorsRepository = new ActorsRepository();
            _unitsFactory = new UnitsFactory(_unitsRepository);
            _aiFactory = new StateMachineAiFactory(_unitsRepository, _actorsRepository, _unitsFactory);
            _spawner = new Spawner(_aiFactory);
            _stateMachines = new List<StateMachineActor>();
            _inputController = new InputController(_inputCatchKeyClick, _unitSelectionManager, 
                                                    _unitSelectionBox, _clickable, _ground, _formationController, _spawner);
        }

        private void Update()
        {
            _inputCatchKeyClick.Update();
            foreach (IAiActor aiActor in _actorsRepository.All.ToArray())
            {
                aiActor.Update();
            }
        }

        private void OnDestroy()
        {
            _inputController.Unsubscribe();
        }
    }
}
