using System.Linq;
using _RTSGameProject.Logic.Common.AI;
using _RTSGameProject.Logic.Common.Construction.Model;
using _RTSGameProject.Logic.Common.Services;
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
        private ChangeScene _changeScene;
        private ScoreGameController _scoreGameController;
        private EntryPointCoreController _entryPointCoreController;
        private IInstantiator _diContainer;

        [Inject]
        public void Construct(IInstantiator diContainer, UnitsRepository unitsRepository,
            WinLoseGame winLoseGame, InputCatchKeyClick inputCatchKeyClick,
            ScoreGameController scoreGameController,
            ActorsRepository actorsRepository,
            HouseBuilding[] buildings)
        {
            _diContainer = diContainer;
            _unitsRepository = unitsRepository;
            _winLoseGame = winLoseGame;
            _inputCatchKeyClick = inputCatchKeyClick;
            _scoreGameController = scoreGameController;
            _actorsRepository = actorsRepository;
            _buildings = buildings;
            _changeScene = _diContainer.Instantiate<ChangeScene>();
            _inputController = _diContainer.Instantiate<InputController>();
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
            _scoreGameController.Update();
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