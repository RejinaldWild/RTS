using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Services;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _RTSGameProject.Logic.Common.SaveLoad
{
    public class SaveService : ISaveService
    {
        private InternetConnectionChecker _internetConnectionChecker;
        
        private readonly LocalSaveLoadService _localSaveLoadService;
        private readonly CloudSaveLoadService _cloudSaveLoadService;
        
        public SaveService(InternetConnectionChecker internetConnectionChecker, LocalSaveLoadService localSaveLoadService, CloudSaveLoadService cloudSaveLoadService)
        {
            _internetConnectionChecker = internetConnectionChecker;
            _localSaveLoadService = localSaveLoadService;
            _cloudSaveLoadService = cloudSaveLoadService;
        }
        
        public async UniTask<bool> IsSaveExist()
        {
            var isConnected = await _internetConnectionChecker.CheckInternetConnection();
            if (isConnected)
            {
                return await _cloudSaveLoadService.IsSaveExist();
            }
            else
            {
                return _localSaveLoadService.IsSaveExist();
            }
        }

        public async UniTask SaveAsync(ScoreGameData data)
        {
            bool isConnected = await _internetConnectionChecker.CheckInternetConnection();
            if (isConnected)
            {
                await _cloudSaveLoadService.SaveAsync(data);
                await _localSaveLoadService.SaveAsync(data);
            }
            else
            {
                await _localSaveLoadService.SaveAsync(data);
            }
        }

        public async UniTask<ScoreGameData> LoadAsync()
        {
            bool isConnected = await _internetConnectionChecker.CheckInternetConnection();
            if (isConnected)
            {
                ScoreGameData scoreGameDataCloud = await _cloudSaveLoadService.LoadAsync();
                ScoreGameData scoreGameDataLocal = await _localSaveLoadService.LoadAsync();
                if (scoreGameDataCloud.DateTime >= scoreGameDataLocal.DateTime)
                {
                    return scoreGameDataCloud;
                }
                
                return scoreGameDataLocal;
            }
            
            return await _localSaveLoadService.LoadAsync();
        }

        public async UniTask DeleteAsync()
        {
            bool isConnected = await _internetConnectionChecker.CheckInternetConnection();
            if (isConnected)
            {
                await _cloudSaveLoadService.DeleteAsync();
            }
            else
            {
                await _localSaveLoadService.DeleteAsync();
            }
        }
    }
}