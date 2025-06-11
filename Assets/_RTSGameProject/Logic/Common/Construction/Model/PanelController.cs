using _RTSGameProject.Logic.Common.Construction.View;

namespace _RTSGameProject.Logic.Common.Construction.Model
{
    public class PanelController
    {
        private ProductionPanel _productionPanel;

        public void Initialize(ProductionPanel productionPanel)
        {
            _productionPanel = productionPanel;
        }

        public void ShowUIPanel(bool flagShow)
        {
            _productionPanel.ToggleUI(flagShow);
        }
        
        public void Subscribe(HouseBuilding building)
        {
            _productionPanel.OnClickUnit += building.SpawnUnit;
            _productionPanel.OnClickExpUnit += building.SpawnExpUnit;
        }
        
        public void Unsubscribe(HouseBuilding building)
        {
            _productionPanel.OnClickUnit -= building.SpawnUnit;
            _productionPanel.OnClickExpUnit -= building.SpawnExpUnit;
        }
    }
}