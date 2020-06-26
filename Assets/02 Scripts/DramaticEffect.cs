using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DramaticEffect : MonoBehaviour
{
    public float newFieldOfView; //
    public float camAddX, camAddY;

    public float slowDownFactor;
    public float slowDownDuration;

    void Start()
    {
        slowDownDuration = 1f;
    }

    void Update()
    {
        if (PlayerDie.isDead)
        {
            return;
        }

        //adds incrementally every frame update
        Time.timeScale += (1f / slowDownDuration) * Time.unscaledDeltaTime;
        //timeScale is not to overshoot below 0f or beyond 1f
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        //print(Time.timeScale);
    }

    void DoSlowMotionTwo() //set to be triggered by animation events
    {
        slowDownFactor = 0.01f;
        slowDownDuration = 1f;
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    void None()
    {

    }
}
