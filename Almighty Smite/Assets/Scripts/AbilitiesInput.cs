using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesInput : MonoBehaviour
{
    public static AbilitiesInput instance;
    [Header("Camera")]
    public Camera cam;
    [Header("Tornado related things")]
    public GameObject tornado;                                                                                          //GameObjectet som ska spawnas vid knapptryckning
    public static bool TornadoSpawned;                                                                                         //säger true eller false ifall en tornado existerar eller inte
    private float TornadoSpin;
    private float tornadoTimer;
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
    private CameraController CamController;
    private List<Camera> cameras;
    // Start is called before the first frame update

    void Start()
    {
        CamController = GameObject.Find("CameraController").GetComponent<CameraController>();

        cameras = new List<Camera>();
        for (int i = 0; i < Camera.allCameras.Length - 1; i++)
        {
            cameras.Add(Camera.allCameras[i]);
            if (cameras[i].tag != "MainCamera")
            {
                cameras.RemoveAt(i);
            }
        }

        instance = this;
        EarthquakeKey = KeyCode.C;                                                                                      //sätter en keycode för enklare återanvänding
        LightningKey = KeyCode.V;
        EarthquakeTimer = 0;
        TornadoSpin = 0;
        tornadoTimer = 0;

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
        TornadoSpin = Input.GetAxis("Mouse ScrollWheel");
        if (TornadoSpin != 0)
        {
            TornadoTimer();
        }
        if (TornadoTimer() != 0)                                                                                  //kollar ifall tornadoknappen är nertryckt ger true eller false varje frame
        {
            if (TornadoSpawned == false)                                                                               //kollar ifall det redan finns en tornado i gamespacet
            {
                Cooldowns.instance.SetCooldown(4, "Tornado");
                if (cameras[0].transform.position.x + (cameras[0].orthographicSize) < CamController.MinX || cameras[0].transform.position.x + (cameras[0].orthographicSize) > CamController.MaxX)
                {
                    AbilitiesInput.Charging = true;                                                                        //sätter en bool till true för att säga att tornadon laddas upp
                    AbilitiesInput.TornadoSpawned = true;                                                                                 //sätter en bool till sant som säger att det finns redan en tornado spawnad                                                                          //säger att det nu finns en earthquake
                    Instantiate(tornado, cameras[0].ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, cam.nearClipPlane)), Quaternion.identity);
                    Instantiate(tornado, cameras[1].ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, cam.nearClipPlane)), Quaternion.identity);
                    Invoke("StopTornado", 2);
                }
                else
                {
                    AbilitiesInput.Charging = true;                                                                        //sätter en bool till true för att säga att tornadon laddas upp
                    AbilitiesInput.TornadoSpawned = true;                                                                                 //sätter en bool till sant som säger att det finns redan en tornado spawnad
                    var clone = Instantiate(tornado, cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, cam.nearClipPlane)), Quaternion.identity);
                    Invoke("StopTornado", 2);
                    //spawnar en tornado på kamerans mittpunkt och med en rotation som i det här momentet är vad den är i prefaben
                }
            }
        }
        else if(TornadoTimer() == 0)
        {
            StopTornado();
        }
    }

    private float TornadoTimer()
    {
        if (tornadoTimer < 0)
        {
            tornadoTimer += 1 * Time.deltaTime;
            if (tornadoTimer > 0)
                tornadoTimer = 0;
        }
        else if (tornadoTimer > 0)
        {
            tornadoTimer -= 1 * Time.deltaTime;
            if (tornadoTimer < 0)
                tornadoTimer = 0;
        }

        if (TornadoSpin != 0)
        {
            tornadoTimer = 0.5f;
        }
        return tornadoTimer;
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
        else
            AbilitiesInput.EarthQuakeDestroy = false;

        if (Input.GetKeyDown(EarthquakeKey))                                                                                             //Kollar om knappen för earthquakes är nedtryckt
        {
            if (AbilitiesInput.EarthquakeSpawned == false)                                                                               //kollar ifall det redan finns en earthquake
            {
                if (cameras[0].transform.position.x + (cameras[0].orthographicSize) < CamController.MinX || cameras[0].transform.position.x + (cameras[0].orthographicSize) > CamController.MaxX)
                {
                    AbilitiesInput.EarthquakeSpawned = true;                                                                                //säger att det nu finns en earthquake
                    Instantiate(earthquake, cameras[0].ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, cam.nearClipPlane)), Quaternion.identity);
                    Instantiate(earthquake, cameras[1].ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, cam.nearClipPlane)), Quaternion.identity);
                    EarthquakeTimer += 1.0f;
                }
                else
                {
                    AbilitiesInput.EarthquakeSpawned = true;                                                                                //säger att det nu finns en earthquake
                    var clone = Instantiate(earthquake, cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, cam.nearClipPlane)), Quaternion.identity);
                    EarthquakeTimer += 1.0f;
                }                                                                                                                        //Spawnar en earthquake i mitten av kameran
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
        if(Input.GetKeyDown(LightningKey) && Cooldowns.LightningOnCD == false)
        {
            var clone = Instantiate(lightning, cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, cam.nearClipPlane)), Quaternion.identity);
            AbilitiesInput.LightningSpawned = true;
        }
    }

    private void StopTornado()
    {
        AbilitiesInput.Charging = false;                                                                           //sätter boolen som säger att vi inte laddar längre
    }

    public static float TornadoIncrease()
    {
        return instance.TornadoSpin;
    }
}
