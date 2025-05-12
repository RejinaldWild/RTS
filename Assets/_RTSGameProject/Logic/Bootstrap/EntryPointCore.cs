using System.Linq;
using _RTSGameProject.Logic.Common.AI;
using _RTSGameProject.Logic.Common.Construction.Model;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class EntryPointCore : MonoBehaviour
    {
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button[] _mainMenuButtons;

        public Button NextLevelButton => _nextLevelButton;
        public Button[] MainMenuButtons => _mainMenuButtons;

        private HouseBuilding[] _buildings;
        private InputController _inputController;
        private InputCatchKeyClick _inputCatchKeyClick;
        private ActorsRepository _actorsRepository;
        private UnitsRepository _unitsRepository;
        private WinLoseGame _winLoseGame;
        private SceneChanger _sceneChanger;
        private ScoreGameController _scoreGameController;
        private IInstantiator _diContainer;
        private SaveSystem _saveSystem;

        [Inject]
        public void Construct(IInstantiator diContainer, UnitsRepository unitsRepository,
            WinLoseGame winLoseGame, InputCatchKeyClick inputCatchKeyClick,
            ScoreGameController scoreGameController,
            ActorsRepository actorsRepository,
            HouseBuilding[] buildings, SaveSystem saveSystem)
        {
            _diContainer = diContainer;
            _unitsRepository = unitsRepository;
            _winLoseGame = winLoseGame;
            _inputCatchKeyClick = inputCatchKeyClick;
            _scoreGameController = scoreGameController;
            _actorsRepository = actorsRepository;
            _buildings = buildings;
            _saveSystem = saveSystem;
            _sceneChanger = _diContainer.Instantiate<SceneChanger>();
            _inputController = _diContainer.Instantiate<InputController>();
        }

        private void Awake()
        {
            Subscribe();
            if (_saveSystem.IsSaveExist<ScoreGameData>().ToString() != "False")
            {
                _scoreGameController.LoadData();
            }
            else
            {
                _sceneChanger.LoadStartData();
            }
        }

        private void Update()
        {
            _inputCatchKeyClick.Update();
            _scoreGameController.Update();
            foreach (IAiActor aiActor in _actorsRepository.All.ToArray())
            {
                aiActor.Update();
            }
        }

        private void OnDestroy()
        {
            Unsubscribe();
            StopAllCoroutines();
        }
        
        private void Subscribe()
        {
            _winLoseGame.Subscribe();
            _unitsRepository.Subscribe();
            _inputController.Subscribe();
            _scoreGameController.Subscribe();
            
            foreach (HouseBuilding building in _buildings)
            {
                building.Subscribe();
            }
            
            NextLevelButton.onClick.AddListener(_sceneChanger.ToNextLevel);
            
            foreach (Button mainMenuButton in MainMenuButtons)
            {
                mainMenuButton.onClick.AddListener(_sceneChanger.ToMainMenu);
            }
        }
        
        private void Unsubscribe()
        {
            _inputController.Unsubscribe();
            _winLoseGame.Unsubscribe();
            _scoreGameController.Unsubscribe();
            _unitsRepository.Unsubscribe();

            foreach (HouseBuilding building in _buildings)
            {
                building.Unsubscribe();
            }
            
            NextLevelButton.onClick.RemoveListener(_sceneChanger.ToNextLevel);
            
            foreach (Button mainMenuButton in MainMenuButtons)
            {
                mainMenuButton.onClick.RemoveListener(_sceneChanger.ToMainMenu);
            }
        }
    }
}