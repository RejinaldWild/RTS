using UnityEngine;

namespace _RTSGameProject.Logic.Common.View
{
    public class WinLoseWindow : MonoBehaviour
    {
        [field:SerializeField] public GameObject WinPanel {get; private set;}
        [field:SerializeField] public GameObject LosePanel {get; private set;}
    }
}
