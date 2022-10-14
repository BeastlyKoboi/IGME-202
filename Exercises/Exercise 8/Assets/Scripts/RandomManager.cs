using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManager : MonoBehaviour
{
    public Terrain terrain;
    GameObject[] helices;
    [SerializeField] GameObject helixPrefab; //ref to prefab in Assets > Models, set in Inspector
    private Helix helix;  //reference to the script component of the instantiated helixPrefab 
    private const int numHelices = 100;
   
    void Start()
    {

        float x, y, z;

        helices = new GameObject[numHelices];
        for (int i = 0; i < helices.Length; i++)
        {
            x = Random.Range(-90f, 90f);
            z = Random.Range(-90f, 90f);
            y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrain.GetPosition().y;
            helices[i] = Instantiate(helixPrefab, new Vector3(x, y, z), Quaternion.identity);
            helix = helices[i].GetComponent<Helix>();
            helix.radius = Random.Range(1, 10);
            helix.color = Random.ColorHSV();
            helix.width = Gaussian(.11f, .05f); //Random.Range(.01f, .1f);
            helix.height = Random.Range(1f, 150f);
            helix.numWinds = Random.Range(1, 5);
            helix.GenHelix();
        }
    }

  float Gaussian(float mean, float stdDev)
  {
    float val1 = Random.Range(0f, 1f);
    float val2 = Random.Range(0f, 1f);
    float gaussValue = Mathf.Sqrt(-2.0f * Mathf.Log(val1)) * Mathf.Sin(2.0f * Mathf.PI*val2);
       
       
    return mean + stdDev * gaussValue;
  }

 
}
