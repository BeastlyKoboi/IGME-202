using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoBox : MonoBehaviour
{
    Vector3 pos;
    Vector3 vel;
    Vector3 acc;

    float x, y, z;

    float speed;
    float timeScale;

    float theta;
    float slope;
    Vector3 normal, gradient;
    bool go;
    public Terrain terrain;

    enum Side {  NORTH, WEST, SOUTH, EAST };

    Side leg;

    void Start()
    {
        go = false;

        timeScale = .25f;

        leg = Side.NORTH;

        theta = 15.0f;

        slope = Mathf.Tan(Mathf.Deg2Rad * theta);

        normal = new Vector3(0f, Mathf.Cos(Mathf.Deg2Rad * theta), -Mathf.Sin(Mathf.Deg2Rad * theta));

        gradient = new Vector3(0f, -Mathf.Sin(Mathf.Deg2Rad * theta), -Mathf.Cos(Mathf.Deg2Rad * theta));
         
        x = 100f;
        z = 100f;

        //Exercise 10:  replace the statement below using Terrain's SampleHeight() and GetPosition() 
        //y = slope * z;
        y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrain.GetPosition().y;

        pos = new Vector3(x, y, z);

        transform.position = pos;

        transform.rotation = Quaternion.LookRotation(Vector3.left, normal);
        //RoBox's transform.forward is now < -1,0,0 > and its transform.up is aligned with the surface normal

        transform.Translate(-1, 1, 1, Space.Self); //move RoBox, in its own local coordinate sytem, left and forward, so that its sides are parallel to the edges
        pos = transform.position;
        //Exercise 10:  maybe need to translate upward ?
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.position = pos;
            go = true;
        }

     
        if (go)
        {
            switch (leg)
            {  
                //heading W on the NORTH side
                case Side.NORTH:
                    {
                        transform.rotation = Quaternion.LookRotation(Vector3.left, normal);
                        vel = 198 * transform.forward;
                        pos += timeScale * vel * Time.deltaTime;

                        if (pos.x < -100 + 1)
                        {
                            pos.x = -100 + 1;
                            leg = Side.WEST;  //move in S direction
                        }
                        //Exercise 10:  replace the statement below using Terrain's SampleHeight() and GetPosition()
                        //pos.y = slope * pos.z;
                        pos.y = terrain.SampleHeight(new Vector3(pos.x, 0, pos.z)) + terrain.GetPosition().y;

                        transform.position = pos;
                        //Exercise 10:  maybe need to translate upward ?
                        transform.Translate(0, 1, 0, Space.Self);

                    }
                    break;

                //heading S on WEST side
                case Side.WEST:
                    {
                        transform.rotation = Quaternion.LookRotation(gradient, normal);
                   
                        vel = 198 / Mathf.Cos(Mathf.Deg2Rad * theta) * transform.forward;
                        pos += timeScale * vel * Time.deltaTime;

                        if (pos.z < -100 + Mathf.Cos(Mathf.Deg2Rad * theta)) 
                        {
                            pos.z = -100 + Mathf.Cos(Mathf.Deg2Rad * theta); 
                            leg = Side.SOUTH;  //move in E direction
                        }
                        //Exercise 10:  replace the statement below using Terrain's SampleHeight() and GetPosition()
                        //pos.y = slope * pos.z;
                        pos.y = terrain.SampleHeight(new Vector3(pos.x, 0, pos.z)) + terrain.GetPosition().y;

                        transform.position = pos;
                        //Exercise 10:  maybe need to translate up?
                        transform.Translate(0, 1, 0, Space.Self);
                    }
                    break;

                //heading E along SOUTH side
                case Side.SOUTH:
                    {
                        transform.rotation = Quaternion.LookRotation(Vector3.right, normal);
                        vel = 198 * transform.forward;
                        pos += timeScale * vel * Time.deltaTime;

                        if (pos.x > 100 - 1)
                        {
                            pos.x = 100 - 1;
                            leg = Side.EAST; //move in S direction
                        }
                        //Exercise 10:  replace the statement below using Terrain's SampleHeight() and GetPosition()
                        //pos.y = slope * pos.z;
                        pos.y = terrain.SampleHeight(new Vector3(pos.x, 0, pos.z)) + terrain.GetPosition().y;

                        transform.position = pos;
                        //Exercise 10:  maybe need to translate upward ?
                        transform.Translate(0, 1, 0, Space.Self);
                    }
                    break;

                //heading N along EAST side
                case Side.EAST:
                    {
                        transform.rotation = Quaternion.LookRotation(-gradient, normal);
                   
                        vel = 198 / Mathf.Cos(Mathf.Deg2Rad * theta) * transform.forward; //must speed up to go slightly further within the same time as going E or W
                        pos += timeScale * vel * Time.deltaTime;

                        if (pos.z > 100 - Mathf.Cos(Mathf.Deg2Rad * theta)) //we don't want any part of RoBox over the edge of the sloped Terrain
                        {
                            pos.z = 100 - Mathf.Cos(Mathf.Deg2Rad * theta); //nudge it back to the edge, by a translation in RoBox's coordinates
                            leg = Side.NORTH; //move in W direction;
                        }
                        //Exercise 10:  replace the statement below using Terrain's SampleHeight() and GetPosition()
                        //pos.y = slope * pos.z;
                        pos.y = terrain.SampleHeight(new Vector3(pos.x, 0, pos.z)) + terrain.GetPosition().y;

                        transform.position = pos;
                        //Exercise 10:  maybe need to translate upward ?
                        transform.Translate(0, 1, 0, Space.Self);
                    }
                    break;
            }
        }

      }
    }