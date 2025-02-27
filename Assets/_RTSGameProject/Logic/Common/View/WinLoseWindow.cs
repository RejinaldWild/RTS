using System;
using UnityEngine;

public class WinLoseWindow : MonoBehaviour
{
    [field:SerializeField] public GameObject WinPanel {get; private set;}
    [field:SerializeField] public GameObject LosePanel {get; private set;}

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
