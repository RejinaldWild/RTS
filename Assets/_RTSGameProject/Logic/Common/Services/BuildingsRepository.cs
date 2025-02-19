using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Selection;

namespace _RTSGameProject.Logic.Common.Services
{
    public class BuildingsRepository
    {
        public List<Building.Model.Building> AllBuildings;
        private Health _health;
        private SelectionManager _selectionManager;
        
        public BuildingsRepository(SelectionManager selectionManager, Building.Model.Building[] buildings)
        {
            AllBuildings = new List<Building.Model.Building>();
            foreach (var building in buildings)
            {
                AllBuildings.Add(building);
            }
            _selectionManager = selectionManager;
        }
        
        public void Register(Building.Model.Building building) => 
            AllBuildings.Add(building);

        public void Unregister(Building.Model.Building building)
        {
            AllBuildings.Remove(building);
            _selectionManager.SelectedBuildings.Remove(building);
        }
    }
}