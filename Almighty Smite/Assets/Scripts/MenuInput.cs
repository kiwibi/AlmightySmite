using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInput : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        MusicPlayer.Instance.Stop();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.C))                                                                                                    //om någon knapp är tryckt
        {
            ScoreManaging.ResetScore();
            SceneManager.LoadScene("Main Game");                                                                                //starta spelet
        }
    }
}
