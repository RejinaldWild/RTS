using _RTSGameProject.Logic.Common.Score.Model;

namespace _RTSGameProject.Logic.SDK
{
    public interface ISDK
    {
        public void Initialize();
        public void BuiltUnit(int data);
        public void BuiltExpensiveUnit(int data);
        public void WonLevel(int data);
        public void LostLevel(int data);
        public void EnemyKilled(int data);
        public void UnitKilled(int data);
        public void Dispose();
    }
}