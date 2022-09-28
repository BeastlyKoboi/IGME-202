using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private int segments = 1;  //only drawing 1 line segment to go between the origin and the position point of satellite

    public float width;

    public Color color;

    LineRenderer lineRenderer;

    public GameObject calipso; //set in Inspector 

    private Vector3 groundPosition; //where the laser from satellite strikes earth surface

    public float earthRadius; //must be set in Start() 

    void Start()
    {
        color = Color.red;
        width = .08f;
        earthRadius = 50f;
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = (segments + 1);
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.startColor = color;  
        lineRenderer.endColor = lineRenderer.startColor;
        lineRenderer.useWorldSpace = true;

        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, calipso.transform.position);
    }


    void Update()
    {
        //Exercise 6 will require groundPosition to to be set to the surface of the earth,
        //and then used to replace the Vector3.zero in the call to SetPosition() 
        //HINT:  consider a unit vector from origin to the satellite, scaled accordingly so it just breaks the earth's surface . . .
        lineRenderer.SetPosition(0, Vector3.zero);

        lineRenderer.SetPosition(1, calipso.transform.position);

        //Debug.Log("satellite position is " + calipso.transform.position);
    }
}
