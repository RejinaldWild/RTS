using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Services;
using Cysharp.Threading.Tasks;

namespace _RTSGameProject.Logic.Common.SaveLoad
{
    public class SaveScoreService : ISaveScoreService
    {
        private ISerializer _serializer;
        private IDataStorage _dataStorage;
        
        public SaveScoreService(ISerializer serializer, IDataStorage dataStorage)
        {
            _serializer = serializer;
            _dataStorage = dataStorage;
        }

        public bool IsSaveExist()
        {
            string str = ConvertToString();
            return _dataStorage.Exist(str);
        }
        
        public async UniTask SaveAsync(ScoreGameData data)
        {
            string serializedData = await _serializer.ToJsonAsync(data);
            await _dataStorage.WriteAsync(ConvertToString(), serializedData);
        }

        public async UniTask<ScoreGameData> LoadAsync()
        {
            string serializedData = await _dataStorage.ReadAsync(ConvertToString());
            return await _serializer.FromJsonAsync(serializedData);
        }

        public async UniTask DeleteAsync()
        {
            await _dataStorage.DeleteAsync(ConvertToString());
        }

        private string ConvertToString()
        {
            return nameof(ScoreGameData);
        }
    }
}
