using System;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Services;
using UnityEngine;
using static UnityEngine.Object;

namespace _RTSGameProject.Logic.Common.AI
{
    public abstract class AiFactory
    {
        protected readonly UnitsFactory UnitsFactory;
        protected readonly UnitsRepository UnitsRepository;
        protected readonly ActorsRepository ActorsRepository;

        public AiFactory(UnitsRepository unitsRepository, ActorsRepository actorsRepository, UnitsFactory unitFactory)
        {
            UnitsRepository = unitsRepository;
            ActorsRepository = actorsRepository;
            UnitsFactory = unitFactory;
        }

        public void Create(int teamId, Vector3 position)
        {
            Unit unit = UnitsFactory.Create(teamId, position);
            IAiActor aiActor = CreateAiActor(unit);
            
            unit.Health.OnDie += DisposeAi;
            ActorsRepository.Register(aiActor);
            
            void DisposeAi()
            {
                unit.Health.OnDie -= DisposeAi;
                ActorsRepository.Unregister(aiActor);
            }
        }
        
        protected abstract IAiActor CreateAiActor(Unit unit);
    }
}