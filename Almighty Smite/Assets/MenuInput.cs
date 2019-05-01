using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInput : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)                                                                                                    //om någon knapp är tryckt
        {
            SceneManager.LoadScene("Main Game");                                                                                //starta spelet
        }
    }
}
