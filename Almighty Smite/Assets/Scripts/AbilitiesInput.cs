using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesInput : MonoBehaviour
{
    [Header("Camera")]
    public Camera cam;
    [Header("Tornado related things")]
    public GameObject tornado;                                                                                          //GameObjectet som ska spawnas vid knapptryckning
    private KeyCode TornadoKey;
    public static bool TornadoSpawned;                                                                                         //säger true eller false ifall en tornado existerar eller inte
    [Header("Earthquake related things")]
    public GameObject earthquake;
    private KeyCode EarthquakeKey;
    private float EarthquakeTimer;
    public static bool EarthquakeSpawned;
    public static bool EarthQuakeDestroy;
    [Header("Lightning related things")]
    public GameObject lightning;
    private KeyCode LightningKey;
    public static bool LightningSpawned;

    public static bool Charging;                                                                                        //en bool som ska säga när playern chargar en ability
    // Start is called before the first frame update
    void Start()
    {
        TornadoKey = KeyCode.Space;                                                                                     //sätter en keycode för enklare återanvänding
        EarthquakeKey = KeyCode.C;                                                                                      //sätter en keycode för enklare återanvänding
        LightningKey = KeyCode.V;
        EarthquakeTimer = 0;

        AbilitiesInput.Charging = false;                                                                                 //vi börjar inte med att charga tornados vid starten så det är false
        AbilitiesInput.TornadoSpawned = false;                                                                                          //i början finns inga tornados så den sätts till false
        AbilitiesInput.EarthquakeSpawned = false;                                                                                       //i början finns inga earthquakes så den sätts till false
        AbilitiesInput.LightningSpawned = false;
        AbilitiesInput.EarthQuakeDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        EarthQuake();                                                                                                  //callar funktionen för att kolla allt med en earthquake

        Tornado();                                                                                                     //callar funktionen som ska ha med tornado spawning att göra

        Lightning();
    }

    private void Tornado()
    {
        if (Input.GetKey(TornadoKey))                                                                                  //kollar ifall tornadoknappen är nertryckt ger true eller false varje frame
        {
            if (TornadoSpawned == false)                                                                               //kollar ifall det redan finns en tornado i gamespacet
            {
                AbilitiesInput.Charging = true;                                                                        //sätter en bool till true för att säga att tornadon laddas upp
                AbilitiesInput.TornadoSpawned = true;                                                                                 //sätter en bool till sant som säger att det finns redan en tornado spawnad
                var clone = Instantiate(tornado, cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, cam.nearClipPlane)), Quaternion.identity);
                //spawnar en tornado på kamerans mittpunkt och med en rotation som i det här momentet är vad den är i prefaben
            }
        }
        else if (Input.GetKeyUp(TornadoKey))                                                                           //kollar om knappen har blivit släppt och ger en true i framen det händer
        {
            AbilitiesInput.Charging = false;                                                                           //sätter boolen som säger att vi inte laddar längre
        }
    }

    private void EarthQuake()
    {
        if (EarthquakeTimer > 0 && AbilitiesInput.EarthquakeSpawned == true)
            EarthquakeTimer -= 2.0f * Time.deltaTime;
        if (EarthquakeTimer < 0)
        {
            AbilitiesInput.EarthQuakeDestroy = true;
            EarthquakeTimer = 0;
        }

        if (Input.GetKeyDown(EarthquakeKey))                                                                                             //Kollar om knappen för earthquakes är nedtryckt
        {
            if (AbilitiesInput.EarthquakeSpawned == false)                                                                               //kollar ifall det redan finns en earthquake
            {
                AbilitiesInput.EarthquakeSpawned = true;                                                                                //säger att det nu finns en earthquake
                var clone = Instantiate(earthquake, cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, cam.nearClipPlane)), Quaternion.identity);
                EarthquakeTimer += 1.0f;
                                                                                                                                        //Spawnar en earthquake i mitten av kameran
            }
            else if(AbilitiesInput.EarthquakeSpawned == true && EarthquakeTimer > 0)
            {
                EarthquakeTimer += 1;
                if (EarthquakeTimer > 1)
                    EarthquakeTimer = 1;
            }
        }
    }

    private void Lightning()
    {
        if(Input.GetKeyDown(LightningKey))
        {
            var clone = Instantiate(lightning, cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, cam.nearClipPlane)), Quaternion.identity);
        }
    }
}
