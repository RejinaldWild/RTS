using UnityEngine;

namespace _RTSGameProject.Logic.Common.Services
{
    public interface IFormationPositionGenerator
    {
        public Vector3[] GetPosition(int count);
    }
}
