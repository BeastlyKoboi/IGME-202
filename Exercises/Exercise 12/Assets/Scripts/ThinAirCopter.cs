using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinAirCopter : MonoBehaviour
{

    //public enum LiftMode { LOW, MEDIUM, HIGH };

    private Vector3 pos;
    private Vector3 vel;
    private float windSpeed;
    private float extraPropulsion; //added to liftAgainstGravity in HIGH speed
    private float liftAgainstGravity; //liftAgainstGravity equals MEDIUM speed
    private float altitude;
    private float phi;

    RotorSpinner rotorSpinner;

    Quaternion qYawPlus, qYawMinus;
    Quaternion qPitchPlus, qPitchMinus;
    Quaternion qRollPlus, qRollMinus;

    private void Start()
    {
        extraPropulsion = 2; //added to liftAgainstGravity in HIGH speed
        liftAgainstGravity = 8; //liftAgainstGravity in MEDIUM speed

        windSpeed = extraPropulsion + liftAgainstGravity;
        phi = Mathf.Acos(liftAgainstGravity/(windSpeed)); //liftAgainstGravity is vertical leg, windSpeed is hypotenuse
        //phi is the maximum angle that Ingenuity can tilt (pitch or roll) so as to translate along the Mars surface while maintaining constant altitude
        //assuming that the blades are spinning at HIGH speed
        //values in between 0 and phi would produce upwards and forwards velocity components that would cause a cerain amount of altitude to be gained while
        //also translating, albeit at less than the speed obtained at the angle phi

        qYawMinus = Quaternion.AngleAxis(-.4f, Vector3.up);
        qYawPlus = Quaternion.AngleAxis(+.4f, Vector3.up); 
        qPitchMinus = Quaternion.AngleAxis(-phi * Mathf.Rad2Deg, Vector3.right);
        qPitchPlus = Quaternion.AngleAxis(+phi * Mathf.Rad2Deg, Vector3.right);
        qRollMinus = Quaternion.AngleAxis(-phi * Mathf.Rad2Deg, Vector3.forward);
        qRollPlus = Quaternion.AngleAxis(+phi * Mathf.Rad2Deg, Vector3.forward);

        altitude = 0f;
        pos = new Vector3(0, altitude, 0);
        vel = Vector3.zero;
        
        transform.position = pos;
        rotorSpinner = gameObject.GetComponent<RotorSpinner>();
        rotorSpinner.setSpin(RotorSpinner.LiftMode.MEDIUM);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //perform a pitch (CW rotation about local x axis)
            //and produce a velocity in the direction of the up vector (local y axis) projected onto the global xz plane, which has as its normal vector (0,1,0)

            //transform.Rotate(phi * Mathf.Rad2Deg, 0f, 0f, Space.Self);
            transform.rotation = transform.rotation * qPitchPlus;

            rotorSpinner.setSpin(RotorSpinner.LiftMode.HIGH);

            vel = Vector3.ProjectOnPlane(windSpeed * transform.up, Vector3.up); 
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            //perform a pitch (CW rotation about local x axis) to restore orientation and halt motion
            //transform.Rotate(-phi * Mathf.Rad2Deg, 0f, 0f, Space.Self);
            transform.rotation = transform.rotation * qPitchMinus;
           
            rotorSpinner.setSpin(RotorSpinner.LiftMode.MEDIUM);

            vel = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //similar to W key but pitch is CCW and vel is in the reverse direction

             
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            //perform a pitch (CW rotation about local x axis) to restore orientation and halt motion

           
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
           
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
           
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
           
        }

        if (Input.GetKeyDown(KeyCode.R))
        {

        }

        if (Input.GetKeyUp(KeyCode.R))
        { 
            
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
           
        }

        if (Input.GetKey(KeyCode.Q))
        {
          
        }

        if (Input.GetKey(KeyCode.E))
        {
           
        }

        pos += vel * Time.deltaTime;
        transform.position = pos;

    }//end Update()

}//end class ThinAirCopter
