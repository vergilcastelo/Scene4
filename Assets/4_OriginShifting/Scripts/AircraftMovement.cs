using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftMovement : MonoBehaviour
{

    //set initial speed and range
    // Initial speed in km/hr
     public float speed = 50f;
     // Maximum speed
    private const float MaxSpeed = 500f;
     // Minimum speed
    private const float MinSpeed = 50f;
    // add Yaw speed of rotation in degrees
    public float yawSpeed = 30f;
    


    // Update is called once per frame
    void Update()
    {
        // Throttle control
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // Increase speed
            speed += 10f * Time.deltaTime; 
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
             // Decrease speed
            speed -= 10f * Time.deltaTime;
        }
        //call method to update speed
        SetSpeed(speed);
        // Convert speed from km/hr to meters per second for Unity units
        float speedMetersPerSecond = speed * 1000f / 3600f;
        
        // Move the plane forward
        transform.Translate(Vector3.forward * speedMetersPerSecond * Time.deltaTime);

         // Yaw control
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, yawSpeed * Time.deltaTime); // Rotate to right
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -yawSpeed * Time.deltaTime); // Rotate to left
        }
    }

    // Public method to set speed, can be called from other scripts or UI
    public void SetSpeed(float newSpeed)
    {
        //clamp for range of speed
        speed = Mathf.Clamp(newSpeed, MinSpeed, MaxSpeed);
    }
}
