using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoBall : MonoBehaviour
{
    bool go;
    float timeScale;
    float tT;
    const float halfPI = Mathf.PI / 2;
    float theta;
    float slope;
    float x, y, z;
    Vector3 pos, normal;
    public Terrain terrain;
    public AudioClip audioClip; //set in the inspector
    private AudioSource audioSource; //set this with GetComponent()

    // Start is called before the first frame update
    void Start()
    {
        go = false;
        timeScale = .25f;
        theta = 15;
        slope = Mathf.Tan(theta * Mathf.Deg2Rad);
        x = 0;
        z = 100f;
        //Exercise 10:  replace the statement below using Terrain's SampleHeight() and GetPosition() 
        //y = slope * z;
        y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrain.GetPosition().y;

        pos = new Vector3(x, y, z);
        transform.position = pos;
        normal = new Vector3(0f, Mathf.Cos(Mathf.Deg2Rad * theta), -Mathf.Sin(Mathf.Deg2Rad * theta));

        //Exercise 10:  maybe need to translate upward ?


        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            go = true;
            audioSource.Play();
        }

        if (go)
        {
            tT += timeScale * halfPI * Time.deltaTime;
            x = 100 * Mathf.Sin(tT);  //parametric equations for a circle with radius 100
            z = 100 * Mathf.Cos(tT);  //starts at x = 0, z = 100 and orbits CW 

            //Exercise 10:  replace the statement below using Terrain's SampleHeight() and GetPosition() 
            //y = slope * z;
            y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrain.GetPosition().y;

            pos = new Vector3(x, y, z);
           
            transform.position = pos;
   
            transform.forward = Vector3.Cross(-pos, normal).normalized;  //forward should be perpendicular to -pos vector and the surface normal vector

            //Exercise 10: use the Quaternion.LookRotation() method to set transform.rotation so that the transform.up vector is aligned with the surface normal
           
            

           //Exercise 10:  translate RoBall so that it just touches the surface


        }

    }
}