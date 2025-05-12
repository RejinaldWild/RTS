using System.Collections.Generic;

namespace _RTSGameProject.Logic.Common.SaveLoad
{
    public sealed class MapKeyProvider : IKeyProvider
    {
        private SaveDataBase _saveDataBase;

        public MapKeyProvider(SaveDataBase saveDataBase)
        {
            saveDataBase.Initialize(); // in EntryPoint or Zenject
            _saveDataBase = saveDataBase;
        }
        
        public string Provide<TData>() => _saveDataBase.Map[typeof(TData)];

        public IEnumerable<string> ProvideAll() => _saveDataBase.Map.Values;
    }
    
    
}