using _RTSGameProject.Logic.Common.Construction.View;

namespace _RTSGameProject.Logic.Common.Construction.Model
{
    public class PanelController
    {
        private BuildPanel _buildPanel;
        
        public PanelController(BuildPanel buildPanel)
        {
            _buildPanel = buildPanel;
        }

        public void ShowUIPanel(bool flagShow)
        {
            _buildPanel.ToggleUI(flagShow);
        }
        
        public void Subscribe(HouseBuilding building)
        {
            _buildPanel.OnClick += building.SpawnUnit;
        }
        
        public void Unsubscribe(HouseBuilding building)
        {
            _buildPanel.OnClick -= building.SpawnUnit;
        }
    }
}