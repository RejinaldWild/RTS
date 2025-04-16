using _RTSGameProject.Logic.Common.Score.Model;

namespace _RTSGameProject.Logic.Common.Services
{
    public class SaveSystem
    {
        private JsonConverter _jsonConverter;
        private PlayerPrefsDataStorage _playerDataStorage;
        
        public SaveSystem(JsonConverter jsonConverter, PlayerPrefsDataStorage storage)
        {
            _jsonConverter = jsonConverter;
            _playerDataStorage = storage;
        }

        public void SaveGame(string key, ScoreGameData menuData)
        {
            string serializedData = _jsonConverter.ToJson(menuData.ToString());
            _playerDataStorage.WriteAsync(key, serializedData);
            _playerDataStorage.Save();
        }

        public void LoadGame(string key)
        {
            _playerDataStorage.ReadAsync(key);
        }
    }
}
