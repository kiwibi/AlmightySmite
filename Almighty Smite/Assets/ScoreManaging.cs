using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;

public class ScoreManaging : MonoBehaviour
{
    class Score
    {
        public string name_;
        public int score_;
    }
    private static ScoreManaging instance = null;
    public static ScoreManaging Instance { get { return instance; } }
    private int CurrentScore;
    private string CurrentName;
    private List<Score> Highscore;
    private string[] HighscoreStrings;
    private string path;
    
    void Awake()
    {
        if (Instance != this)
        {
            if (Instance == null)
                instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
        }
        instance = this;
        CurrentScore = 0;
        Highscore = new List<Score>();
        DontDestroyOnLoad(gameObject);
        path = Application.persistentDataPath + "Assets";
        ReadFile();
    }

    public static void AddScore(int value)
    {
        instance.CurrentScore += value;
    }

    public static void RemoveScore(int value)
    {
        instance.CurrentScore -= value;
    }

    public static void SaveScore()
    {
        Score tmpScore = new Score
        {
            name_ = instance.CurrentName,
            score_ = instance.CurrentScore
        };
        instance.Highscore.Add(tmpScore);
        instance.Highscore = instance.Highscore.OrderByDescending(x => x.score_).ToList<Score>();
        if (instance.Highscore.Count > 10)
        {
            instance.Highscore.RemoveAt(10);
        }
        instance.Rewrite();
    }

    private void Rewrite()
    {
        Score tmpScore = new Score();
        int index = 0;
        foreach(var score in Highscore)
        {
            string tmpString = score.name_ + "&" + score.score_.ToString();
            HighscoreStrings[index] = tmpString;
            index++;
        }
        try
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            File.WriteAllLines("Assets/HighScore.txt", HighscoreStrings);
        }
        catch (System.Exception ex)
        {
            string ErrorMessages = "File Write Error\n" + ex.Message;
            Debug.LogError(ErrorMessages);
        }
    }

    private void ReadFile()
    {
        try
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            HighscoreStrings = File.ReadAllLines("Assets/HighScore.txt");
        }
        catch (System.Exception ex)
        {
            string ErrorMessages = "File Write Error\n" + ex.Message;
            Debug.LogError(ErrorMessages);
        }
        for (int i = 0; i < HighscoreStrings.Length; i++)
        {
            int namePos = HighscoreStrings[i].IndexOf('&');
            Score tmpScore = new Score
            {
                name_ = HighscoreStrings[i].Substring(0, namePos)
            };
            string subTmpString = HighscoreStrings[i].Substring(namePos + 1);
            int.TryParse(subTmpString, out tmpScore.score_);
            Highscore.Add(tmpScore);
        }
    }

    public static string[] GetHighScores()
    {

        return instance.HighscoreStrings;
    }

    public static void ResetScore()
    {
        instance.CurrentScore = 0;
    }

    public static void SetName(string name)
    {
        instance.CurrentName = name;
    }

    public static int GetScore()
    {
        return instance.CurrentScore;
    }

    public static int GetLowestHighscore()
    {
        return instance.Highscore[9].score_;
    }
}
