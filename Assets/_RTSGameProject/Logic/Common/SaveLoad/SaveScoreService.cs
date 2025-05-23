using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Services;
using Cysharp.Threading.Tasks;

namespace _RTSGameProject.Logic.Common.SaveLoad
{
    public class SaveScoreService : ISaveScoreService
    {
        private ISerializer _serializer;
        private IDataStorage _dataStorage;
        private IKeyProvider _keyProvider;
        
        public SaveScoreService(ISerializer serializer, IDataStorage dataStorage, IKeyProvider keyProvider)
        {
            _serializer = serializer;
            _dataStorage = dataStorage;
            _keyProvider = keyProvider;
        }

        public bool IsSaveExist()
        {
            string dataKey = _keyProvider.Provide<ISaveData>();
            return _dataStorage.Exist(dataKey);
        }
        
        public async UniTask SaveAsync(ISaveData data)
        {
            string dataKey = _keyProvider.Provide<ISaveData>();
            string serializedData = await _serializer.ToJsonAsync(data);
            await _dataStorage.WriteAsync(dataKey, serializedData);
        }

        public async UniTask<ScoreGameData> LoadAsync()
        {
            string dataKey = _keyProvider.Provide<ISaveData>();
            string serializedData = await _dataStorage.ReadAsync(dataKey);
            return await _serializer.FromJsonAsync(serializedData);
        }

        public async UniTask DeleteAsync()
        {
            string dataKey = _keyProvider.Provide<ISaveData>();
            await _dataStorage.DeleteAsync(dataKey);
        }
    }
}
