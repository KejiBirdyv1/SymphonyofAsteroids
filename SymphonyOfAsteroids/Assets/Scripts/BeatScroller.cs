using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;
    public bool hasStarted;
    public float angle; // Angle in degrees for the object's direction

    void Start()
    {
        beatTempo = beatTempo / 60f;
    }

    void Update()
    {
        if (!hasStarted)
        {
           /* if (Input.anyKeyDown)
            {
                hasStarted = true;
            } */
        }
        else
        {
            /* Directions 

                Down
                90 = Down | 45 = Diagonal Down Left | -225 = Diagonal Down Right 

                Up
                -90 = Up | 45 = Diagonal Up Right | 225 = Diagonal Up Left

                0 = Left | 180 = Right 
            */

            // Calculate the direction vector based on the angle 
            float angleInRadians = Mathf.Deg2Rad * angle;
            Vector3 direction = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0f);

            // Move the object in the calculated direction
            //transform.position -= direction * beatTempo * Time.deltaTime;
        }
    }
}
