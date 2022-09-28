using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalipsoControl : MonoBehaviour
{
    private float tT;
    
    public float radius;  //Exercise 6 requires writing code to control this using up/down arrow key to +/- orbital radius
    private float orbitsPerUnitTime; 
    public float timeScale;  //Exercise 6 requires writing code to control this using left/right arrow keys to -/+ timeScale
    private Vector3 acc, vel, pos;  //NOTE:  we are not using acc and vel here

    void Start()
    {
        tT = 0;
        radius = 100f;
        orbitsPerUnitTime = 16;
        timeScale = 1/16f; 
        pos = radius * Vector3.up;  //this version starts at the north pole
        transform.position = pos;
        transform.up = -transform.position.normalized;//- sign is there because the .fbx 3D model from NASA has the "laser-emitting side" with outward normal in the +y coordinate
        //vel = radius * (orbitsPerUnitTime) * Vector3.forward;  //this is the tangential velocity vector for uniform circular motion
    }

    // Update is called once per frame
    void Update()
    {
        tT += Time.deltaTime * timeScale;

        //NOTE: this version starts at the north pole
        //Exercise 6 requires that the satellite starts at the south pole, and so thereafter it will always be opposite to the position as given here
        //this parametric curve makes CALIPSO revolve in a circular orbit within the y-z plane

        pos = radius * (new Vector3(0, Mathf.Cos(orbitsPerUnitTime * tT), Mathf.Sin(orbitsPerUnitTime * tT))); 
        transform.position = pos;

        //the orbiting satellite must continually be re-oriented to keep the laser pointed towards earth; rather than perform a rotation requiring a precisely calculated angle,
        //we can simply observe that CALIPSO's local y axis must be parallel to the vector that runs from the center of the earth to the position of the satellite
        //the minus sign is here because the .fbx 3D CALIPSO model from NASA has its "laser-emitting side" in the model's (local) positive y coordinate direction
        transform.up = -transform.position.normalized;
    }
   
}
