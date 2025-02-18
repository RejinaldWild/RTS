using _RTSGameProject.Logic.Common.AI;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Services
{
    public class Spawner
    {
        private UnitsRepository _unitsRepository;
        private AiFactory _aiFactory;

        public Spawner(AiFactory aiFactory)
        {
            _aiFactory = aiFactory;
        }   
        
        public void Spawn(int teamId, Vector3 position)
        {
            if(teamId == 0)
                _aiFactory.Create(teamId, position);
            if(teamId == 1)
                _aiFactory.Create(teamId, position);
        }
    }
}