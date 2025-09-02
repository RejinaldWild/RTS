using _RTSGameProject.Logic.Common.Config;
using UnityEngine;

public interface IVFX
{
    public void ShowEffect(VFXType type, Vector3 position);
    public void ShowEffect(VFXType type, Transform parent);
    public void StopShowing(ParticleSystem effect);
}