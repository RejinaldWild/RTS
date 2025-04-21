using System.Collections.Generic;
using System.Linq;
using _RTSGameProject.Logic.Common.AI;
using _RTSGameProject.Logic.Common.Camera;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Config;
using _RTSGameProject.Logic.Common.Construction.Model;
using _RTSGameProject.Logic.Common.Construction.View;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;
using _RTSGameProject.Logic.Common.Selection;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.Common.View;
using _RTSGameProject.Logic.StateMachine.Implementation;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class EntryPointCore : MonoBehaviour
    {
        [SerializeField] private WinLoseConfig _winLoseConfig;
        [SerializeField] private WinLoseWindow _winLoseWindow;
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _groundMarker;
        [SerializeField] private SelectionBox _selectionBox;
        [SerializeField] private BuildPanel _buildPanel;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private ScoreGameUI _scoreGameView;
        [SerializeField] private HouseBuilding[] _buildings;
        [SerializeField] private Button[] _mainMenuButtons;
        
        public Button NextLevelButton => _nextLevelButton;
        public Button[] MainMenuButtons => _mainMenuButtons;
        
        private SelectionManager _selectionManager;
        private FormationController _formationController;
        private InputController _inputController;
        private InputCatchKeyClick _inputCatchKeyClick;
        private PatrollMovement _patrollMovement;
        private BoxGenerator _generator;
        private ActorsRepository _actorsRepository;
        private StateMachineAiFactory _stateMachineAiFactory;
        private HealthBarFactory _healthBarFactory;
        private HealthBarRepository _healthBarRepository;
        private UnitsFactory _unitsFactory;
        private CameraController _cameraController;
        private BuildingsRepository _buildingsRepository;
        private UnitsRepository _unitsRepository;
        private PanelController _panelController;
        private Health _health;
        private AiFactory _aiFactory;
        private WinLoseGame _winLoseGame;
        private ChangeScene _changeScene;
        private PlayerPrefsDataStorage _playerDataStorage;
        private JsonConverter _jsonConverter;
        private SaveSystem _saveSystem;
        private ScoreGameController _scoreGameController;
        private ScoreGameData _scoreGameData;
        private PauseGame _pauseGame;
        private EntryPointCoreController _entryPointCoreController;
        private IInstantiator _diContainer;
        
        [Inject]
        public void Construct(IInstantiator diContainer, UnitsRepository unitsRepository, 
                                WinLoseGame winLoseGame, PauseGame pauseGame, 
                                InputCatchKeyClick inputCatchKeyClick, 
                                ScoreGameController scoreGameController,
                                HealthBarFactory healthBarFactory,
                                UnitsFactory unitsFactory,
                                StateMachineAiFactory stateMachineAiFactory,
                                ActorsRepository actorsRepository)
        {
            _diContainer = diContainer;
            _unitsRepository = unitsRepository;
            _winLoseGame = winLoseGame;
            _pauseGame = pauseGame;
            _inputCatchKeyClick = inputCatchKeyClick;
            _scoreGameController = scoreGameController;
            _unitsFactory = unitsFactory;
            _changeScene = _diContainer.Instantiate<ChangeScene>();
            _inputController = _diContainer.Instantiate<InputController>();
            //_aiFactory = stateMachineAiFactory;
            _actorsRepository = actorsRepository;
        }
        
        private void Awake()
        {
            _entryPointCoreController = new EntryPointCoreController(this, _changeScene);
            
            _winLoseGame.Subscribe();
            _unitsRepository.Subscribe();
            _inputController.Subscribe();
            _scoreGameController.Subscribe();
            
            foreach (HouseBuilding building in _buildings)
            {
                building.Subscribe();
            }
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
            _winLoseGame.Unsubscribe();
            _scoreGameController.Unsubscribe();
            _entryPointCoreController.Unsubscribe();
            _unitsRepository.Unsubscribe();
            
            foreach (HouseBuilding building in _buildings)
            {
                building.Unsubscribe();
            }
            
            StopAllCoroutines();
        }
    }
}
