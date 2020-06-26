using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ButtStroke.cs에서 isButtStroking이 true일 때 active 된다.
//active 일 동안 object의 transform 과 rotation이 DramaticEffect.cs의 애니메이션 값에 따라 움직인다.

public class CameraAnim : MonoBehaviour
{
    DramaticEffect de;
    Camera cam;
    Quaternion startRot;

    public float rotSpeed;

    private void OnEnable()
    {
        cam = GetComponent<Camera>();
        de = GetComponentInChildren<DramaticEffect>();
        startRot = transform.localRotation;
    }

    void Update()
    {
        if (PlayerDie.isDead)
        {
            return;
        }

        NewFieldOfView();
        NewRotation();
    }

    void NewFieldOfView()
    {
        cam.fieldOfView = de.newFieldOfView;
    }

    void NewRotation()
    {
        Quaternion rot = Quaternion.identity;
        rot = startRot * Quaternion.Euler(de.camAddX, 0f, 0f);
        transform.localRotation = rot;
    }
}
