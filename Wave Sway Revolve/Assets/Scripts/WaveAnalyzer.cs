using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAnalyzer : MonoBehaviour
{
    int numberRows = 10;
    int numberColumns = 12;

    //int distance = 2; //distance from one ball's center to a neighboring ball's center, the same here in both horizontal and vertical directions 
    public Vector3 center;  

    private GameObject[,] circles;
    public GameObject circle; //reference to the prefab in Project > Assets > Models, set in the Inspector

    private GameObject[,,] lineSegments;  // [,,0] for horizontal, [,,1] for vertical
    public GameObject lineSegment; //reference to the prefab in Project > Assets > Models, set in the Inspector


    bool drawCircles = false;
    bool drawHorizontalSegments = false;
    bool drawVerticalSegments = false;

    public GameObject[,] orbs;  //set in WaveGenerator after it initializes array

    void Start()
    {

        circles = new GameObject[numberRows,numberColumns];
        lineSegments = new GameObject[numberRows, numberColumns, 2];

        for (int m = 0; m < numberRows; m++)
        {
            for (int n = 0; n < numberColumns; n++)
            {
                //NOTE:  there is an assumption here that the Start() method of WaveGenerator will run before the Start() method of WaveAnalyzer 
                center = orbs[m, n].GetComponent<Orb>().center; //new Vector3(distance * n, distance * m, 0);
                circles[m, n] = Instantiate(circle, center, Quaternion.identity);
                circles[m, n].GetComponent<Circle>().radius = orbs[m, n].GetComponent<Orb>().orbitalRadius; //.1f * (m + 1);
                circles[m, n].SetActive(false);

                for (int k = 0; k < 2; k++)  //k = 0 horizontal, 1 vertical
                {
                    lineSegments[m, n, k] = Instantiate(lineSegment, Vector3.zero, Quaternion.identity);

                    lineSegments[m, n, k].SetActive(false);
                }
            }
        }

    }

    void Update()
    {
        int m, n;
        Vector3 beginPoint, endPoint;

        //NOTE:  the circles don't change, but the endpoints of the line segments do

        if (drawHorizontalSegments)  //the conditional isn't necessary, but its more efficient when SetActive(false) 
        {
            for (m = 0; m < numberRows; m++)
            {
                for (n = 1; n < numberColumns; n++)  //skip n = 0 case for horizontal line segment
                {
                    beginPoint = orbs[m, n - 1].transform.position;
                    endPoint = orbs[m, n].transform.position;
                    lineSegments[m, n, 0].GetComponent<LineSegment>().SetPoints(beginPoint, endPoint);
                }
            }
        }

        if (drawVerticalSegments)   //the conditional isn't necessary, but its more efficient when SetActive(false) 
        {
          
            for (m = 1; m < numberRows; m++) //skip m = 0 case for vertical line segment
            {
                for (n = 0; n < numberColumns; n++)
                {
                    beginPoint = orbs[m - 1, n].transform.position;
                    endPoint = orbs[m, n].transform.position;
                    lineSegments[m, n, 1].GetComponent<LineSegment>().SetPoints(beginPoint, endPoint);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            drawCircles = !drawCircles;
            for (m = 0; m < numberRows; m++)
            {
                for (n = 0; n < numberColumns; n++)
                {
                    circles[m, n].SetActive(drawCircles);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            drawHorizontalSegments = !drawHorizontalSegments;

            for (m = 0; m < numberRows; m++)
            {
                for (n = 0; n < numberColumns; n++)
                {
                    lineSegments[m, n, 0].SetActive(drawHorizontalSegments);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            drawVerticalSegments = !drawVerticalSegments;
            for (m = 0; m < numberRows; m++)
            {
                for (n = 0; n < numberColumns; n++)
                {
                    lineSegments[m, n, 1].SetActive(drawVerticalSegments);
                }
            }
        }
    }  
}