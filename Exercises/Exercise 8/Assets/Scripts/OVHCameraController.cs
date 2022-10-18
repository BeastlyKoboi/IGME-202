using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVHCameraController : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Exercise 8
            //move the camera further away from surface
            transform.Translate(0, 1, 0, Space.World);
        }
       

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Exercise 8
            //bring the camera closer to surface
            transform.Translate(0, -1, 0, Space.World);
        }

        

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Exercise 8
            //perform a yaw (CCW rotation about global y axis)
            transform.Rotate(0, 1, 0, Space.World);
        }
        

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Exercise 8
            //perform a yaw (CW rotation about global y axis)
            transform.Rotate(0, -1, 0, Space.World);
        }

    }//end Update()

}//end class ThinAirCopter
