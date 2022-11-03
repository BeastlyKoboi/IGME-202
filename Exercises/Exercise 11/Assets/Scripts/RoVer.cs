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

    float g = 3.71f;  //gravitational acceleration on Mars 
    Vector3 gravity;

    float thrst = 4f;  //this variable added to give vehicle self-propulsion, must be large enough to overcome gravity  NOTE: this is a scalar, magnitude of thrust and brake

    Vector3 thrust, brake;  

    Vector3 gradient, normal;

    public Terrain terrain;

    // Start is called before the first frame update
    void Start()
    {
        x = 0f;
        z = 0f;
        y = terrain.SampleHeight(new Vector3(x, 1000f, z)) + terrain.GetPosition().y; 
        pos = new Vector3(x, y, z);
        transform.position = pos;
        normal = terrain.GetComponent<TerrainCollider>().terrainData.GetInterpolatedNormal((x + 100f) / 200f, (z + 100f) / 200f).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, normal).normalized, normal);
        vel = Vector3.zero;
        acc = Vector3.zero;
        gravity = new Vector3(0, -g, 0);
        thrust = Vector3.zero;
        brake = Vector3.zero;
    }
    
    // Update is called once per frame
    void Update()
    {
        acc = Vector3.zero;  //reset, to start a new update cycle from scratch

        acc = Vector3.Dot(transform.forward, gravity) * transform.forward;

        vel = vel + Time.deltaTime * acc;

        if (Vector3.Dot(vel, transform.forward) <= 0) vel = Vector3.zero;
        //essentially this is like an automatic parking brake that gets applied, which would produce an opposite acceleration to balance gravity
 
        if (Input.GetKey(KeyCode.W))
        {
            SpeedUp();
        }

        if (Input.GetKey(KeyCode.S))
        {
            SlowDown();
        }

        if (Input.GetKey(KeyCode.A))
        {
            TurnLeft();
        }

        if (Input.GetKey(KeyCode.D))
        {
            TurnRight();
        }
        //end of section to provide WASD control code

        x = x + Time.deltaTime * vel.x;
        z = z + Time.deltaTime * vel.z;
        y = terrain.SampleHeight(new Vector3(x,1000f, z)) + terrain.GetPosition().y;
        pos = new Vector3(x, y, z);
        transform.position = pos;

        normal = terrain.GetComponent<TerrainCollider>().terrainData.GetInterpolatedNormal((x + 100f) / 200f, (z + 100f) / 200f).normalized;
        if(vel.magnitude > 0)
          transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(vel, normal).normalized, normal);
    }
    
    public void SpeedUp()  //if (Input.GetKey(KeyCode.W))
    {
        thrust = thrst * transform.forward;
        vel = vel + Time.deltaTime * thrust;
    }

    public void SlowDown()  //if (Input.GetKey(KeyCode.S))
    {
        brake = -thrst * vel.normalized;
        if (vel.magnitude <= thrst * Time.deltaTime)  //to avoid having vehicle move backwards, just stop entirely
        {
            acc = Vector3.zero;
            vel = Vector3.zero;  //but be careful not to set forward vector to zero!
        }
        else
            vel = vel + Time.deltaTime * brake;
    }

    public void TurnLeft() //if (Input.GetKey(KeyCode.A))
    {
        speed = vel.magnitude;
        if (speed >= .02f)
        {
            transform.Rotate(0f, -1f, 0f, Space.Self); //this maintains transform.up, but changes transform.forward and transform.right
            vel = speed * transform.forward;
        }
    }

    public void TurnRight()  //if (Input.GetKey(KeyCode.D))
    {
        speed = vel.magnitude;
        if (speed >= .02f)
        {
            transform.Rotate(0f, 1f, 0f, Space.Self); //this maintains transform.up, but changes transform.forward and transform.right
            vel = speed * transform.forward;
        }
    }

}