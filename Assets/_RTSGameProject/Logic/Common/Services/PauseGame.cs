using _RTSGameProject.Logic.Common.AI;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.View;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Services
{
    public class PauseGame
    {
        private UnitsRepository _unitsRepository;
        private WinLoseWindow _winLoseWindow;
        
        public PauseGame(UnitsRepository unitsRepository, WinLoseWindow winLoseWindow)
        {
            _unitsRepository = unitsRepository;
            _winLoseWindow = winLoseWindow;
        }

        public void Pause()
        {
            if (_winLoseWindow.WinPanel.activeSelf)
            {
                foreach (Unit unit in _unitsRepository.AllUnits)
                {
                    if (unit.Team == 1)
                    {
                        unit.gameObject.GetComponent<Renderer>().material.color = Color.gray;
                    }
                    
                    unit.GetComponent<UnitMovement>().enabled = false;
                    unit.GetComponent<UnitAttackAct>().enabled = false;
                    unit.GetComponent<Unit>().enabled = false;
                }
            }
            
            if (_winLoseWindow.LosePanel.activeSelf)
            {
                foreach (Unit unit in _unitsRepository.AllUnits)
                {
                    if (unit.Team == 0)
                    {
                        unit.gameObject.GetComponent<Renderer>().material.color = Color.gray;
                    }
                    
                    unit.GetComponent<UnitMovement>().enabled = false;
                    unit.GetComponent<UnitAttackAct>().enabled = false;
                    unit.GetComponent<Unit>().enabled = false;
                }
            }
        }
    }
}
