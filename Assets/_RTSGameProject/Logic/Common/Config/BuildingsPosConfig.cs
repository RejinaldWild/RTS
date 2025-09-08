using UnityEngine;

namespace _RTSGameProject.Logic.Common.Config
{
    [CreateAssetMenu(menuName = "BuildingsPositions", fileName = "BuildingsPositionsConfig")]
    public class BuildingsPosConfig : ScriptableObject
    {
        public Vector3[] BuildingPositionScene;
    }
}
