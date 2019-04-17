using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{                                                                                //:::::Here be variables:::::
    private Vector2 CameraPosition;                                              //Keeps track of camera's position.
    private Vector2 CameraDirection;                                             //Keeps track of the camera's direction.
    private readonly float MaxX = 19.911f;                                       //This is the eastern border of the map
    private readonly float MinX = -19.911f;                                      //This is the western border of the map
    private readonly float MaxY = 11.2f;                                         //This is the "roof" of the map
    private readonly float MinY = -11.2f;                                        //This is the "floor" of the map
    private readonly float FarMaxX = 37.69f;                                     //This is one camera's space away from the eastern border
    private readonly float FarMinX = -37.69f;                                    //This is one camera's space away from the western border
    private bool LeftCheck = true;                                               //Checks if the camera was just wrapped around on the left side.
    private bool RightCheck = true;                                              //Checks if the camera was just wrapped around on the right side.
    [SerializeField] private float CameraSpeed = 10.0f;                          //Change this variable to change camera movement speed.

    Transform Camera1;                                                           //This variable stores the position of Camera 1
    Transform Camera2;                                                           //This variable stores the position of Camera 2

    void Awake()                                                                 //This happens once the object the script is attached to is instansiated
    {
        Camera1 = transform.Find("Camera 1");                                    //This associates the variable to the game object Camera 1
        Camera2 = transform.Find("Camera 2");                                    //This associates the variable to the game object Camera 2
    }

    void Update()                                                                //This function is called once per frame
    {
        GetInput();                                                              //Calls input function once per frame
        CameraMovement();                                                        //Calls movement function once per frame
        CameraCheck();                                                           //Calls the camera loop function once per frame
    }

    void GetInput()                                                              //This function checks for player input
    {                                                                            //Camera direction is not exclusive to either up, down, left or right.
        CameraDirection = Vector2.zero;                                          //Zeroes the cameras direction
        if (Input.GetKey(KeyCode.W))                                             //Checks if W is being pressed
        {
            if (Camera1.position.y < MaxY && Camera2.position.y < MaxY)          //Checks if the cameras have reached the roof of the map
            {
                CameraDirection += Vector2.up;                                   //Sets direction to up if W is being pressed
            }
        }
        if (Input.GetKey(KeyCode.A))                                             //Checks if A is being pressed
        {
            CameraDirection += Vector2.left;                                     //Sets direction to left if A is being pressed
        }
        if (Input.GetKey(KeyCode.S))                                             //Checks if S is being pressed
        {
            if (Camera1.position.y > MinY && Camera2.position.y > MinY)          //Checks if the cameras have reached the floor of the map
            {
                CameraDirection += Vector2.down;                                 //Sets direction to down if S is being pressed
            }
        }
        if (Input.GetKey(KeyCode.D))                                             //Checks if D is being pressed
        {
            CameraDirection += Vector2.right;                                    //Sets direction to right if D is being pressed
        }
    }

    void CameraMovement()                                                        //This function moves the camera
    {
        transform.Translate(CameraDirection * CameraSpeed * Time.deltaTime);     //Moves the camera according to direction and speed times time in seconds
    }

    void CameraCheck()                                                                                                                          //This Function moves the individual cameras to wrap around the map.
    {
        if (Camera1.position.x > MaxX && RightCheck == true)                                                                                    //Checks if Camera 1 has reached the right edge of the map
        {
            Camera1.transform.SetPositionAndRotation(new Vector3(FarMinX, Camera1.position.y, -10), new Quaternion(0, 0, 0, 0));                //Moves Camera 1 to the opposite side of the map horizontally
            LeftCheck = false;                                                                                                                  //Sets leftcheck to false to avoid conflicts with other if-statements
        }
        if (Camera2.position.x > FarMaxX)                                                                                                       //Checks if Camera 2 has wandered off the right side of the map
        {
            Camera2.transform.SetPositionAndRotation(new Vector3(Camera1.position.x, Camera1.position.y, -10), new Quaternion(0, 0, 0, 0));     //Moves Camera 2 to Camera 1
            LeftCheck = true;                                                                                                                   //Resets LeftCheck
        }
        if (Camera1.position.x < MinX && LeftCheck == true)                                                                                     //Checks if Camera 1 has reached the left edge of the map
        {
            Camera1.transform.SetPositionAndRotation(new Vector3(FarMaxX, Camera1.position.y, -10), new Quaternion(0, 0, 0, 0));                //Moves Camera 1 to the opposite side of the map horizontally
            RightCheck = false;                                                                                                                 //Sets rightcheck to false to avoid conflicts
        }
        if (Camera2.position.x < FarMinX)                                                                                                       //Checks if Camera 2 has wandered of the left side of the map
        {
            Camera2.transform.SetPositionAndRotation(new Vector3(Camera1.position.x, Camera1.position.y, -10), new Quaternion(0, 0, 0, 0));     //Moves Camera 2 to Camera 1
            RightCheck = true;                                                                                                                  //Resets RightCheck
        }
    }
}
