using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Collisions.
/// Component of the Game Manager game object.
/// Determines collisions between 2 game objects.
/// </summary>
public class CollisionDetector: MonoBehaviour 
{
    public bool AABBTest(GameObject a, GameObject b)
	{

        // get horizontal extents of sprite A
        float aMinX = a.transform.position.x + a.GetComponent<SpriteInfo>().lowLeft.x;
        float aMaxX = a.transform.position.x + a.GetComponent<SpriteInfo>().upRight.x;
       
        // get vertical extents of sprite A
        float aMinY = a.transform.position.y + a.GetComponent<SpriteInfo>().lowLeft.y;
        float aMaxY = a.transform.position.y + a.GetComponent<SpriteInfo>().upRight.y;

        // get horizontal extents of sprite B
        float bMinX = b.transform.position.x + b.GetComponent<SpriteInfo>().lowLeft.x;
        float bMaxX = b.transform.position.x + b.GetComponent<SpriteInfo>().upRight.x;

        // get vertical extents of sprite B
        float bMinY = b.transform.position.y + b.GetComponent<SpriteInfo>().lowLeft.y;
        float bMaxY = b.transform.position.y + b.GetComponent<SpriteInfo>().upRight.y;


        /*
        // Check for a collision using the concept of a separating plane, which if it exists between sprite A and sprite B, means they are not colliding

        if (aMaxX < bMinX) //sprite A is completely to the left of sprite B, so a vertical separating plane exists
         return false;
        else if (bMaxX < aMinX) //sprite B is completely to the left of sprite A, so a vertical separating plane exists
         return false;
        else if (aMaxY < bMinY) //sprite A is completely below sprite B, so a horizontal separating plane exists
         return false;
        else if (bMaxY < aMinY) //sprite B is completely below sprite A, so a horizontal separating plane exists
         return false;
        else 
         return true; // the only remaining alternative is that sprite A and B are colliding

        */

        //this is equivalent to the above, but it's written using ||'s, which do in fact provide a quick "opt out"; as soon as one expression is found to be true, no others are evaluated
        if ((aMaxX < bMinX) || (bMaxX < aMinX) || (aMaxY < bMinY) || (bMaxY < aMinY))
            return false;
        else
            return true;
        //

        //Exercise 9 requires that you rewrite the above, using deMorgan's Laws and the four conditions aMaxX > bMinX, bMaxX > aMinX, aMaxY > bMinY, and bMaxY > aMinY

    }

    public bool BoundingCircleTest(GameObject a, GameObject b)
    {
        float distance;

        distance = (b.transform.position - a.transform.position).magnitude;

        if (distance > (a.GetComponent<SpriteInfo>().radius + b.GetComponent<SpriteInfo>().radius))
            return false;
        else
            return true;
    }

    //Exercise 9 requires that you make this routine above more efficient, by using sqrMagnitude instead of magnitude. 

    //The square root calculation is done implicitly by Unity during the calculation of the magnitude value, 

    //The modified test becomes whether:

    //     sqrMagnitude = square of (the distance between two circle centers) is greater than the square of (the sum of the radii of the two circles)

}