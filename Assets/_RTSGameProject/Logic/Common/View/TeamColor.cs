using System.Linq;
using UnityEngine;
using _RTSGameProject.Logic.Common.Character.Model;

namespace _RTSGameProject.Logic.Common.Character.View
{
    public class TeamColor : MonoBehaviour
    {
        [SerializeField] private Unit _unit;
        [SerializeField] private Building.Model.Building _building;
        [SerializeField] private ColorConfig[] _colorConfigs;
        [SerializeField] private Renderer _renderer;

        private void Start()
        {
            _renderer.material.color = _colorConfigs.First(x => x.Team == _unit.Team).Color;
            //_renderer.material.color = _colorConfigs.First(x => x.Team == _building.Team).Color;
        }
    }
}