using Unity.Services.Core;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Services
{
    public class InitializeUnityServices : MonoBehaviour
    {
        private async void Start()
        {
            await UnityServices.InitializeAsync();
        }
    }
}