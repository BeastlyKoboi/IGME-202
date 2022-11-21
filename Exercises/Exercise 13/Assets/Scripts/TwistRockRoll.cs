using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistRockRoll : MonoBehaviour
{
    //NOTE:  this script acts on RoBox, which is the parent of RoBall, cyli, and CAPS, and the model axes
    //       RozXy is the parent of RoBox et al, and is affected by the FigureSkater script
    public enum Pose { NORTH, SOUTH, WEST, EAST };

    Pose pose;

    void Start()
    {
        pose = Pose.NORTH;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) //TurnLeft
        {
            switch (pose)
            {
                case Pose.NORTH:
                    transform.Rotate(0f, 0f, +15f, Space.Self);
                    break;

                //Exercise 13 requires that you complete these cases
                 

                case Pose.SOUTH:
                   
                   
                    break;
                case Pose.EAST:
                   
                                  
                    break;
                case Pose.WEST:
                   
                                  
                    break;
                                   
            }
        }

        //Exercise 13 requires that you complete these cases
        if (Input.GetKeyDown(KeyCode.RightArrow)) //TurnRight
        {
            switch (pose)
            {
                
                case Pose.NORTH:
                    transform.Rotate(0f, 0f, -15f, Space.Self);
                    break;

                case Pose.SOUTH:
                   
                                  
                    break;
                case Pose.EAST:
                    
                                   
                    break;
                case Pose.WEST:
                    
                                   
                    break;
                                   
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
            Twist(Pose.WEST);
        if (Input.GetKeyDown(KeyCode.D))
            Twist(Pose.EAST);
        if (Input.GetKeyDown(KeyCode.W))
            Twist(Pose.NORTH);
        if (Input.GetKeyDown(KeyCode.S))
            Twist(Pose.SOUTH);


        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            Twist(pose);
        }
    }

    public void Twist(Pose wayToFace)
    {
        pose = wayToFace;
        switch (pose)
        {
            case Pose.NORTH:
                transform.localRotation = Quaternion.identity;
                break;
            case Pose.SOUTH:
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                break;
            case Pose.EAST:
                transform.localRotation = Quaternion.Euler(0, 90, 0);
                break;
            case Pose.WEST:
                transform.localRotation = Quaternion.Euler(0, -90, 0);
                break;
        }
    }
}

