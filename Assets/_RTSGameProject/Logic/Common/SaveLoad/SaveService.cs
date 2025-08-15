using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Services;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _RTSGameProject.Logic.Common.SaveLoad
{
    public class SaveService : ISaveService
    {
        [Inject(Id = "LocalSaveLoad")]
        private ISaveService _localSaveLoadService;
        [Inject(Id = "CloudSaveLoad")]
        private ISaveService _cloudSaveLoadService;
        
        private InternetConnectionChecker _internetConnectionChecker;
        
        public SaveService(InternetConnectionChecker internetConnectionChecker)
        {
            _internetConnectionChecker = internetConnectionChecker;
        }


        public async UniTask Initialize()
        {
            var isConnected = await _internetConnectionChecker.CheckInternetConnection();
            if (isConnected)
            {
                await _cloudSaveLoadService.Initialize();
            }
            else
            {
                await _localSaveLoadService.Initialize();
            }
        }

        public async UniTask<bool> IsSaveExist()
        {
            var isConnected = await _internetConnectionChecker.CheckInternetConnection();
            if (isConnected)
            {
                bool localResult = await _localSaveLoadService.IsSaveExist();
                bool cloudResult = await _cloudSaveLoadService.IsSaveExist();
                return cloudResult && localResult;
            }
            else
            {
                return await _localSaveLoadService.IsSaveExist();
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
                if (scoreGameDataLocal!=null && scoreGameDataCloud!=null && scoreGameDataCloud.DateTime >= scoreGameDataLocal.DateTime)
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
                await _localSaveLoadService.DeleteAsync();
            }
            else
            {
                await _localSaveLoadService.DeleteAsync();
            }
        }
    }
}