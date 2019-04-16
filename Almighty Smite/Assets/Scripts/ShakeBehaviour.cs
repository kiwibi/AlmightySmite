using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehaviour : MonoBehaviour
{
    private float camSize;                                                                                                                 //Variabel som sparar den orginella storleken på kameran
    public static ShakeBehaviour instance;                                                                                                 //variabel för att säga just vilken instance av scriptet vi vill ha

    public static bool isShaking;                                                                                                          //variabel som säger iffal skärmen redan skakar


    void Awake()
    {
        instance = this;                                                                                                                   //instance är just det här scriptet
        ShakeBehaviour.isShaking = false;                                                                                                  //skärmen skaks inte till att börja med

    }

    public static void Shake(float duration, float magnitude)
    {
        ShakeBehaviour.isShaking = true;                                                                                                   //då shake funktionen kallas så sätter vi boolen till true för nu skakar skärmen
        instance.camSize = Camera.main.orthographicSize;                                                                                   //tar den nuvarande storleken
        instance.StartCoroutine(instance.cShake(duration, magnitude));                                                                     //startar funktionen cShake
    }

    public IEnumerator cShake(float duration, float magnitude)
    {

        float endTime = Time.time + duration;                                                                                              //sätter sluttiden till den nuvarande tiden + så länge vi vill att den ska gå

        while (Time.time < endTime)                                                                                                        //medans tiden inte är slut gör det inom nästa { }
        {
            Camera.main.orthographicSize = Random.Range(4.5f, 5.0f);                                                                       //sätter kamerans size till ett nummer mellan 4.5 och 5

            duration -= Time.deltaTime;                                                                                                    //sänker tiden med deltatime

            yield return null;                                                                                                             //hoppar tillbaka upp till början av cShake
        }

        Camera.main.orthographicSize = camSize;                                                                                            //sätter kamerans storlek till den orginella storleken
        ShakeBehaviour.isShaking = false;                                                                                                  //nu skakar kameran inte längre så den sätts till false
    }
}
