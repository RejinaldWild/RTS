using System.Linq;
using _RTSGameProject.Logic.Common.AI;
using _RTSGameProject.Logic.Common.Camera;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Construction.Model;
using _RTSGameProject.Logic.Common.Construction.View;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;
using _RTSGameProject.Logic.Common.Selection;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.Common.View;
using _RTSGameProject.Logic.StateMachine.Implementation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class EntryPointCore : MonoBehaviour
    {
        [SerializeField] private int _winConditionKillUnits;
        [SerializeField] private int _loseConditionKillUnits;
        [SerializeField] private WinLoseWindow _winLoseWindow;
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _groundMarker;
        [SerializeField] private SelectionBox _selectionBox;
        [SerializeField] private LayerMask _clickable;
        [SerializeField] private LayerMask _ground;
        [SerializeField] private LayerMask _buildingMask;
        [SerializeField] private HouseBuilding[] _buildings;
        [SerializeField] private BuildPanel _buildPanel;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button[] _mainMenuButton;
        [SerializeField] private ScoreUI _scoreView;
        
        private SelectionManager _selectionManager;
        private FormationController _formationController;
        private InputController _inputController;
        private InputCatchKeyClick _inputCatchKeyClick;
        private PatrollMovement _patrollMovement;
        private BoxGenerator _generator;
        private ActorsRepository _actorsRepository;
        private StateMachineAiFactory _stateMachineAiFactory;
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
        private int _sceneIndex;
        private ScoreController _scoreController;
        private ScoreData _scoreData;
        private PauseGame _pauseGame;

        private void Awake()
        {
            _selectionManager = new SelectionManager(_groundMarker);
            _buildingsRepository = new BuildingsRepository(_selectionManager, _buildings);
            _unitsRepository = new UnitsRepository(_selectionManager);
            _selectionBox.Construct(_unitsRepository, _buildingsRepository, _selectionManager);
            _generator = new BoxGenerator();
            _formationController = new FormationController(_generator);
            _inputCatchKeyClick = new InputCatchKeyClick(_camera);
            _actorsRepository = new ActorsRepository();
            _unitsFactory = new UnitsFactory(_unitsRepository);
            _aiFactory = new StateMachineAiFactory(_unitsRepository, _actorsRepository, _unitsFactory);
            _panelController = new PanelController(_buildPanel);
            _pauseGame = new PauseGame(_unitsRepository, _winLoseWindow);
            _winLoseGame = new WinLoseGame(_winLoseWindow, _pauseGame, _unitsRepository, _winConditionKillUnits, _loseConditionKillUnits);
            _playerDataStorage = new PlayerPrefsDataStorage();
            _jsonConverter = new JsonConverter();
            _saveSystem = new SaveSystem(_jsonConverter, _playerDataStorage);
            _sceneIndex = SceneManager.GetActiveScene().buildIndex;
            _changeScene = new ChangeScene(_sceneIndex, _mainMenuButton, _nextLevelButton);
            _scoreData = new ScoreData();
            _scoreController = new ScoreController(_scoreView, _scoreData, _winLoseGame, _changeScene, _saveSystem);
            _scoreView.Construct(_scoreController);
            
            foreach (HouseBuilding building in _buildings)
            {
                building.Construct(_aiFactory, _panelController);
            }
            
            _inputController = new InputController(_inputCatchKeyClick, _selectionManager, 
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
            _scoreController.Unsubscribe();
            _changeScene.Unsubscribe();
            StopAllCoroutines();
        }
    }
}
