using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;

public class ScoreManaging : MonoBehaviour
{
    class score
    {
        public string name_;
        public int score_;
    }
    private static ScoreManaging instance = null;
    public static ScoreManaging Instance { get { return instance; } }
    private int CurrentScore;
    private string CurrentName;
    private List<score> Highscore;
    private string[] HighscoreStrings;
    private float timeBonus;
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
        Highscore = new List<score>();
        DontDestroyOnLoad(gameObject);
        path = Application.persistentDataPath + "Assets";

        timeBonus = 0;
        ReadFile();
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Main Game")
        {
            timeBonus += Time.deltaTime;
        }
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
        score tmpScore = new score();
        tmpScore.name_ = instance.CurrentName;
        int tmpBonus = Mathf.RoundToInt(300f - instance.timeBonus);
        tmpScore.score_ = instance.CurrentScore + tmpBonus;
        instance.Highscore.Add(tmpScore);
        instance.Highscore = instance.Highscore.OrderByDescending(x => x.score_).ToList<score>();
        if (instance.Highscore.Count > 10)
        {
            instance.Highscore.RemoveAt(10);
        }
        instance.Rewrite();
    }

    private void Rewrite()
    {
        score tmpScore = new score();
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
            score tmpScore = new score();
            tmpScore.name_ = HighscoreStrings[i].Substring(0, namePos);
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
        return instance.CurrentScore + Mathf.RoundToInt(300f - instance.timeBonus);
    }
}
