using UnityEngine;

namespace _RTSGameProject.Logic.Common.Config
{
    [CreateAssetMenu(fileName = "WinLoseConfig", menuName = "Config/WinLoseConfig")]
    public class WinLoseConfig: ScriptableObject
    {
        [SerializeField] private int _winConditionKillUnits;
        [SerializeField] private int _loseConditionKillUnits;
        
        public int WinConditionKillUnits => _winConditionKillUnits;
        public int LoseConditionKillUnits => _loseConditionKillUnits;
    }
}