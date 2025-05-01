using System;
using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Score.Model;

namespace _RTSGameProject.Logic.Common.SaveLoad
{
    public class SaveDataBase
    {
        private IReadOnlyDictionary<Type, string> _map;
        public IReadOnlyDictionary<Type, string> Map => _map;

        public void Initialize()
        {
            _map = new Dictionary<Type, string>()
            {
                { typeof(ScoreGameData), "ScoreGameData" }
            };
        }
    }
}