using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInput : MonoBehaviour
{
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
