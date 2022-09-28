﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject earth; 

    public GameObject calipso; //set in the Inspector

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        //Exercise 6 will require the implementation of keyboard event-driven method calls to change +/- the orbital radius in CalipsoControl, 
        //and the timescale in both CalipsoControl and Octahedron Sphere
    }

    void OnGUI()
    { 
        GUI.color = Color.white;
        GUI.skin.box.fontSize = 20;
        GUI.skin.box.wordWrap = false;

        //note:  must use (int) or else the float digits flicker

        //Exercise 6 will require that latitude and longitude be computed and displayed 

        float phi = Mathf.Asin(calipso.transform.position.y / calipso.transform.position.magnitude) * Mathf.Rad2Deg;

        GUI.Box(new Rect(10, 10, 300, 60), "Elevation Angle " + (int) phi);

        //NOTE:  we can't use this

        //theta = Mathf.Rad2Deg * Mathf.Atan2(calipso.transform.position.z, calipso.transform.position.x);  // 0 <= theta < 360

        //because it is the earth beneath the satellite that is rotating

        //and we can't we use rotation.y, because it isn't an angle, it is a component of a quaternion 

        int theta = (int)earth.transform.rotation.eulerAngles.y;

        GUI.Box(new Rect(10, 60, 300, 60), "Azimuthal Angle " + (theta > 180 ? theta - 360 : theta) );  // -180 < azimuthal angle <= 180  
    }
}