using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBehaviour : MonoBehaviour
{
    public GameObject Lightning;                                                                                                                                             //object that will be spawned
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
    }

    // Update is called once per frame
    void Update()
    {
        if (cameras[0].transform.position.x < CamController.MinX || cameras[0].transform.position.x > CamController.MaxX)
        {
            transform.position = FindMiddleOfScreen(1);
        }
        else
        {
            transform.position = FindMiddleOfScreen(0);
        }
        //set the position to the middle of screen
        if (Input.GetKeyUp(KeyCode.V))                                                                                                                                       //if key is released
        {
            Instantiate(Lightning, transform.position, Quaternion.identity);                                                                                                //spawn the lightning at this objects position
            EventManager.TriggerEvent("Lightning");
            Destroy(gameObject);                                                                                                                                            //destroy this (the shadow)
        }
    }

    private Vector3 FindMiddleOfScreen(int index)
    {
        Vector3 tmp;                                                                                                                                                        //temporary vector3 to store position in
        tmp = cameras[index].ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));                                                  //find the middle of the screen
        return tmp;                                                                                                                                                         //send back the valuse of the current middlepoint of the screen
    }
}
