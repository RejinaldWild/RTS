namespace _RTSGameProject.Logic.Common.SaveLoad
{
    public sealed class KeyProvider : IKeyProvider
    {
        private SaveDataBase _saveDataBase;

        public KeyProvider(SaveDataBase saveDataBase)
        {
            _saveDataBase = saveDataBase;
        }
        
        public string Provide<TData>() => _saveDataBase.ScoreGameData;
    }
    
    
}