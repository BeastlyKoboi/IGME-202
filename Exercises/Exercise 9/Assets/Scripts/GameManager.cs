using UnityEngine;
using UnityEngine.UI; // Note this new line is needed for UI
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public Text scoreText;
	public Text gameOverText;
    CollisionDetector collisionDetector;
    public List<GameObject> rocks;
    public List<GameObject> antirocks;
    public GameObject rocket;
    GameObject antirock;
    GameObject rock;

    int hits;
    int antiRocksAwry;
    int rocksMissed;


    bool rockOut;

    public void Start()
    {
        hits = 0;
        antiRocksAwry = 0;
        rocksMissed = 0;
        DisplayStats();
        collisionDetector = gameObject.GetComponent<CollisionDetector>();
        rocks = new List<GameObject>();
        antirocks = new List<GameObject>();
    }


    public void Update()
    {
        //check to see whether any rock has rolled onto the rocket
        foreach (GameObject rck in rocks)
        {
            if (collisionDetector.AABBTest(rocket, rck))
               PlayerDied();
        }

        //check to see whether any antirocks are colliding with any rocks
        rockOut = false;  //important to note the use of this boolean - see below!

        foreach (GameObject antirck in antirocks )
        {
            foreach (GameObject rck in rocks)
            {
                if (collisionDetector.BoundingCircleTest(antirck, rck) )
                {
                    //Debug.Log("antirock hit rock!");
                    rockOut = true;
                    antirock = antirck;
                    rock = rck;
                    break;
                }
            }

            if (rockOut)
                break;
        }

        //it was necessary to use the boolean rockOut to break out of both loops above, since the following removal will alter the two list data structures!
        if (rockOut)
        {
            RemoveRockFromList(rock);
            RemoveAntiRockFromList(antirock);
            Destroy(rock);
            Destroy(antirock);
            hits++;
            DisplayStats();
        }

        //Exercise 9 requires that you remove AntiRocks and Rocks that are out-of-bounds
        //Suggestion:  use the similar approach as the above, but with separate, not nested, loops since you will process awry AntiRocks separately from the missed Rocks
    }

    public void AddRockToList(GameObject rock)
    {
        rocks.Add(rock);
    }

    public bool RemoveRockFromList(GameObject rock)
    {
        return rocks.Remove(rock);
    }

    public void AddAntiRockToList(GameObject antirock)
    {
        antirocks.Add(antirock);
    }

    public bool RemoveAntiRockFromList(GameObject antirock)
    {
        return antirocks.Remove(antirock);
    }


    //Exercise 9 requires that you display the status regarding Awry AntiRocks and Missed Rocks
    public void DisplayStats()
	{
        scoreText.text = "Hits " + hits.ToString() + "\n" + "AntiRocks Awry  " + antiRocksAwry.ToString() + "\n" + "Rocks Missed " + rocksMissed.ToString();
    }

    public void PlayerDied()
	{
		gameOverText.enabled = true;

		// This freezes the game
		Time.timeScale = 0;				
	}
}
