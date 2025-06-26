namespace _RTSGameProject.Logic.Analytic
{
    public interface IAnalyticService
    {
        public void SendBuildUnit(int data);
        public void SendBuildExpensiveUnit(int data);
        public void SendWinLevelEvent(int data);
        public void SendLoseLevel(int data);
        public void SendKillEnemy(int data);
        public void SendKillUnit(int data);
    }
}