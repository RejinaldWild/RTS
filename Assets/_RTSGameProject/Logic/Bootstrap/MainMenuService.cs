using System.Threading.Tasks;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Services;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class MainMenuService
    {
        private readonly IPurchase _purchaseService;
        
        private ISaveService _saveService;
        private ISceneChanger _sceneChanger;
        private ScoreMenuController _scoreMenuController;
        
        public MainMenuService(IPurchase purchaseService, ScoreMenuController scoreMenuController, ISceneChanger sceneChanger, ISaveService saveService)
        {
            _saveService = saveService;
            _sceneChanger = sceneChanger;
            _scoreMenuController = scoreMenuController;
            _purchaseService = purchaseService;
        }

        public async Task Initialize()
        {
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
            _sceneChanger.ToStartGame();
        }

        public void OnLoadButtonClick()
        {
            _sceneChanger.ToLoadGame();
        }
        
        public async void OnDeleteButtonClick()
        {
            await _scoreMenuController.DeleteSaves();
        }

        public void OnQuitButtonClick()
        {
            _sceneChanger.ToQuitGame();
        }
        
        public void OnNoAdsButtonClick()
        {
            _purchaseService.Payment();
        }
    }
}