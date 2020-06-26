using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//걷거나, 뛰거나, 사격할 때 카메라를 transform.position.x&y와 eulerangle.z로흔든다.


public class CameraShake : MonoBehaviour
{
    Vector3 initialPos;
    Vector3 newPos;
    float initialZ;
    float newZ;

    float timer; //sine함수 x값
    float amount; //sine함수 y값
    float freq, amp; //sine 주기, 진폭

    public float shakeSpeed; //Lerp 속도

    void Start()
    {
        initialPos = transform.localPosition;
        initialZ = transform.localEulerAngles.z;
    }

    void Update()
    {
        if (PlayerDie.isDead)
        {
            return;
        }

        //multiplier : horizontal, vertical input의 절대합
        float multiplier = Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical"));
        multiplier = Mathf.Clamp(multiplier, 0, 1);

        //달리지 않을 때
        if (!PlayerMove2.isRunning)
        {
            //걸을 때
            if (PlayerMove2.isWalking)
            {
                freq = 10f;
                amp = 0.5f;
                shakeSpeed = 2.5f;
            }
            else
            {
                multiplier = 0; //newPos 무력화

                if (amount == 0)
                {
                    timer = 0;
                }
            }
        }
        //달릴 때
        else
        {
            freq = 10f;
            amp = 1.5f;
            shakeSpeed = 2.5f;
        }

        timer += Time.deltaTime;

        //Sine모양 Position 
        amount = (Mathf.Sin(freq * timer) * amp) * multiplier;

        newPos = new Vector3(amount, Mathf.Abs(amount) / 3, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPos + newPos, Time.deltaTime * shakeSpeed);

        //Z축 Rotation 
        newZ = Mathf.Lerp(newZ, initialZ + amount * 5, Time.deltaTime);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, newZ);
    }
}
