using System.Threading.Tasks;
using _RTSGameProject.Logic.Common.Config;

namespace _RTSGameProject.Logic.Common.Services
{
    public interface IRemoteConfigProvider
    {
        public WinLoseConfig WinLoseConfig { get; set; }
        public UnitConfig UnitConfig { get; set; }
        public Task FetchDataAsync();
    }
}