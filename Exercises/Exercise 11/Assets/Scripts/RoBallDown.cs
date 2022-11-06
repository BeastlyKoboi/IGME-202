using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoBallDown : MonoBehaviour
{
    Vector3 pos;
    Vector3 vel;

    float speed;
    float x, y, z;

    Vector3 grad, normal;

    public Terrain terrain;

    bool onBoundary;

    // Start is called before the first frame update
    void Start()
    {
        x = 0f;
        z = 0f;
        y = terrain.SampleHeight(new Vector3(x, 0f, z)) + terrain.GetPosition().y;
        pos = new Vector3(x, y, z);
        transform.position = pos;

        vel = Vector3.zero;
        speed = 32f;

        onBoundary = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Exercise 11 requires this robot to stop searching at the boundary
        if (x > 100 || x < -100 || z > 100 || z < -100)
        {
            onBoundary = true;
        }

        if (!onBoundary)
        {
            normal = terrain.GetComponent<TerrainCollider>().terrainData.GetInterpolatedNormal((x + 100f) / 200f, (z + 100f) / 200f);

            //grad = Vector3.ProjectOnPlane(normal, new Vector3(0f,1f,0f); //projection of the normal vector onto the x-z plane
            grad = new Vector3(normal.x, 0f, normal.z); //equivalent to the above, more efficient and precise

            //only one of the following four should be uncommented, based on the desired behavior (e.g. go higher, go lower, stay level and go right, stay level and go left)  

            vel = speed * grad; //direction of steepest descent 
            //vel = speed * (new Vector3(-grad.x, 0f, -grad.z));  //opposite to the vel in previous statement                                                                     
            //vel = speed * (new Vector3(-grad.z, 0f, grad.x));  //perpendicular to both of the above
            //vel = speed * (new Vector3(grad.z, 0f, -grad.x));  //opposite to the vel in previous statement

            x = x + Time.deltaTime * vel.x;
            z = z + Time.deltaTime * vel.z;

            y = terrain.SampleHeight(new Vector3(x, 0f, z)) + terrain.GetPosition().y;

            transform.position = new Vector3(x, y, z);

            normal = terrain.GetComponent<TerrainCollider>().terrainData.GetInterpolatedNormal((x + 100f) / 200f, (z + 100f) / 200f).normalized;

            transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, normal).normalized, normal);

            transform.Translate(0, 1, 0, Space.Self);
        }
    }

      
    
}
