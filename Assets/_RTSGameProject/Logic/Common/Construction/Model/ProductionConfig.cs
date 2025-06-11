using _RTSGameProject.Logic.Common.Character.Model;
using UnityEngine;
using UnityEngine.UI;

namespace _RTSGameProject.Logic.Common.Construction.Model
{
    [CreateAssetMenu(fileName = "ProductionBuildingConfig", menuName = "Config/ProductionBuildingConfig")]
    public class ProductionConfig:ScriptableObject
    {
        [SerializeField] private Unit UnitPrefab;
        [SerializeField] private Unit EnemyPrefab;
        [SerializeField] private Unit ExpensiveUnitPrefab;
        [SerializeField] private Button UnitButton;
        [SerializeField] private Button ExpensiveUnitButton;
    }
}