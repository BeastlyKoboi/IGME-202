using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorSpinner : MonoBehaviour
{

    public enum LiftMode { LOW, MEDIUM, HIGH };
    //LOW setting for slowly descending and idling on ground
    //MEDIUM setting for producing lift that equals gravity
    //HIGH setting for producing lift greater than gravity, to gain altitude or for maintaining altitude while also moving in a direction that's parallel to the xz plane

   // LiftMode spin;
    public GameObject rotorCCW;
    public GameObject rotorCW;
    
    private float spinSpeed;

    
    void Start()
    {
        setSpin(LiftMode.MEDIUM); 
    }
    
    void Update()
    {
        rotorCCW.transform.Rotate(spinSpeed * Time.deltaTime, 0, 0);
        rotorCW.transform.Rotate(spinSpeed * Time.deltaTime, 0, 0);
    }

    public void setSpin(LiftMode mode)
    {

        switch (mode)
        {
            case LiftMode.LOW:
                spinSpeed = 1000/2f;
                break;
            case LiftMode.MEDIUM:
                spinSpeed = 2000/2f;
                break;
            case LiftMode.HIGH:
                spinSpeed = 3000/2f;
                break;
        }

        
    }
}
