using UnityEngine;

namespace _RTSGameProject.Logic.Common.Config
{
    [CreateAssetMenu(menuName = "EnemyBuildingsPositions", fileName = "EnemyBuildingsPositionsConfig")]
    public class EnemyBuildingsPosConfig : ScriptableObject
    {
        public Vector3[] BuildingPositionScene;
    }
}
