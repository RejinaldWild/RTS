using _RTSGameProject.Logic.Common.Config;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.Common.Services.SoundFX;
using Cysharp.Threading.Tasks;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class MainMenuService
    {
        private readonly IPurchase _purchaseService;
        
        private ISaveService _saveService;
        private ISceneChanger _sceneChanger;
        private IAudio _audioService;
        private ScoreMenuController _scoreMenuController;
        
        public MainMenuService(IPurchase purchaseService, ScoreMenuController scoreMenuController, ISceneChanger sceneChanger, ISaveService saveService, IAudio audioService)
        {
            _saveService = saveService;
            _sceneChanger = sceneChanger;
            _scoreMenuController = scoreMenuController;
            _purchaseService = purchaseService;
            _audioService = audioService;
        }

        public async UniTask Initialize()
        {
            await _saveService.Initialize();
            if (await _saveService.IsSaveExist())
            {
                await _scoreMenuController.LoadDataAsync();
                _scoreMenuController.GetDataToShowScore(_scoreMenuController.ScoreGameData);
            }
            else
            {
                _scoreMenuController.InitializeScoreGameData();
                _scoreMenuController.GetDataToShowScore(_scoreMenuController.ScoreGameData);
            }
        }
        
        public void OnStartButtonClick()
        {
            _audioService.PlayRandomSoundFX(SoundType.CLICK);
            _sceneChanger.ToStartGame();
        }

        public void OnLoadButtonClick()
        {
            _audioService.PlayRandomSoundFX(SoundType.CLICK);
            _sceneChanger.ToLoadGame();
        }
        
        public async void OnDeleteButtonClick()
        {
            _audioService.PlayRandomSoundFX(SoundType.CLICK);
            await _scoreMenuController.DeleteSaves();
        }

        public void OnQuitButtonClick()
        {
            _audioService.PlayRandomSoundFX(SoundType.CLICK);
            _sceneChanger.ToQuitGame();
        }
        
        public void OnNoAdsButtonClick()
        {
            _audioService.PlayRandomSoundFX(SoundType.CLICK);
            _purchaseService.Payment();
        }
    }
}