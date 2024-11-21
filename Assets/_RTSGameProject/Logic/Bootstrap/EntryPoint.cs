using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Camera;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Selection;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.StateMachineAI.Core;
using _RTSGameProject.Logic.StateMachineAI.Implementation;
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
        private StateMachineAiFactory _stateMachineAiFactory;
        private List<StateMachine> _stateMachines;
        private CameraController _cameraController;
        private UnitRepository _unitRepository;

        private void Awake()
        {
            _unitRepository = new UnitRepository(_unitListParent);
            _unitSelectionManager = new UnitSelectionManager(_groundMarker);
            _unitSelectionBox.Construct(_unitRepository, _unitSelectionManager);
            _generator = new BoxGenerator();
            _formationController = new FormationController(_generator);
            _inputCatchKeyClick = new InputCatchKeyClick(_camera);
            _stateMachineAiFactory = new StateMachineAiFactory();
            _stateMachines = new List<StateMachine>();
            _inputController = new InputController(_inputCatchKeyClick, _unitSelectionManager, 
                                                    _unitSelectionBox, _clickable, _ground, _formationController);

            for (int i = 0; i < _unitRepository.AllUnits.Count; i++)
            {
                _stateMachines.Add(_stateMachineAiFactory.Create(_unitRepository.AllUnits[i]));
            }
        }

        private void Update()
        {
            _inputCatchKeyClick.Update();
            foreach (StateMachine stateMachine in _stateMachines)
            {
                stateMachine.Update();
            }
        }

        private void OnDestroy()
        {
            _inputController.Unsubscribe();
        }
    }
}
