using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressbarBehaviour : MonoBehaviour
{
    public Image ProgressBar;
    public float ProgressPool = 0.0f;
    public float AddPoolTMP = 0.1f;

    void Awake()
    {
        ProgressBar.fillAmount = 0;
    }

    void Update()
    {
        ProgressBar.fillAmount = ProgressPool;
        Debug.Log(ProgressPool);        
    }
}
