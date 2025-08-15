using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Construction.Model;
using _RTSGameProject.Logic.Common.Selection;

namespace _RTSGameProject.Logic.Common.Services
{
    public class BuildingsRepository
    {
        public List<HouseBuilding> AllBuildings;
        private SelectionManager _selectionManager;
        
        public BuildingsRepository(SelectionManager selectionManager)
        {
            AllBuildings = new List<HouseBuilding>();
            _selectionManager = selectionManager;
        }
        
        public void Register(HouseBuilding houseBuilding) => 
            AllBuildings.Add(houseBuilding);

        public void Unregister(HouseBuilding houseBuilding)
        {
            AllBuildings.Remove(houseBuilding);
            _selectionManager.SelectedBuildings.Remove(houseBuilding);
        }
    }
}