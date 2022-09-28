using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject arrow1;  //fill in by Inspector
    private Arrow1 arrow1_script;
     
    // Start is called before the first frame update
    void Start()
    {
        arrow1_script = arrow1.GetComponent<Arrow1>();
    }

    // Update is called once per frame
    void Update()
    {
       
        // Reads up and down arrow keys 
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            arrow1_script.LiftOff();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            arrow1_script.CeaseThrust();
        }

        // Stop the rocket in place
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            arrow1_script.Halt();
        }
        
        // Reads left and right arrow keys
        if (Input.GetKeyDown(KeyCode.LeftArrow) && arrow1_script.GetLambda() < 135)
        {
            arrow1_script.AdjustThrustAngle(1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && arrow1_script.GetLambda() > 45)
        {
            arrow1_script.AdjustThrustAngle(-1);
        }

    }

    /// <summary>
    /// Created OnGui method to display 
    /// </summary>
    private void OnGUI()
    {
        GUI.color = Color.white;
        GUI.skin.box.fontSize = 15;
        GUI.skin.box.wordWrap = false;

        GUI.Box(new Rect(5, 80, 100, 30), "speed = " + arrow1_script.GetSpeed());

        GUI.Box(new Rect(5, 110, 100, 30), "theta = " + arrow1_script.GetTheta());

        GUI.Box(new Rect(5, 140, 100, 30), "lambda = " + arrow1_script.GetLambda());

        GUI.Box(new Rect(5, 170, 100, 30), "time = " + arrow1_script.GetTime());

        GUI.Box(new Rect(5, 200, 100, 30), "Distance = " + arrow1_script.GetDistance());
    }


}
