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
        ProgressPool = 0.5f;
    }

    void Update()
    {
        if (DEVSWITCH == false)
        {
            ProgressBar.fillAmount = ProgressPool;
            if (ProgressPool >= 1)
            {
                DestoryDisasters();
                ShakeBehaviour.StopShake();
                LoseText.SetActive(true);
                Time.timeScale = 0;
            }
            //if (ProgressPool < 0)
            //{
            //    DestoryDisasters();
            //    ShakeBehaviour.StopShake();
            //    WinText.SetActive(true);
            //    Time.timeScale = 0;
            //}
        }
    }

    private void DestoryDisasters()
    {
        GameObject[] tmpObj = GameObject.FindGameObjectsWithTag("Disaster");
        for (var i = 0; i < tmpObj.Length; i++)
        {
            Destroy(tmpObj[i]);
        }
    }
}
