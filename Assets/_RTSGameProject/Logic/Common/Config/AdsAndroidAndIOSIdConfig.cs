using UnityEngine;

namespace _RTSGameProject.Logic.Common.Config
{
    [CreateAssetMenu (fileName = "AdsIdsConfig", menuName = "Config/AdsIds")]
    public class AdsAndroidAndIOSIdConfig: ScriptableObject
    {
        [SerializeField] public string AdsIdAndroid;
        [SerializeField] public string AdsIdIOS;
        [SerializeField] public bool IsTesting;
        [SerializeField] public string AndroidAdIdInterstitial;
        [SerializeField] public string IosAdIdInterstitial;
        [SerializeField] public string AndroidAdIdRewarded;
        [SerializeField] public string IosAdIdRewarded;
    }
}