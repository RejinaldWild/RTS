using UnityEngine;

namespace RTS.Scripts
{
    public class BoxGenerator : IFormationPositionGenerator
    {
        public Vector3[] GetPosition(int count)
        {
            int sideBox = Mathf.CeilToInt(Mathf.Sqrt(count));

            Vector3[] result = new Vector3[count];
            for (int i = 0; i < count; i++)
            {
                int row = i / sideBox;
                int column = i % sideBox;

                result[i] = new Vector3(row, 0, column);
            }

            return result;
        }
    }
}
