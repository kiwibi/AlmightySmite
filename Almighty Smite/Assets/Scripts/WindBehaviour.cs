using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBehaviour : MonoBehaviour
{
    private int WindDirection;
    private Vector2 WindMovement;
    //[Tooltip("nord först \n medsols runt kompassen")]
    //public Sprite[]WindIndicatorSprites;
    private SpriteRenderer WindIndicatorRenderer;
    [Header("Offset från mitten i väderstjärnan")]
    public float WindIndicatorOffset;

    public static WindBehaviour instance;

    void Start()
    {
        WindDirection = 0;
        WindIndicatorRenderer = GetComponentsInChildren<SpriteRenderer>()[1];
        instance = this;
        
    }

    void Update()
    {
        {
            if (Input.GetKeyUp(KeyCode.Keypad8))                    //nord
                WindDirection = 0; 
            else if (Input.GetKeyUp(KeyCode.Keypad9))               //nordöst
                WindDirection = 1;
            else if (Input.GetKeyUp(KeyCode.Keypad6))               //öst
                WindDirection = 2;
            else if (Input.GetKeyUp(KeyCode.Keypad3))               //sydöst
                WindDirection = 3;
            else if (Input.GetKeyUp(KeyCode.Keypad2))               //syd
                WindDirection = 4;
            else if (Input.GetKeyUp(KeyCode.Keypad1))               //sydväst
                WindDirection = 5;
            else if (Input.GetKeyUp(KeyCode.Keypad4))               //väst
                WindDirection = 6;
            else if (Input.GetKeyUp(KeyCode.Keypad7))               //nordväst
                WindDirection = 7;
        }                                                         //check for input                                             

        ChooseDirection();                                          //direction
    }

    private void ChooseDirection()
    {
        switch (WindDirection)
        {
            case 0:                                                //nord
                WindMovement.Set(0, 1);
                WindIndicatorRenderer.transform.localPosition = new Vector3(0, WindIndicatorOffset);
                WindIndicatorRenderer.transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
            case 1:                                                //nordöst
                WindMovement.Set(1, 1);
                WindIndicatorRenderer.transform.localPosition = new Vector3(WindIndicatorOffset - 0.5f, WindIndicatorOffset - 0.5f);
                WindIndicatorRenderer.transform.localRotation = Quaternion.Euler(0, 0, 315);
                break;
            case 2:                                                //öst
                WindMovement.Set(1, 0);
                WindIndicatorRenderer.transform.localPosition = new Vector3(WindIndicatorOffset, 0);
                WindIndicatorRenderer.transform.localRotation = Quaternion.Euler(0, 0, 270);
                break;
            case 3:                                                //sydöst
                WindMovement.Set(1, -1);
                WindIndicatorRenderer.transform.localPosition = new Vector3(WindIndicatorOffset - 0.5f, -(WindIndicatorOffset - 0.5f));
                WindIndicatorRenderer.transform.localRotation = Quaternion.Euler(0, 0, 225);
                break;
            case 4:                                                //syd
                WindMovement.Set(0, -1);
                WindIndicatorRenderer.transform.localPosition = new Vector3(0, -WindIndicatorOffset);
                WindIndicatorRenderer.transform.localRotation = Quaternion.Euler(0, 0, 180);
                break;
            case 5:                                                //sydväst
                WindMovement.Set(-1, -1);
                WindIndicatorRenderer.transform.localPosition = new Vector3(-(WindIndicatorOffset - 0.5f), -(WindIndicatorOffset - 0.5f));
                WindIndicatorRenderer.transform.localRotation = Quaternion.Euler(0, 0, 135);
                break;
            case 6:                                                //väst
                WindMovement.Set(-1, 0);
                WindIndicatorRenderer.transform.localPosition = new Vector3(-WindIndicatorOffset, 0);
                WindIndicatorRenderer.transform.localRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 7:                                                //nordväst
                WindMovement.Set(-1, 1);
                WindIndicatorRenderer.transform.localPosition = new Vector3(-(WindIndicatorOffset - 0.5f), WindIndicatorOffset - 0.5f);
                WindIndicatorRenderer.transform.localRotation = Quaternion.Euler(0, 0, 45);
                break;
        }
    }

    public static Vector2 GetWindMovement()
    {
        return instance.WindMovement;
    }
}
