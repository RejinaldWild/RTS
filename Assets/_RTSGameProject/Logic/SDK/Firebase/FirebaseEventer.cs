using Firebase.Analytics;

namespace _RTSGameProject.Logic.SDK.Firebase
{
    public class FirebaseEventer
    {
        public void StartApplication()
        {
            FirebaseAnalytics.LogEvent("StartApp" , new Parameter("StartApplication", 1));
        }

        public void BuiltUnit(int data)
        {
            FirebaseAnalytics.LogEvent("BuiltUnits" , new Parameter("BuiltUnitsFromHouse", data));
        }

        public void UnitKilled(int data)
        {
            FirebaseAnalytics.LogEvent("Casualties" , new Parameter("UnitCasualties", data));
        }
        
        public void EnemyKilled(int data)
        {
            FirebaseAnalytics.LogEvent("Kills" , new Parameter("KilledEnemies", data));
        }
        
        public void WonLevel(int data)
        {
            FirebaseAnalytics.LogEvent("Wins" , new Parameter("WinScore", data));
        }

        public void LostLevel(int data)
        {
            FirebaseAnalytics.LogEvent("Loses" , new Parameter("LoseScore", data));
        }

        public void BuiltExpensiveUnit(int data)
        {
            FirebaseAnalytics.LogEvent("BuiltExpUnits" , new Parameter("BuiltExpensiveUnits", data));
        }
        
        public void StopApplication()
        {
            FirebaseAnalytics.LogEvent("StopApp" , new Parameter("StopApplication", 0));
        }
    }
}