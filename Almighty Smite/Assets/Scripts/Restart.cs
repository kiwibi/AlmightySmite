using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))                                                                                                                           //om p är nertryckt
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("StartScene");                                                                                                                 //byt till menu scene
        }
    }
}
