using System.Collections.Generic;
using _RTSGameProject.Logic.Common.AI;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ActorsRepository
    {
        public IEnumerable<IAiActor> All => _actors;
    
        private readonly List<IAiActor> _actors = new List<IAiActor>();

        public void Register(IAiActor actor) => _actors.Add(actor);
    
        public void Unregister(IAiActor actor) => _actors.Remove(actor);
    }
}
