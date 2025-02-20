using System.Linq;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Character.View;
using _RTSGameProject.Logic.Common.Construction.Model;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.View
{
    public class TeamColor : MonoBehaviour
    {
        [SerializeField] private Unit _unit;
        [SerializeField] private HouseBuilding _houseBuilding;
        [SerializeField] private ColorConfig[] _colorConfigs;
        [SerializeField] private Renderer _renderer;

        private void Start()
        {
            if (_unit != null)
            {
                _renderer.material.color = _colorConfigs.First(x => x.Team == _unit.Team).Color;
            }

            if (_houseBuilding != null)
            {
                _renderer.material.color = _colorConfigs.First(x => x.Team == _houseBuilding.Team).Color;
            }
        }
    }
}