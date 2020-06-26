using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Idle 상태 움직임, 사격시 움직임
public class WeaponHolderMove : MonoBehaviour
{
    Vector3 startPos;
    float my;
    float mx;

    public float freq;
    public float amplitude;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerDie.isDead)
        {
            return;
        }

        //mz = Mathf.Sin(zFreq * Time.time) * zAmp;
        my = Mathf.Sin(freq * Time.time) * amplitude;
        mx = Mathf.Sin(freq * Time.time) * amplitude/2;

        // 1. 아래위로 움직인다
        transform.localPosition = startPos + new Vector3(mx, my, 0);
    }

    public void Stable(float freq, float amplitude)
    {
        this.freq = freq;
        this.amplitude = amplitude;
    }

}
