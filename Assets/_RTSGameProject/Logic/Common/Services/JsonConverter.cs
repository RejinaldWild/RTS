using UnityEngine;

namespace _RTSGameProject.Logic.Common.Services
{
    public class JsonConverter
    {
        public string ToJson(string data)
        {
            return JsonUtility.ToJson(data);
        }

        public string FromJson(string serializedData)
        {
            return JsonUtility.FromJson<string>(serializedData);
        }
    }
}
