using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InclinedPlaneGenerator : MonoBehaviour
{

    private TerrainData myTerrainData;
    public Vector3 worldSize;
    public int resolution; // number of sample points along X and Z axes
    float[,] heightArray;
    float theta;
    public int xSize, ySize, zSize;

    void Start()
    {
        myTerrainData = gameObject.GetComponent<TerrainCollider>().terrainData;
        ySize = 200;
        xSize = 200;
        zSize = 200;
        worldSize = new Vector3(xSize, ySize, zSize);
        myTerrainData.size = worldSize;
        resolution = 257; //a power of two plus one
        myTerrainData.heightmapResolution = resolution;
        heightArray = new float[resolution, resolution];
        theta = 15.0f;
        Slope(theta);
        myTerrainData.SetHeights(0, 0, heightArray);
        transform.position = new Vector3(-xSize/2, -ySize/2 * Mathf.Tan(Mathf.Deg2Rad * theta), -zSize/2); //one needs to draw a diagram to explain why y is set to this
    }


    void Update()
    {


    }

    void Slope(float theta)
    {
        for (int k = 0; k < resolution; k++)  //note that this corresponds to the z direction
            for (int i = 0; i < resolution; i++) //note that this corresponds to the x direction
                heightArray[k, i] = Mathf.Tan(Mathf.Deg2Rad * theta) * (float)k/(resolution - 1);  //note the avoidance of integer division
    }

}