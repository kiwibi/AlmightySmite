using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayCurrentScore : MonoBehaviour
{
    int score;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = ScoreManaging.GetScore();
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        score = ScoreManaging.GetScore();
        scoreText.text = score.ToString();
    }
}
