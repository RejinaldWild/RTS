using System;
using _RTSGameProject.Logic.Common.Services;
using UniRx;
using UnityEngine;
using Unit = _RTSGameProject.Logic.Common.Character.Model.Unit;

namespace _RTSGameProject.Logic.Common.AI
{
    public abstract class AiFactory
    {
        protected readonly UnitsFactory UnitsFactory;
        protected readonly UnitsRepository UnitsRepository;
        protected readonly ActorsRepository ActorsRepository;
        protected readonly PauseGame PauseGame;

        public AiFactory(UnitsRepository unitsRepository, ActorsRepository actorsRepository, UnitsFactory unitFactory, PauseGame pauseGame)
        {
            UnitsRepository = unitsRepository;
            ActorsRepository = actorsRepository;
            UnitsFactory = unitFactory;
            PauseGame = pauseGame;
        }

        public void Create(int teamId, Vector3 position)
        {
            Unit unit = UnitsFactory.Create(teamId, position);
            IAiActor aiActor = CreateAiActor(unit,PauseGame);

            IDisposable disposable = null;
            disposable = unit.IsAlive.Subscribe(isAlive =>
            {
                if (!isAlive)
                {
                    DisposeAi();
                }
            });
            
            ActorsRepository.Register(aiActor);
            
            void DisposeAi()
            {
                ActorsRepository.Unregister(aiActor);
                disposable.Dispose();
            }
        }
        
        protected abstract IAiActor CreateAiActor(Unit unit, PauseGame pauseGame);
    }
}