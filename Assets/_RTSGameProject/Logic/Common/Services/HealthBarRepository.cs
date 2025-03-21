using System.Collections.Generic;
using _RTSGameProject.Logic.Common.View;

namespace _RTSGameProject.Logic.Common.Services
{
    public class HealthBarRepository
    {
        private List<HealthView> _items = new List<HealthView>();
        
        public void Register(HealthView healthView)
        {
            _items.Add(healthView);
        }

        public void Unregister(HealthView healthView)
        {
            _items.Remove(healthView);
        }
    }
}