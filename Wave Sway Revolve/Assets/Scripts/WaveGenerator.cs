using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{

    public GameObject waveAnalyzer;  //to be set in the Inspector

    public GameObject orb;  //reference to the prefab in Project > Assets > Models, to be set in the Inspector

    public GameObject[,] orbs;  //rows x columns
    const int numberRows = 10;
    const int numberColumns = 12;

    int distance = 2; //distance from one ball's center to a neighboring ball's center, the same spacing in both horizontal and vertical directions 

    // Start is called before the first frame update - NOTE: the script execution order needs to be set explicitly in the Unity editor
    void Start()
    {
        orbs = new GameObject[numberRows, numberColumns];
        waveAnalyzer.GetComponent<WaveAnalyzer>().orbs = orbs;

        for (int m = 0; m < numberRows; m++)
        {
            for (int n = 0; n < numberColumns; n++)
            {
                //orbs[m, n] = (GameObject)Instantiate(orb, Vector3.zero, Quaternion.identity); 
                //NOTE:  there is a discrepancy between the Unity API docs and what is seen when mouse "hovers", which shows return value is GameObject
                orbs[m, n] = Instantiate(orb);
                orbs[m, n].GetComponent<Orb>().center = new Vector3(distance * n, distance * m, 0);
                orbs[m, n].GetComponent<Orb>().phaseAngle = (2 * Mathf.PI / numberColumns) * n; //phase differs by 30 degrees from column to column
                orbs[m, n].GetComponent<Orb>().orbitalRadius = .1f * (m + 1); //chosen so that the radii changes linearly with depth; .1 is just an arbitrary pick
                //alternatively, we could write setter methods in Orb for the center, phaseAngle, and orbitalRadius and call them here
            }
        }
    }

    //NOTE: no need for Update() since Orb script has its own Update()
   
}



