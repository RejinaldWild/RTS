using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Services;
using Cysharp.Threading.Tasks;

namespace _RTSGameProject.Logic.Common.SaveLoad
{
    public class SaveSystem : ISaveSystem
    {
        private ISerializer _serializer;
        private IDataStorage _dataStorage;
        private IKeyProvider _keyProvider;
        
        public SaveSystem(ISerializer serializer, IDataStorage dataStorage, IKeyProvider keyProvider)
        {
            _serializer = serializer;
            _dataStorage = dataStorage;
            _keyProvider = keyProvider;
        }

        public UniTask<bool> IsSaveExist<TData>() where TData: ISaveData
        {
            string dataKey = _keyProvider.Provide<TData>();
            return _dataStorage.ExistAsync(dataKey);
        }
        
        public async UniTask SaveAsync<TData>(TData data) where TData: ISaveData
        {
            string dataKey = _keyProvider.Provide<TData>();
            string serializedData = await _serializer.ToJsonAsync(data);
            await _dataStorage.WriteAsync(dataKey, serializedData);
        }

        public async UniTask<TData> LoadAsync<TData>() where TData: ISaveData
        {
            string dataKey = _keyProvider.Provide<TData>();
            string serializedData = await _dataStorage.ReadAsync(dataKey);
            return await _serializer.FromJsonAsync<TData>(serializedData);
        }

        public async UniTask DeleteAsync<TData>() where TData : ISaveData
        {
            string dataKey = _keyProvider.Provide<TData>();
            await _dataStorage.DeleteAsync(dataKey);
        }
    }
}
