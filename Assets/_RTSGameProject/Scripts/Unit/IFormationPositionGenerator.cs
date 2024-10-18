using UnityEngine;

namespace RTS.Scripts
{
    public interface IFormationPositionGenerator
    {
        public Vector3[] GetPosition(int count);
    }
}
