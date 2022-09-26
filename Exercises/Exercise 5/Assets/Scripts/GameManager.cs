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
       
    }

}
