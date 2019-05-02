using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressbarBehaviour : MonoBehaviour
{
    public Image ProgressBar;
    public GameObject WinText;
    public GameObject LoseText;
    public float ProgressPool = 0.0f;
    public float AddPoolTMP = 0.1f;

    void Awake()
    {
        LoseText.SetActive(false);
        WinText.SetActive(false);
        Time.timeScale = 1;
        ProgressBar.fillAmount = 0;
    }

    void Update()
    {
        ProgressBar.fillAmount = ProgressPool;
        Debug.Log(ProgressPool);
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
