using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressbarBehaviour : MonoBehaviour
{
    public Image ProgressBar;
    public GameObject WinText;
    public GameObject LoseText;
    public float ProgressPool = 0;
    public bool DEVSWITCH = false;

    void Awake()
    {
        LoseText.SetActive(false);
        WinText.SetActive(false);
        Time.timeScale = 1;
        ProgressPool = 0.3f;
    }

    void Update()
    {
        if (DEVSWITCH == false)
        {
            ProgressBar.fillAmount = ProgressPool;
            if (ProgressPool >= 1)
            {
                LoseText.SetActive(true);
                Time.timeScale = 0;
            }
            if (ProgressPool < 0)
            {
                WinText.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
