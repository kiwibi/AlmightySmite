using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class namePicker : MonoBehaviour
{
    //65 -> 90
    public Text[] Letters;
    public Transform medals;
    public GameObject CurrentLetter;
    int currentLetter = 65;
    int LetterIndex;
    float LetterTimeStamp;
    private void Awake()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        LetterIndex = 0;
        for (int i = 0; i < Letters.Length; i++)
        {
            Letters[i].text = ((char)currentLetter).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse Y") != 0)
        {
            if (LetterTimeStamp < Time.time)
                ChangeLetter();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (LetterIndex < 2)
            {
                LetterIndex++;
                currentLetter = 65;
                CurrentLetter.transform.position = new Vector3(CurrentLetter.transform.position.x + 2.5f, CurrentLetter.transform.position.y);
            }
            else
            {
                ScoreManaging.SetName(Letters[0].text + Letters[1].text + Letters[2].text);
                ScoreManaging.SaveScore();
                medals.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }

    private void ChangeLetter()
    {
       
            if (Input.GetAxis("Mouse Y") < 0)
                currentLetter--;
            else
                currentLetter++;
           
            if (currentLetter < 65)
                currentLetter = 90;
            else if (currentLetter > 90)
                currentLetter = 65;

            Letters[LetterIndex].text = ((char)currentLetter).ToString();

        LetterTimeStamp = Time.time + 0.3f;
    }
}
