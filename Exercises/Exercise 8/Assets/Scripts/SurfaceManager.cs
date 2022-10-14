using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceManager : MonoBehaviour
{
    public Terrain terrain;
    
    float increment;
   
    private bool designerMode;

    private void Start()
    {
        increment = .01f;

        designerMode = true;
        
        gameObject.GetComponent<CameraManager>().enabled = false;
        gameObject.GetComponent<DishManager>().enabled = false;
        gameObject.GetComponent<RandomManager>().enabled = false;
        
    }

    public void Update()
    {
        if (designerMode && Input.GetKeyDown(KeyCode.Escape))
        {
            designerMode = false;
            
            gameObject.GetComponent<RandomManager>().enabled = true;  //this enables the script that generates the Helices

            gameObject.GetComponent<DishManager>().enabled = true;  //this enables the script that generates the Dish Antennas

            gameObject.GetComponent<CameraManager>().enabled = true;  //this enables the script that allows other cameras to become active
            
        }
            
        if (designerMode)
        {

            if (Input.GetKey(KeyCode.W))
            {
                terrain.GetComponent<TerrainGeneration>().ShiftOriginate(0, increment);
            }

            if (Input.GetKey(KeyCode.S))
            {
                terrain.GetComponent<TerrainGeneration>().ShiftOriginate(0, -increment);
            }

            if (Input.GetKey(KeyCode.A))
            {
                terrain.GetComponent<TerrainGeneration>().ShiftOriginate(-increment, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                terrain.GetComponent<TerrainGeneration>().ShiftOriginate(increment, 0);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                terrain.GetComponent<TerrainGeneration>().AdjustHeight(10);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                terrain.GetComponent<TerrainGeneration>().AdjustHeight(-10);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                terrain.GetComponent<TerrainGeneration>().ReRange(false);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                terrain.GetComponent<TerrainGeneration>().ReRange(true);
            }
        }
        
    }

    private void OnGUI()
    {
        
    }

}