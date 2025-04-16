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
        [SerializeField] private LayerMask _clickable;
        [SerializeField] private LayerMask _ground;
        [SerializeField] private LayerMask _buildingMask;
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
        public void Construct(IInstantiator diContainer)
        {
            _diContainer = diContainer;
            _healthBarRepository = _diContainer.Instantiate<HealthBarRepository>();
            _actorsRepository = _diContainer.Instantiate<ActorsRepository>();
            _buildingsRepository = _diContainer.Instantiate<BuildingsRepository>();
            _unitsRepository = _diContainer.Instantiate<UnitsRepository>();
            _selectionManager = _diContainer.Instantiate<SelectionManager>();
            _pauseGame = _diContainer.Instantiate<PauseGame>();
            _saveSystem = _diContainer.Instantiate<SaveSystem>();
            _changeScene = _diContainer.Instantiate<ChangeScene>();
            _formationController = _diContainer.Instantiate<FormationController>();
            _panelController = _diContainer.Instantiate<PanelController>();
            _inputCatchKeyClick = _diContainer.Instantiate<InputCatchKeyClick>();
            _winLoseGame = _diContainer.Instantiate<WinLoseGame>();
            _scoreGameController = _diContainer.Instantiate<ScoreGameController>();
            //_inputController = _diContainer.Instantiate<InputController>();
            // _healthBarFactory = _diContainer.Instantiate<HealthBarFactory>();
            // _unitsFactory = _diContainer.Instantiate<UnitsFactory>();
            // _aiFactory = _diContainer.Instantiate<StateMachineAiFactory>();
        }
        
        private void Awake()
        {
            _entryPointCoreController = new EntryPointCoreController(this, _changeScene);
            _selectionBox.Construct(_unitsRepository, _buildingsRepository, _selectionManager);
            
            _healthBarFactory = new HealthBarFactory(_healthBarRepository);
            _unitsFactory = new UnitsFactory(_unitsRepository, _healthBarFactory, _pauseGame);
            _aiFactory = new StateMachineAiFactory(_unitsRepository, _actorsRepository, _unitsFactory, _pauseGame);
            
            //_winLoseGame = new WinLoseGame(_winLoseWindow, _pauseGame, _unitsRepository, _winLoseConfig);
            _winLoseGame.Subscribe();
            _unitsRepository.Subscribe();
            _scoreGameView.Construct(_scoreGameController);
            
            foreach (HouseBuilding building in _buildings)
            {
                building.Construct(_aiFactory, _pauseGame, _panelController);
            }
            
            _inputController = new InputController(_inputCatchKeyClick, _pauseGame, _selectionManager, 
                                                    _selectionBox, _clickable, _ground, 
                                                    _buildingMask, _formationController);
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
            StopAllCoroutines();
        }
    }
}
