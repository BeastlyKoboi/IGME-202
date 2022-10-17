using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    public Terrain terrain;
    public GameObject dish_prefab; //this is a reference to the Dish prefab in the Assets > Models folder
    private GameObject[,] dishArray;
    private GameObject dish;
  
    float x, y, z;

    private int dishHeight;
    private const int numDishes = 5; //actually, there are numDishs * numDishes number of dishes

    void Start()
    {
      //Exercise 8 requires that you replace the code below with a call to the completed SetUpDishArray() method

      
      z = 0;
      x = 0;
      y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrain.GetPosition().y + dishHeight;
      dish = Instantiate(dish_prefab, Vector3.zero, Quaternion.identity);
      dish.transform.position = new Vector3(x, y, z);
      dish.transform.localScale = new Vector3(.1f, .1f, .1f);

      //Exercise 8 remove the // below when you have completed the code in the SetUpDishArray method
      SetUpDishArray();
    }


    //Exercise 8 requires that you fill in the missing statements in the SetUpDishArray() method below
    //to instantiate a 5x5 grid of Dish Antenna which lie upon the curved surface and have equally spaced x and z coordinates

    void SetUpDishArray()
    {
        //NOTE:  this method must not be run before the TerrainGeneration's Start() method, since it needs to get the computed SampleHeight() 
        //If necessary, within the Unity editor, use Edit > Project Settings > Script Execution Order
        
        dishHeight = 2;

        dishArray = new GameObject[numDishes, numDishes];

        TerrainGeneration terrainScript = terrain.GetComponent<TerrainGeneration>();

        for (int k = 0; k < numDishes; k++)
        {
            z = (terrainScript.zSize / 4) * k - terrainScript.zSize / 2;
            for (int i = 0; i < numDishes; i++)
            {
                x = (terrainScript.xSize / 4) * i - terrainScript.xSize / 2;
                y = terrain.SampleHeight(new Vector3(x, 0, y)) + terrain.GetPosition().y;
                dish = Instantiate(dish_prefab, Vector3.zero, Quaternion.identity);
                dish.transform.position = new Vector3(x, y, z);
                dish.transform.localScale = new Vector3(.1f, .1f, .1f);
                dishArray[k, i] = dish;
            }
        }
    }
      
    



}