using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveyManager : MonoBehaviour
{
    public Terrain terrain;
    float increment;
  
    bool surveyingMode;

    public GameObject RoBox, RoBall, cyli, CAPS;
    public GameObject RoVer;

    public Camera[] selfies;
    public Camera activeSelfie; 

    // Start is called before the first frame update
    void Start()
    { 
        increment = .01f;

        surveyingMode = false; //NOTE:  this is the flip side to the boolean variable designerMode that was used in Tworound 
        RoVer.SetActive(false);
        RoBox.SetActive(false);
        RoBall.SetActive(false);
        cyli.SetActive(false);
        CAPS.SetActive(false);

        foreach (Camera cam in selfies)
        {
            cam.enabled = false;
        }

        //foreach (Camera cam in selfies)
        //{
        //    cam.gameObject.SetActive(false);
        //}
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            surveyingMode = true;
            RoVer.SetActive(true);
            RoBox.SetActive(true);
            RoBall.SetActive(true);
            cyli.SetActive(true);
            CAPS.SetActive(true);
            GameObject[,] normalVectorList = GetComponent<NormalManager>().normalVectorList;
            foreach (GameObject norm in normalVectorList)
            {
                norm.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            RoBox.SetActive(true);
            
            if (activeSelfie != null)
            {
                if (activeSelfie == selfies[4])
                {
                    activeSelfie.enabled = !activeSelfie.enabled;
                }
                else
                {
                    activeSelfie.enabled = false;
                    selfies[4].enabled = true;
                }
            }
            else
            {
                selfies[4].enabled = true;
            }

            activeSelfie = selfies[4];

            //selfies[4].gameObject.SetActive(true);
            //if (activeSelfie != null)
            //{
            //    if (activeSelfie == selfies[4])
            //    {
            //        activeSelfie.gameObject.SetActive(!activeSelfie.enabled);
            //    }
            //    else
            //    {
            //        activeSelfie.gameObject.SetActive(false);
            //    }
            //}
            //activeSelfie = selfies[4];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            RoBall.SetActive(true);

            if (activeSelfie != null)
            {
                if (activeSelfie == selfies[3])
                {
                    activeSelfie.enabled = !activeSelfie.enabled;
                }
                else
                {
                    activeSelfie.enabled = false;
                    selfies[3].enabled = true;
                }
            }
            else
            {
                selfies[3].enabled = true;
            }

            activeSelfie = selfies[3];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cyli.SetActive(true);

            if (activeSelfie != null)
            {
                if (activeSelfie == selfies[2])
                {
                    activeSelfie.enabled = !activeSelfie.enabled;
                }
                else
                {
                    activeSelfie.enabled = false;
                    selfies[2].enabled = true;
                }
            }
            else
            {
                selfies[2].enabled = true;
            }

            activeSelfie = selfies[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CAPS.SetActive(true);

            if (activeSelfie != null)
            {
                if (activeSelfie == selfies[1])
                {
                    activeSelfie.enabled = !activeSelfie.enabled;
                }
                else
                {
                    activeSelfie.enabled = false;
                    selfies[1].enabled = true;
                }
            }
            else
            {
                selfies[1].enabled = true;
            }

            activeSelfie = selfies[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && 
            RoBox.activeSelf && 
            RoBall.activeSelf && 
            cyli.activeSelf && 
            CAPS.activeSelf)
        {
            RoVer.SetActive(true);

            if (activeSelfie != null)
            {
                if (activeSelfie == selfies[0])
                {
                    activeSelfie.enabled = !activeSelfie.enabled;
                }
                else
                {
                    activeSelfie.enabled = false;
                    selfies[0].enabled = true;
                }
            }
            else
            {
                selfies[0].enabled = true;
            }

            activeSelfie = selfies[0];
        }



        if (!surveyingMode)
        { 
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
        }
    }
}