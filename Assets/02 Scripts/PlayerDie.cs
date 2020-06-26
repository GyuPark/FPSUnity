using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    /* 체력이 0 이하로 떨어지면,
    카메라가 뒤로 빠지면서
    메쉬 모두 꺼준다. fps 캐릭터가 생성된다. 이캐릭터는 태어나자마자 죽는 애니메이션이 재생된다
    서서히 페이드아웃된다
    reload the scene */

    
    public static bool isDead;
    bool camPosLocked;
    Transform swat;
    GameObject mainCam;

    public float camMoveSpeed;

    Vector3 startPos;


    // Start is called before the first frame update
    void Start()
    {
        camPosLocked = false;
        isDead = false;
        swat = transform.Find("swat");
        mainCam = Camera.main.gameObject;
    }

    private void Update()
    {
        if (isDead && !camPosLocked)
        {
            camPosLocked = true;
            startPos = mainCam.transform.TransformPoint(0, 0, 0); //receives the world position of the World Cam
        }

        if (isDead)
        {
            swat.gameObject.SetActive(true);
            if (mainCam.activeSelf)
            {
                mainCam.SetActive(false);
            }
        }
    }
}
