using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Scripts
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
        private InputManager _inputManager;
        private Patrolling _patrolling;
        private BoxGenerator _generator;
        private List<Enemy> _enemies;

        private void Awake()
        {
            _unitSelectionManager = new UnitSelectionManager(_unitListParent,_groundMarker);
            _unitSelectionBox.Construct(_unitSelectionManager);
            _generator = new BoxGenerator();
            _formationController = new FormationController(_generator);
            _inputManager = new InputManager(_camera);
            _inputController = new InputController(_inputManager, _unitSelectionManager, 
                                                    _unitSelectionBox, _clickable, _ground, _formationController);
            _enemies = new List<Enemy>();
            foreach (Transform enemyChild in _unitListParent)
            {
                if (enemyChild.TryGetComponent(out Enemy enemy))
                {
                    _enemies.Add(enemy);
                }
            }
            _patrolling = new Patrolling(_enemies);
            _unitSelectionBox.Construct(_unitSelectionManager);
        }

        private void Update()
        {
            _inputManager.Update();
        }

        private void FixedUpdate()
        {
            _patrolling.FixedUpdate();
        }

        private void OnDestroy()
        {
            _inputController.Unsubscribe();
        }
    }
}
