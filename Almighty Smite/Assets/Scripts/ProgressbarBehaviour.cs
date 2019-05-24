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
    
    Color Stage01 = new Color(0, 255, 0);
    Color Stage02 = new Color(255, 255, 0);
    Color Stage03 = new Color(255, 0, 0);

    void Awake()
    {
        //Bar.GetComponent<Image>().color = new Color(0, 255, 0);
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
