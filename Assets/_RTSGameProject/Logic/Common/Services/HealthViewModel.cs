using _RTSGameProject.Logic.Common.Character.Model;
using UniRx;

namespace _RTSGameProject.Logic.Common.Services
{
    public class HealthViewModel
    {
        public IReadOnlyReactiveProperty<float> HeathRelative { get; set; }
        
        public HealthViewModel(Health healthModel)
        {
            HeathRelative = healthModel.Current
                .Select(hp => hp / healthModel.Max)
                .ToReadOnlyReactiveProperty();
        }
    }
}