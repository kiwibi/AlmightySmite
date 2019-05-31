using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    private CameraController CamController;
    private List<Camera> cameras;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;                                                                                                                                 //cursor is not rendered
        Cursor.lockState = CursorLockMode.Locked;
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
    }

    private Vector3 FindMiddleOfScreen(int index)
    {
        Vector3 tmp;                                                                                                                                           //temporary vector3 to store position in
        tmp = cameras[index].ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));                                     //find the middle of the screen
        return tmp;                                                                                                                                            //send back the valuse of the current middlepoint of the screen
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag != "Land" && col.gameObject.tag != "MainCamera")                                                                               //om den inte krockar med någonting vi inte bryr oss om
        {
            //Debug.Log("Highlight City");
        }
    }
}
