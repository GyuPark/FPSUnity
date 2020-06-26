using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목표 : weapon sway

public class WeaponSway : MonoBehaviour
{
    public float amount;
    public float maxAmount;
    public float smoothAmount; //access : weapon

    Vector3 initialPosition;

    float upToDown;
    public float amplitude;
    public float frequency;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerDie.isDead)
        {
            return;
        }

        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;

        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY , -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition + finalPosition, Time.deltaTime * smoothAmount);

    }

    public void SmoothAmount(float smoothAmount)
    {
        this.smoothAmount = smoothAmount;
    }
}
