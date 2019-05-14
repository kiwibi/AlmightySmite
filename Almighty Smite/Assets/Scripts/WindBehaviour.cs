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
        WindMovement = Vector2.up;
        WindDirection = 0;                                                                                              //sätter vinden till at börja peka mot norr
        WindIndicatorRenderer = GetComponentsInChildren<Image>()[1];                                           //getcomponent av pekarens sprite som ska flyttas och roteras
        instance = this;                                                                                                //klargör att instance är just det här scriptet
    }

    void Update()
    {

        {
            if(Input.GetButton("Up"))
            {
                WindMovement += Vector2.up;
            }
            if (Input.GetButton("Down"))
            {
                WindMovement += Vector2.down;
            }
            if (Input.GetButton("Right"))
            {
                WindMovement += Vector2.right;
            }
            if (Input.GetButton("Left"))
            {
                WindMovement += Vector2.left;
            }
        }                                                                                                            //check for input     
        if (WindMovement != Vector2.zero)
        {
            WindMovement.Normalize();
            Vector3 up = new Vector3(0, 0, -1);
            Quaternion rotation = Quaternion.LookRotation(WindMovement,up);
            rotation.x = 0;
            rotation.y = 0;
            WindIndicatorRenderer.transform.rotation = rotation;
        }
    }

    public static Vector2 GetWindMovement()
    {
        return instance.WindMovement;                                                                                  //ger tillbaka vilken riktning vinder rör sig när funktionen blir kallad
    }
}
