using _RTSGameProject.Logic.Common.Score.Model;
using Cysharp.Threading.Tasks;

namespace _RTSGameProject.Logic.Common.SaveLoad
{
    public interface ISaveSystem
    {
        UniTask SaveAsync<TData>(TData data) where TData : ISaveData;
        UniTask<TData> LoadAsync<TData>() where TData : ISaveData;
    }
}