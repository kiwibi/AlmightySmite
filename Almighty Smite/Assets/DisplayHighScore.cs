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

    void Start()
    {
        LeaderBoards = ScoreManaging.GetHighScores();
        string tmpString;
        for (int i = 0; i < LeaderBoards.Length; i++)
        {       
            tmpString = LeaderBoards[i].Replace('&', ' ');
            tmpString = tmpString.Insert(tmpString.IndexOf(' '), "  "); 
            scores[i].text = tmpString;
        }
        LeaveTimeStamp = Time.time + 8f;
    }

    // Update is called once per frame
    void Update()
    {
        if(LeaveTimeStamp < Time.time || Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
