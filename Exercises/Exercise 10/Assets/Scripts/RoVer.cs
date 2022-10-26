using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoVer : MonoBehaviour
{
    Vector3 pos;
    Vector3 vel;
    Vector3 acc;

    float speed;
    float x, y, z;

    float g = 3.71f;  //as it would be on the planet mars
    Vector3 gravity;
    bool zeroG;
    float thrustMagnitude = 5f;
    //thrustMagnitude represents the magnitude of acceleration due to the self-propulsion;  it should be large enough to overcome
    //gravity.  Also, we will always set the propulsive acceleration vector to be aligned with velocity
    //as such, this can be considered capable of increasing Rover's current speed, by applying a push (or pull), in the direction of its current velocity 

    Vector3 gradient;
    Vector3 normal;

    public Terrain terrain;

    float slope;

    float theta;

  
    void Start()
    {

        //The inclined plane will satisfy the explicit equation: y = slope * z, or the implicit equation 0 * x + 1 * y - slope * z = 0 (note that there is no dependency on x)
        //
        //which can be rewritten as 0 * x + cos(theta) * y - sin(theta) * z = 0   since slope is in fact equal to tan(theta) = sin(theta)/cos(theta)
        //
        //now recall the point-normal form of a plane equation, which in vector form is:   N DOT (X - P ) = 0, or which can be expanded into:
        //
        //<nx, ny, nz> DOT (<x, y, z> - <px, py, pz>) = 0 
        //
        //by using normal N = <nx, ny, nz> and with X = <x, y, z> and the point in the plane P = <px, py, pz> :   

        theta = 15f;  //angle of inclination of the plane
        slope = Mathf.Tan(Mathf.Deg2Rad * theta); //tan(theta) = sin(theta)/cos(theta)

        //so the vector V = <0, sin, cos> would be in the yz plane and be parallel to the inclined plane
        //
        //thus the normal vector is the "upward-pointing" normal to the plane N = <0, cos, -sin>
        //
        //and so the equation for the inclined plane is <0, cos, -sin> DOT <x, y, z> = 0, since P is the origin = <0, 0, 0>
        //
        normal = new Vector3(0f, Mathf.Cos(Mathf.Deg2Rad * theta), -Mathf.Sin(Mathf.Deg2Rad * theta));

        //the gradient vector is a unit vector that points in direction of steepest descent,
        //
        //and in this case (a surface with constant slope) it is the opposite of the unit vector V = <0, sin(theta), cos(theta)>
        //
        //that lies in the zy plane and points upwards making an angle of theta with the z axis
        //

        gradient = new Vector3(0f, -Mathf.Sin(Mathf.Deg2Rad * theta), -Mathf.Cos(Mathf.Deg2Rad * theta));  
       
        x = 0f;
        z = 0f;
        //Exercise 10:  replace the statement below using Terrain's SampleHeight() and GetPosition() 
        y = slope * z;
        
        pos = new Vector3(x, y, z);

        transform.position = pos;

      
        transform.rotation = Quaternion.LookRotation(gradient, normal);
        //NOTE: this does the same as "simultaneously" setting the following
        //transform.up = normal; 
        //transform.forward = gradient; 

        gravity = g * Vector3.down;
        zeroG = false;
        
        vel = Vector3.zero;
        acc = Vector3.zero;
    }

    void Update()
    {
        if(zeroG)
        {
            acc = Vector3.zero;
        }    
        else
        {
            //project gravity onto the forward (unit) vector
            
            acc = Vector3.Dot(transform.forward, gravity) * transform.forward;
            
            //NOTE:  calculation below is equal to the calculation above, but takes advantage of the x and z components of gravity being 0
           
            //acc = (-g * transform.forward.y) * transform.forward; 
        }

        vel = vel + Time.deltaTime * acc;

        //NOTE: pos is not updated yet, since vel might still be further modified by WASD or Arrow key presses

      
        //Exercise 10:  add propulsion (thrust)
        if (Input.GetKey(KeyCode.W))
        {
            
        }
       
        //produce an acceleration that slows RoVer down (a deceleration)
        if (Input.GetKey(KeyCode.S))
        {
            acc = -thrustMagnitude * vel.normalized;
            if (vel.magnitude <= thrustMagnitude * Time.deltaTime)  //to avoid having vehicle move backwards, just stop entirely
            {
                acc = Vector3.zero;
                vel = Vector3.zero;
            }
            else
                vel = vel + Time.deltaTime * acc;
        }

        //produce an acceleration that turns RoVer left
        if (Input.GetKey(KeyCode.A))
        {
            speed = vel.magnitude;
            acc = -speed * transform.right;
            vel = vel + Time.deltaTime * acc;
            vel = speed * vel.normalized;
        }

        //produce an acceleration that turns RoVer right
        if (Input.GetKey(KeyCode.D))
        {
            speed = vel.magnitude;
            acc = speed * transform.right;
            vel = vel + Time.deltaTime * acc;
            vel = speed * vel.normalized;
        }
  

        //we don't apply pos = pos + Time.deltaTime * vel because we want to keep pos.y on the surface
        //so instead we apply the vel only to x and z
        
        x = x + Time.deltaTime * vel.x;
        z = z + Time.deltaTime * vel.z;

        //Exercise 10:  replace the statement below using Terrain's SampleHeight() and GetPosition() 
        y = slope * z;
        

        pos = new Vector3(x, y, z);
        transform.position = pos;

        //we want to keep RoVer aligned with the velocity vector and parallel to the inclined plane

        if(vel.sqrMagnitude > .02) //this is done so as to avoid problems when the speed is close to zero, hence vel is close to Vector3.zero
          transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(vel, normal).normalized, normal);

    }
}