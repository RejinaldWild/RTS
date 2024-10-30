using System.Collections.Generic;
using UnityEngine;

namespace RTS.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private List<Enemy> _enemies;
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _groundMarker;
        [SerializeField] private UnitSelectionBox _unitSelectionBox;
        [SerializeField] private LayerMask _clickable;
        [SerializeField] private LayerMask _ground;
        private UnitSelectionManager _unitSelectionManager;
        private FormationController _formationController;
        private InputController _inputController;
        private Patrolling _patrolling;
        private BoxGenerator _generator;

        private void Awake()
        {
            _unitSelectionManager = new UnitSelectionManager(_groundMarker);
            _unitSelectionBox.Construct(_unitSelectionManager);
            _generator = new BoxGenerator();
            _formationController = new FormationController(_generator);
            _inputController = new InputController(_camera, _unitSelectionManager, 
                                                    _unitSelectionBox, _clickable, _ground, _formationController);
            _patrolling = new Patrolling(_enemies);
        }

        private void Start()
        {
            _unitSelectionBox.Construct(_unitSelectionManager);
        }

        private void Update()
        {
            _inputController.Update();
        }

        private void FixedUpdate()
        {
            _patrolling.FixedUpdate();
        }
    }
}
