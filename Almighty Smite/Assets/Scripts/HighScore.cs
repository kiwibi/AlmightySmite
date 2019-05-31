using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HighScoreList")]
public class HighScore : ScriptableObject
{
    public List<score> ListOfScores = new List<score>();

    public class score
    {
        public int score_;
        public string name_;
    }
}
