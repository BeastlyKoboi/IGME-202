using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureSkater : MonoBehaviour
{

    //NOTE:  This script acts on RozXy, which is the parent of RoBox, which is the parent of RoBall, cyli, and CAPS
    //       RoBox is affected by the TwistRockRoll script
    public enum TurnMode { ACCELERATE_NORMAL, ROTATE };

    TurnMode turnMode;

    public Material blue;

    float aN;  //this is the magnitude of the normal component of acceleration, used when turnMode is set to ACCELERATE_NORMAL
    Vector3 acc;
    Vector3 vel;
    Vector3 pos;
   
    float speed;
    float theTa;
    float turnAngle; //used when turnMode is set to ROTATE
    Vector3 center;
    int radiusIndex;
    bool showOscCircleRadiusCurvature;
    Vector3 pointOnCircle;

    float timeElapsed;

    public GameObject robox; 



    int turning; //0 for straight, 1 for right turn (CW osculating circle) and -1 for left turn (CCW osculating circle) 
    float[] radii = {199, 197, 193, 191, 181, 179, 173, 167, 163, 157, 151, 149, 139, 137, 131, 127, 113, 109, 107, 103, 101,
    97, 89, 83, 79, 73, 71, 67, 61, 59, 53, 47, 43, 41, 37, 31 ,29, 23, 19, 17, 13, 11, 7, 5, 3, 2}; 
    //note that the radiusIndex resembles curvature:  the smaller it gets, the larger the radius of curvature, and vice-versa

    float scale = .25f;  //an adjustment to the radii to match the skater's speed and rink's dimensions; modify as desired based on computer power and desired appearance

    // Start is called before the first frame update
    void Start()
    {
        turnMode = TurnMode.ACCELERATE_NORMAL;
        theTa = 0f;
        center = Vector3.zero;
        turning = 0;

        showOscCircleRadiusCurvature = true;
        radiusIndex = 0;
        for (int i = 0; i < radii.Length; i++)
        {
            radii[i] = scale * radii[i];
        }

        pos = Vector3.zero;   
        vel = radii[0] * Vector3.forward; //just an arbitrary choice for the speed
        speed = vel.magnitude;  //the objective is to maintain a constant speed, regardless of whether skater is moving straight or in a circular arc

        transform.position = pos;
        transform.forward = vel.normalized;
        
        aN = speed * speed / radii[radiusIndex];  //for uniform circular motion, the acceleration has magnitude equal to (speed)^2/R, and is perpendicular to velocity
                                                  //and velocity is perpendicular to the radial position vector   
        timeElapsed = 0;
    }



    void Update()
    {
        turning = 0;


        //Exercise 13 requires that you provide the code to reset radiusIndex to 0 when RozXy is not turning and both Up/Down arrow keys are pressed

        if (Input.GetKey(KeyCode.RightArrow))
        {
                turning = 1;  
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
                turning = -1;
        }

        //select radius from a discrete set of values  
        if (Input.GetKeyDown(KeyCode.UpArrow))  //increase the radius of the osculating circle
        {
            radiusIndex--;
            if (radiusIndex < 0) radiusIndex = 0; //this is the largest radius, smallest radiusIndex
            //Debug.Log("speed is " + speed); //check to see if speed remains constant
            
            aN = speed * speed / radii[radiusIndex];
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))  //decrease the radius of the osculating circle
        {
            radiusIndex++;
            if (radiusIndex > radii.Length - 1) radiusIndex = radii.Length - 1; //this is the smallest radius, largest radiusIndex
            //Debug.Log("speed is " + speed);  //check to see if speed remains constant
            
           aN = speed * speed / radii[radiusIndex];    
        }


        if (turning == 0)  //straight line
        {
            vel = speed * vel.normalized;
            pos += vel * Time.deltaTime;
        }

        else
        {
            switch (turnMode)
            {
                case TurnMode.ACCELERATE_NORMAL:
                    speed = vel.magnitude; //saved before modifying vel
                    acc = turning * aN * transform.right;  //computes an acceleration that acts entirely in Normal direction, no Tangential component
                    //NOTE: whenever radius changes,  aN is recomputed to equal speed*speed/radius, as required for uniform circular motion
                    vel = vel + Time.deltaTime * acc; //NOTE:  this changes both vel direction and speed
                    vel = speed * vel.normalized; //to keep speed constant
                    pos += vel * Time.deltaTime;
                    center = pos + radii[radiusIndex] * turning * transform.right;
                    break;

                case TurnMode.ROTATE:
                    speed = vel.magnitude; //saved before modifying vel
                    //Exercise 13 requires that you fix the following so that TurnMode.ROTATE produces the same motion as TurnMode.ACCELERATE_NORMAL

                    //compute turnAngle based on tan(turnAngle) = deltaTime * ||acc||/||vel||
                    

                    turnAngle = turning * Mathf.Rad2Deg * Mathf.Atan2(Time.deltaTime * (speed * speed / radii[radiusIndex]), speed);  //only works when radiusIndex == 0
                    transform.Rotate(0f, turnAngle, 0f, Space.Self);
                    vel = speed * transform.forward; //to keep speed constant
                    pos += vel * Time.deltaTime;
                    center = pos + radii[radiusIndex] * turning * transform.right;
                    break;
            }
        }
        
        transform.position = pos;
        transform.forward = vel.normalized; 
     
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            showOscCircleRadiusCurvature = !showOscCircleRadiusCurvature;  //used in OnRenderObject() below to display osculating circle and radius of curvature
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            turnMode = TurnMode.ACCELERATE_NORMAL;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
           turnMode = TurnMode.ROTATE;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.DownArrow) && turning == 0)
        {
            radiusIndex = 0;
        }

        timeElapsed += Time.deltaTime;
    }

    private void OnRenderObject()
    {
        if (showOscCircleRadiusCurvature && turning != 0)
        {
            blue.SetPass(0);
            GL.Begin(GL.LINES);
            GL.Vertex3(transform.position.x, transform.position.y + .5f, transform.position.z);
            GL.Vertex3(center.x, .5f, center.z);
            GL.End();
            GL.Begin(GL.LINE_STRIP);
            theTa = 0f;
            for (int i = 0; i < 24; i++)
            {
                pointOnCircle = radii[radiusIndex] * (Mathf.Cos(theTa) * transform.right + Mathf.Sin(theTa) * transform.forward) + center;
                GL.Vertex3(pointOnCircle.x, pointOnCircle.y, pointOnCircle.z);
                theTa += Mathf.PI / 12;
            }
            GL.End();
        }
    }

    void OnGUI()
    {

        GUI.color = Color.white;
        GUI.skin.box.fontSize = 15;
        GUI.skin.box.wordWrap = false;

        //note:  must use (int) or else the float digits flicker

        GUI.Box(new Rect(0, 0, 300, 30), "turnMode: " + turnMode);

        GUI.Box(new Rect(0, 30, 300, 30), "radius: " + (int)radii[radiusIndex]); //NOTE:  the radii are scaled from their initial assigned values in the array declaration

        GUI.Box(new Rect(0, 60, 300, 30), "radius index: " + radiusIndex);

        GUI.Box(new Rect(0, 90, 300, 30), "Turn Direction: " + turning);

        char direction;

        switch (robox.transform.localRotation.y)
        {
            case 0:
                direction = 'N';
                break;
            case 1:
                direction = 'S';
                break;
            case > .707f:
                direction = 'E';
                break;
            case < -.707f:
                direction = 'W';
                break;
            default:
                direction = 'E';
                break;
        }
        

        GUI.Box(new Rect(0, 120, 300, 30), "Facing: " + direction);

        GUI.Box(new Rect(0, 150, 300, 30), "Time Elapsed: " + (int)timeElapsed);



    }

}