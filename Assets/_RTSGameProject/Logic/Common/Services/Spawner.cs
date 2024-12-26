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
        
        public void Spawn(int teamId)
        {
            if(teamId == 0)
                _aiFactory.Create(teamId, new Vector3(22.5f,0.45f,32.65f));
            if(teamId == 1)
                _aiFactory.Create(teamId, new Vector3(29.2f,0.45f,22f));
        }
    }
}