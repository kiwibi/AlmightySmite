using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuakeBehaviour : MonoBehaviour
{
    public float ShakeDuration;                                                                                                                         //Variabel för hur länge skärmen ska skaka
    public float ShakeMagnitude;                                                                                                                        //variabel för hur starkt skärmen ska skaka
    public float GrowthRate;                                                                                                                            //variabel för hur snabbt jordbävningen växer

    private CircleCollider2D QuakeCollider;                                                                                                             //variabel för att kunna komma åt collidern på objektet
    // Start is called before the first frame update
    void Start()
    {
        QuakeCollider = GetComponent<CircleCollider2D>();                                                                                               //tar den collidern vi vill göra saker med

        if(ShakeBehaviour.isShaking == false)                                                                                                           //ser till så att kameran inte redan skakar
            ShakeBehaviour.Shake(ShakeDuration, ShakeMagnitude);                                                                                        //startar skaka skärmen med längden och styrkan som sätts i unity

        EventManager.TriggerEvent("Earthquake");
    }

    // Update is called once per frame
    void Update()
    {
        if (AbilitiesInput.EarthQuakeDestroy == true)
        {
            AbilitiesInput.EarthQuakeDestroy = false;
            Destroy(gameObject);
        }
        QuakeCollider.radius += GrowthRate * Time.deltaTime;                                                                                                            //öka storleken på jordbävning lite varje frame


    }

    void OnDestroy()
    {
        EventManager.TriggerEvent("StopSound");
        ShakeBehaviour.StopShake();
        AbilitiesInput.EarthquakeSpawned = false;                                                                               //när jorbävningen dör ut så sätts boolen till false för det finns ingen tornado spawnad längre                                              
    }
}
