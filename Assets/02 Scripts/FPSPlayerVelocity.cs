using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerVelocity
{
    /// <summary>
    /// Current horizontal and vertical input from the user.
    /// </summary>
    Vector3 input;
    
    /// <summary>
    /// Current value of the "velocity" field of this class.
    /// </summary>
    public Vector3 velocity;

    /// <summary>
    /// Returns current horizontal and vertical input from the user, in Vector3.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetInputValue()
    {
        return input;
    }

    /// <summary>
    /// Calculates and updates the "velocity" field of this class. Takes a desired player speed and a gravity constant as parameters. 
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="gravity"></param>
    public void GetVelocity(float speed, float gravity)
    {
            input = GetInputXZ();
            velocity.x = input.x * speed;
            velocity.y += gravity * Time.deltaTime;
            velocity.z = input.z * speed;
    }

    /// <summary>
    /// Calculates and updates the "velocity" field of this class. Takes a desired player speed and a gravity constant as parameters. 
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="gravity"></param>
    public void GetVelocityCamTrDirection(float speed, float gravity)
    {
        input = GetInputXZ();
        input = Camera.main.transform.TransformDirection(input);
        velocity.x = input.x * speed;
        velocity.y += gravity * Time.deltaTime;
        velocity.z = input.z * speed;
    }

    /// <summary>
    /// Assigns horizontal and vertical user inputs to x and z elements of Vector3 v, respectively. Returns the normalized Vector3.
    /// </summary>
    /// <returns></returns>
    Vector3 GetInputXZ()
    {
        Vector3 v = Vector3.zero;
        v.x = Input.GetAxisRaw("Horizontal");
        v.z = Input.GetAxisRaw("Vertical");
        return v;
    }
}