using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishController : MonoBehaviour
{
    private int elevation;
    // Update is called once per frame
   
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            //Exercise 8
            //perform a positive pitch (CW rotation about local x axis)
           transform.Rotate(1, 0, 0, Space.Self);
        }
       
        if (Input.GetKey(KeyCode.S))
        {
            //Exercise 8
            //perform a negative pitch (CCW rotation about local x axis)
            transform.Rotate(-1, 0, 0, Space.Self);
        }

        if (Input.GetKey(KeyCode.A))
        {
            //Exercise 8
            //perform a negative yaw (CCW rotation about global y axis)
            transform.Rotate(0, -1, 0, Space.World);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            //Exercise 8
            //perform a positive yaw (CW rotation about global y axis)
            transform.Rotate(0, 1, 0, Space.World);
        }
 
    }//end Update()

}//end class ThinAirCopter
