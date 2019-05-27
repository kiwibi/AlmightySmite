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
    public Image Bar;
    
    Color Stage01 = new Color(0.0f, 0.85f, 0.0f);
    Color Stage02 = new Color(1.0f, 1.0f, 0.0f);
    Color Stage03 = new Color(1.0f, 0.0f, 0.0f);

    void Awake()
    {
        Bar.color = new Color(0, 200, 0);
        LoseText.SetActive(false);
        WinText.SetActive(false);
        Time.timeScale = 1;
        ProgressPool = 0.5f;
    }

    void Update()
    {
        if (ProgressBar.fillAmount < 0.60f)
        {
            Bar.color = Stage01;
        }
        if (ProgressBar.fillAmount > 0.60f && ProgressBar.fillAmount < 0.75f)
        {
            Bar.color = Stage02;
        } 
        if (ProgressBar.fillAmount > 0.75f)
        {
            Bar.color = Stage03;
        }

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
