using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalManager : MonoBehaviour
{
    public Terrain terrain;

    public GameObject[,] normalVectorList;

    public GameObject normalVector;  //must be set in Inspector to UpAxis prefab reference in Assets > Models

    private const int numNormalVectors = 17; //actually there are (numNormalVectors)^2 of these

    private float x, y, z;

    private Vector3 normal;

    // Start is called before the first frame update
    void Start()
    {

        normalVectorList = new GameObject[numNormalVectors, numNormalVectors];

        for (int k = 0; k < numNormalVectors; k++)
        {
            z = -100f + k * 200f / (numNormalVectors - 1);
            for (int i = 0; i < numNormalVectors; i++)
            {
                x = -100f + i * 200f / (numNormalVectors - 1);
                y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrain.GetPosition().y;
                normalVectorList[k, i] = Instantiate(normalVector, new Vector3(x, y, z), Quaternion.identity);
                normalVectorList[k, i].transform.localScale = new Vector3(5, 5, 5);
                normal = terrain.GetComponent<TerrainCollider>().terrainData.GetInterpolatedNormal((x + 100f) / 200f, (z + 100f) / 200f).normalized;

                normalVectorList[k, i].transform.up = normal;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int k = 0; k < numNormalVectors; k++)
        {
            z = -100f + k * 200f / (numNormalVectors - 1);
            for (int i = 0; i < numNormalVectors; i++)
            {
                x = -100f + i * 200f / (numNormalVectors - 1);
                y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrain.GetPosition().y;

                normalVectorList[k, i].transform.position = new Vector3(x, y, z);

                normal = terrain.GetComponent<TerrainCollider>().terrainData.GetInterpolatedNormal((x + 100f) / 200f, (z + 100f) / 200f).normalized;
                normalVectorList[k, i].transform.up = normal;
            }
        }
    }
}