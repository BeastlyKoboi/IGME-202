using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinAir : MonoBehaviour
{
    // Fields from write-up
    private float altitude;
    private Vector3 pos;
    private Vector3 vel;

    public GameObject thinAirBalloon;

    /// <summary>
    /// Makes the altitude read-only
    /// </summary>
    public float Altitude
    {
        get { return altitude; }
    }


    // Start is called before the first frame update
    void Start()
    {
        // 
        altitude = 5.5f; //so the bottom of the Gondola just touches the ground
        pos = new Vector3(0, altitude, 0);
        vel = Vector3.zero;
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        // Key reading from write-up
        if (Input.GetKey(KeyCode.Alpha1))
        {
            altitude += .01f;
            pos.y = altitude; //NOTE: this is more efficient than the statement below
                              //pos = new Vector3(transform.position.x, altitude, transform.position.z);
        }
        if (Input.GetKey(KeyCode.Alpha0))
        {
            altitude -= .01f; //should check to make sure this doesn’t make altitude < 0
            pos.y = altitude; //NOTE: this is more efficient than the statement below
                              //pos = new Vector3(transform.position.x, altitude, transform.position.z);
        }

        pos += vel * Time.deltaTime;
        transform.position = pos; //NOTE: transform.position.y cannot be set

    }

    /// <summary>
    /// Method from write-up to adjust balloon's velocity
    /// </summary>
    /// <param name="windSpeed"></param>
    /// <param name="windDirection"></param>
    public void WindVelocity(float windSpeed, float windDirection)
    {
        vel = windSpeed * (new Vector3(Mathf.Sin(windDirection), 0, Mathf.Cos(windDirection)));
    }
}
