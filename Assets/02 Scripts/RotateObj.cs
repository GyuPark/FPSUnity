using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목표 : 마우스 XY input에 따라 물체를 회전한다.
//1. 회전한다 transform.eulerAngle = new Vector3(x,y,z);
//2. x = Input.GetMouseAxisX * speed;
//3. y = Input.GetMouseAxisY * speed;
//4. x축 rotation은 제한을 둔다.
//필요속성 : newX, newY, mouseSensitivity;

public class RotateObj : MonoBehaviour
{
    float _newX, _newY;
    public float mouseSensitivity;

    float _newZ; //z축 회전값
    public float zFreq; //z축 기울기 변화 주기
    public float zAmp; //z축 기울기 변화 크기
    float zStartPos;

    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        _newZ = zStartPos;
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerDie.isDead)
        {
            return;
        }


        // 회전한다.
        Rotate(Vector2MouseXY().x, Vector2MouseXY().y, mouseSensitivity, ref _newX, ref _newY);

        //x축 회전을 제한한다.
        ClampCameraY(ref _newY, 60.0f);


        //_newZ = zStartPos + Mathf.Sin(zFreq * Time.time) * zAmp;


        //1. 회전값 대입
        player.transform.eulerAngles = new Vector3(0, _newX, 0);

        //transform.eulerAngles = new Vector3(-_newY, _newX, transform.eulerAngles.z);
        //로컬로 돌려버리면...
        transform.localEulerAngles = new Vector3(-_newY, transform.localEulerAngles.y, transform.localEulerAngles.z);
        
    }

    Vector2 Vector2MouseXY()
    {
        Vector2 v = Vector2.zero;
        v.x = Input.GetAxis("Mouse X");
        v.y = Input.GetAxis("Mouse Y");
        return v;
    }

    void ClampCameraY(ref float y, float clampAngle)
    {
        y = Mathf.Clamp(y, -clampAngle, clampAngle);
    }

    void Rotate(float mouseX, float mouseY, float sensitivity, ref float newX, ref float newY)
    {
        newX += mouseX * sensitivity * Time.deltaTime;
        newY += mouseY * sensitivity * Time.deltaTime;
    }
}
