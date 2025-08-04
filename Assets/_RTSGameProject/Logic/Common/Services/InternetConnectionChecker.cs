using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace _RTSGameProject.Logic.Common.Services
{
    public class InternetConnectionChecker
    {
        public async UniTask<bool> CheckInternetConnection()
        {
            using (UnityWebRequest webRequest = new UnityWebRequest("https://www.google.com"))
            {
                var operation = webRequest.SendWebRequest();

                while (!operation.isDone)
                {
                    await UniTask.Yield();
                }

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}