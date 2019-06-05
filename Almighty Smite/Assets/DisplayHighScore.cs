using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DisplayHighScore : MonoBehaviour
{
    public Text[] scores;
    private string[] LeaderBoards;
    private float LeaveTimeStamp;

    void Awake()
    {
        LeaderBoards = ScoreManaging.GetHighScores();
        string tmpString;
        for (int i = 0; i < LeaderBoards.Length; i++)
        {       
            tmpString = LeaderBoards[i].Replace('&', ' ');
            tmpString = tmpString.Insert(tmpString.IndexOf(' '), "  "); 
            scores[i].text = tmpString;
        }
        if(SceneManager.GetActiveScene().name == "HighScore")
        {
            Invoke("SwitchScene", 8);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            CancelInvoke("SwitchScene");
            SceneManager.LoadScene(0);
        }
    }

    private void SwitchScene()
    {
        SceneManager.LoadScene(0);
    }
}
