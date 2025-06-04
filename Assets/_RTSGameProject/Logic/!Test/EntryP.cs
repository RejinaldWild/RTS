using System;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Services;
using Zenject;

public class EntryP : IInitializable, ITickable, IDisposable
{
    private SaveScoreService _saveScoreService;
    private readonly JsonConverter _jsonConverter;
    private ScoreGameData _scoreGameData;
    private JsonConverter _serializer;
    private PlayerPrefsDataStorage _dataStorage;
    private KeyProvider _keyProvider;
    private SaveDataBase _saveData;

    public EntryP(JsonConverter jsonConverter, 
                    ScoreGameData scoreGameData, 
                    IKeyProvider keyProvider, 
                    SaveDataBase saveData,
                    PlayerPrefsDataStorage dataStorage,
                    SaveScoreService saveScoreService)
    {
        _jsonConverter = jsonConverter;
        _scoreGameData = scoreGameData;
        _keyProvider = keyProvider as KeyProvider;
        _saveData = saveData;
        _dataStorage = dataStorage;
        _saveScoreService = saveScoreService;
    }
    
    public async void Initialize()
    {
        if (_saveScoreService.IsSaveExist())
        {
            _scoreGameData = await _saveScoreService.LoadAsync();
        }
        else
        {
            await _saveScoreService.SaveAsync(_scoreGameData);
        }
    }

    public void Tick()
    {
        
    }

    public async void Dispose()
    {
        _scoreGameData.AddWinScore();
        _scoreGameData.AddLoseScore();
        await _saveScoreService.SaveAsync(_scoreGameData);
    }
}
