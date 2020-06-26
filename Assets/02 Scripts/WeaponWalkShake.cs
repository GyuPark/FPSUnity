using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWalkShake : MonoBehaviour
{
    Vector3 initialPos;
    Vector3 newPos;
    //float initialZ;
    //float newZ;

    float timer;

    float amount;
    float freq, amp;
    float divider;

    public float shakeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        divider = 10f;
        initialPos = transform.localPosition;
        //initialZ = transform.localEulerAngles.z;
    }

    //달릴 때만 카메라가 흔들리도록 한다
    void Update()
    {
        if (PlayerDie.isDead)
        {
            return;
        }

        float multiplier = Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical"));
        multiplier = Mathf.Clamp(multiplier, 0, 1);

        //걸을 때
        if (PlayerMove2.isWalking)
        {
            freq = 10f;
            amp = 0.5f;
            shakeSpeed = 2.5f;

            if (Weapon.isZooming)
            {
                shakeSpeed = 10f;
                divider = 100f;
                amp = 0.05f;
                //multiplier = 0; //newPos 무력화

                //if (amount == 0)
                //{
                //    timer = 0;
                //}
            }
            else
            {
                shakeSpeed = 2.5f;
                divider = 10f;
            }

        }
        else
        {
            multiplier = 0; //newPos 무력화

            if (amount == 0)
            {
                timer = 0;
            }
        }

        timer += Time.deltaTime;

        //Sine모양 Position 
        amount = (Mathf.Sin(freq * timer) * amp) * multiplier;

        //print(PlayerMove2.isRunning);
        //print(amount);
        //print(multiplier);

        newPos = new Vector3(amount/5, Mathf.Abs(amount) /10, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPos + newPos, Time.deltaTime * shakeSpeed);

        ////Z축 Rotation 
        //newZ = Mathf.Lerp(newZ, initialZ + amount * 5, Time.deltaTime);
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, newZ);
    }
}
