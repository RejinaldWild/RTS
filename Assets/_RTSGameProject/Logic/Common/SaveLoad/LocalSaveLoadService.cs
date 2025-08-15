using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.SaveLoad
{
    public class LocalSaveLoadService: ISaveService
    {
        private const string SCORE_GAME_DATA = "ScoreGameData";
        
        private readonly ISerializer _serializer;
        private readonly IDataStorage _dataStorage;
        
        public LocalSaveLoadService(ISerializer serializer, IDataStorage dataStorage)
        {
            _serializer = serializer;
            _dataStorage = dataStorage;
        }

        public async UniTask Initialize()
        {
            Debug.Log("LocalSaveLoadService initialized");
            await UniTask.CompletedTask;
        }
        
        public UniTask<bool> IsSaveExist()
        {
            string scoreGameData = SCORE_GAME_DATA;
            bool exist = _dataStorage.Exist(scoreGameData);
            return new UniTask<bool>(exist);
        }
        
        public async UniTask SaveAsync(ScoreGameData data)
        {
            string serializedData = await _serializer.ToJsonAsync(data);
            await _dataStorage.WriteAsync(SCORE_GAME_DATA, serializedData);
        }

        public async UniTask<ScoreGameData> LoadAsync()
        {
            string serializedData = await _dataStorage.ReadAsync(SCORE_GAME_DATA);
            return await _serializer.FromJsonAsync(serializedData);
        }

        public async UniTask DeleteAsync()
        {
            await _dataStorage.DeleteAsync(SCORE_GAME_DATA);
        }
    }
}
