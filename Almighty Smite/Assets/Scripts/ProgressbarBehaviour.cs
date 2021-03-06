﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressbarBehaviour : MonoBehaviour
{
    public Image ProgressBar;
    public float ProgressPool = 0;
    public bool DEVSWITCH = false;
    public static bool PlayerWin;
    public static bool GameEnd;
    public Image Bar;
    private float SecondScore;
    bool added;
    
    Color Stage01 = new Color(0.0f, 0.85f, 0.0f);
    Color Stage02 = new Color(1.0f, 1.0f, 0.0f);
    Color Stage03 = new Color(1.0f, 0.0f, 0.0f);

    void Awake()
    {
        SecondScore = 0;
        Bar.color = new Color(0, 200, 0);
        Time.timeScale = 1;
        ProgressPool = 0.5f;
        ProgressbarBehaviour.PlayerWin = false;
        ProgressbarBehaviour.GameEnd = false;
        MusicPlayer.Instance.Play();
        added = false;
    }

    void Update()
    {
        if(SecondScore < Time.time && AssistantBehaviour.Tutorial == false)
        {
            ScoreManaging.AddScore(1);
            SecondScore = Time.time + 1;
        }
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
                PlayerWin = false;
                GameEnd = true;
                Time.timeScale = 0;
                StartCoroutine(switchScene());
            }
            if (ProgressPool < 0)
            {
                DestoryDisasters();
                ShakeBehaviour.StopShake();
                if(added == false)
                {
                    ScoreManaging.AddScore(5000);
                    added = true;
                }
                PlayerWin = true;
                GameEnd = true;
                Time.timeScale = 0;
                StartCoroutine(switchScene());
            }
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

    private IEnumerator switchScene()
    {
        yield return new WaitForSecondsRealtime(3);
            SceneManager.LoadScene("HighScore");
    }
}
