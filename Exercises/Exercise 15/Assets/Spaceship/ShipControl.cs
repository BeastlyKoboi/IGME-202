using UnityEngine;

public class ShipControl : MonoBehaviour
{
	public GameManager gameManager;
	public GameObject antiRock;

   
    public void purSeek(float theta)
    {
        Vector3 spawnPos = transform.position;
        spawnPos += new Vector3(0, 0, 0);
        gameManager.AddAntiRockToList(Instantiate(antiRock, spawnPos, Quaternion.Euler(0,0,theta)));
    }

    //Exercise 15 requires that you add the code to Update() to have the "vertical axis" keys (Up/Down arrow, W/S key) translate ship in +/- y direction
    //See Exercise 9 Rocket + AntiRocks - Rocsk project for code that handles the "horizontal axis" keys translation in +/- x direction
}