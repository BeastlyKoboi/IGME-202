using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
   
{
    //camera array that holds a reference to every camera in the scene
    public Camera[] cameras;

    //current camera
    private int currentCameraIndex;

    //Use this for initialization
    void Start()
    {
        currentCameraIndex = 0;

        //Turn of all cameras, except the first default one
        for(int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
        //if any cameras were added to the controller, enable the first one
        if(cameras.Length > 0)
        {
            cameras[0].gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            cameras[currentCameraIndex].gameObject.SetActive(false);
            currentCameraIndex = 0;
            cameras[currentCameraIndex].gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cameras[currentCameraIndex].gameObject.SetActive(false);
            currentCameraIndex = 1;
            cameras[currentCameraIndex].gameObject.SetActive(true);
        }

    }
}
