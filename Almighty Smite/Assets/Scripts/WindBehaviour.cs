using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindBehaviour : MonoBehaviour
{
                                                                                                                        //privata variabler
    private int WindDirection;
    private Vector2 WindMovement;
    private Image WindIndicatorRenderer;

    [Header("Offset från mitten i väderstjärnan")]                                                                      //public variables
    public float WindIndicatorOffset;                                                                                   //Distans som pekaren är ifrån mitten av kompassen/vindriktningsvisaren
                                                                                                                        //Statiska funktioner som kan nås från vilket script som hellst utan att behöva vara includat
    public static WindBehaviour instance;                                                                               //för att säga att det är just den här verisionen av scriptet som ska användas


    void Start()
    {
        WindDirection = 0;                                                                                              //sätter vinden till at börja peka mot norr
        WindIndicatorRenderer = GetComponentsInChildren<Image>()[1];                                           //getcomponent av pekarens sprite som ska flyttas och roteras
        instance = this;                                                                                                //klargör att instance är just det här scriptet
    }

    void Update()
    {   
        {
            //if (Input.GetKey(KeyCode.Keypad8))                    //nord
            //    WindDirection = 0; 
            //else if (Input.GetKey(KeyCode.Keypad8) && Input.GetKey(KeyCode.Keypad6))               //nordöst
            //    WindDirection = 1;
            //else if (Input.GetKey(KeyCode.Keypad6))               //öst
            //    WindDirection = 2;
            //else if (Input.GetKey(KeyCode.Keypad6) && Input.GetKey(KeyCode.Keypad2))               //sydöst
            //    WindDirection = 3;
            //else if (Input.GetKeyUp(KeyCode.Keypad2))               //syd
            //    WindDirection = 4;
            //else if (Input.GetKey(KeyCode.Keypad2) && Input.GetKey(KeyCode.Keypad4))               //sydväst
            //    WindDirection = 5;
            //else if (Input.GetKey(KeyCode.Keypad4))               //väst
            //    WindDirection = 6;
            //else if (Input.GetKey(KeyCode.Keypad4) && Input.GetKey(KeyCode.Keypad8))               //nordväst
            //    WindDirection = 7;
            if(Input.GetButton("Up"))
            {
                WindMovement += Vector2.up;
            }
            if (Input.GetButton("Down"))
            {
                WindMovement -= Vector2.up;
            }
            if (Input.GetButton("Right"))
            {
                WindMovement += Vector2.right;
            }
            if (Input.GetButton("Left"))
            {
                WindMovement -= Vector2.right;
            }
        }                                                                                                            //check for input                                             
        WindMovement.Normalize();
        Quaternion rotation = Quaternion.LookRotation(WindMovement, new Vector3(0,0,1));
        rotation.x = 0;
        rotation.y = 0;
        WindIndicatorRenderer.transform.rotation = rotation;
        //ChooseDirection();                                                                                             //direction
    }

    private void ChooseDirection()
    {
        switch (WindDirection)
        {
            case 0:                                                //nord
                WindMovement.Set(0, 1);
                //WindIndicatorRenderer.transform.localPosition = new Vector3(0, WindIndicatorOffset);                                                //localPosition = positionen som objektet ska ha beroende på vilken riktning. i det här fallet är det pekaren
                WindIndicatorRenderer.transform.localRotation = Quaternion.Euler(0, 0, 0);                                                          //localRotation = rotationen som objektet ska ha beroende på vilken riktning. i det här fallet är det pekaren 
                break;
            case 1:                                                //nordöst
                WindMovement.Set(1, 1);
                //WindIndicatorRenderer.transform.localPosition = new Vector3(WindIndicatorOffset - 0.5f, WindIndicatorOffset - 0.5f);
                WindIndicatorRenderer.transform.localRotation = Quaternion.Euler(0, 0, 315);
                break;
            case 2:                                                //öst
                WindMovement.Set(1, 0);
                //WindIndicatorRenderer.transform.localPosition = new Vector3(WindIndicatorOffset, 0);
                WindIndicatorRenderer.transform.localRotation = Quaternion.Euler(0, 0, 270);
                break;
            case 3:                                                //sydöst
                WindMovement.Set(1, -1);
                //WindIndicatorRenderer.transform.localPosition = new Vector3(WindIndicatorOffset - 0.5f, -(WindIndicatorOffset - 0.5f));
                WindIndicatorRenderer.transform.localRotation = Quaternion.Euler(0, 0, 225);
                break;
            case 4:                                                //syd
                WindMovement.Set(0, -1);
                //WindIndicatorRenderer.transform.localPosition = new Vector3(0, -WindIndicatorOffset);
                WindIndicatorRenderer.transform.localRotation = Quaternion.Euler(0, 0, 180);
                break;
            case 5:                                                //sydväst
                WindMovement.Set(-1, -1);
                //WindIndicatorRenderer.transform.localPosition = new Vector3(-(WindIndicatorOffset - 0.5f), -(WindIndicatorOffset - 0.5f));
                WindIndicatorRenderer.transform.localRotation = Quaternion.Euler(0, 0, 135);
                break;
            case 6:                                                //väst
                WindMovement.Set(-1, 0);
                //WindIndicatorRenderer.transform.localPosition = new Vector3(-WindIndicatorOffset, 0);
                WindIndicatorRenderer.transform.localRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 7:                                                //nordväst
                WindMovement.Set(-1, 1);
                //WindIndicatorRenderer.transform.localPosition = new Vector3(-(WindIndicatorOffset - 0.5f), WindIndicatorOffset - 0.5f);
                WindIndicatorRenderer.transform.localRotation = Quaternion.Euler(0, 0, 45);
                break;
        }
    }

    public static Vector2 GetWindMovement()
    {
        return instance.WindMovement;                                                                                  //ger tillbaka vilken riktning vinder rör sig när funktionen blir kallad
    }
}
