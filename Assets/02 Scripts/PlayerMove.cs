using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목표1 : Camera가 바라보는 방향을 앞으로 움직인다
//목표2 : -9.8의 중력 가속도를 받으며, 스페이스바를 눌러 점프할 수 있다.
//목표3 : 점프 중 방향 수정이 불가하다.

public class PlayerMove : MonoBehaviour
{
    CharacterController cc;
    float moveSpeed;
    public float sitSpeed;
    public float walkSpeed;
    public float runSpeed;

    public float jumpPower;
    Vector3 dir;
    float yVelocity;
    float gravityConstant;

    [HideInInspector]
    public static bool isJumping;
    [HideInInspector]
    public static bool isSitting;
    [HideInInspector]
    public static bool isRunning;
    [HideInInspector]
    public static bool isWalking;
    [HideInInspector]
    public static bool isStill;


    float ccHeight;
    public float heightChangeSpeed;
    

    void Start()
    {
        Init();
        if (Weapon.isReloading)
        {
            Weapon.isReloading = false;
        }
    }

    void Init()
    {
        isSitting = false;
        isJumping = false;
        ccHeight = 1.42f;
        gravityConstant = -9.8f;
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Player가 죽은 상태라면 아래 함수를 실행하지 않는다.
        if (PlayerDie.isDead)
        {
            return;
        }

        //Player가 착지한 상태라면, 
        if (cc.isGrounded)
        {
            //중력값은 0으로 설정한다.
            yVelocity = 0f;
        }
        

        GetVector3InputXZ();
        InputDirToCamDir();
        StayStill();
        Run();
        Jump();
        Sit();
        ApplyGravity();
        Speed();
        Move();
    }

    void Speed()
    {
        if (isRunning)
        {
            moveSpeed = runSpeed;
        }
        else if (isSitting)
        {
            moveSpeed = sitSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }
    }

    //키보드로부터 X-Axis와 Z-Axis 방향값을 받는 함수
    public void GetVector3InputXZ()
    {
        dir = Vector3.zero;
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.z = Input.GetAxisRaw("Vertical");
        dir.Normalize();
    }

    //키보드로부터 받은 월드 공간 방향을 Main Camera 기준 로컬 방향으로 치환 
    void InputDirToCamDir()
    {
        dir = Camera.main.transform.TransformDirection(dir);
    }

    void StayStill()
    {
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            isStill = true;
        }
        else
        {
            isStill = false;
        }
    }

    void Run()
    {
        //shift를 누르는 동안은 달리는 상태이다
        //누르지 않으면 달리지 않는 상태이다

        if (Input.GetKey(KeyCode.LeftShift) && !isSitting && !Weapon.isShooting && !Weapon.isZooming &&!isStill && !PlayerHit.playerHit && !Buttstroke.isButtstroking)
        {
            isRunning = true;
            isWalking = false;
        }
        else
        {
            //뛰지 않고 걸을 때
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") !=0)
            {
                isWalking = true;
            }
            //가만히 있을 때
            else
            {
                isWalking = false;
            }
            isRunning = false;
        }
    }

    
    void Jump()
    {
        if (cc.isGrounded)
        {
            isJumping = false;
        }
        if (cc.isGrounded && Input.GetKeyDown(KeyCode.Space) && !isSitting)
        {
            isJumping = true;
            yVelocity = 0;
            yVelocity += jumpPower;
        }
    }

    void Sit()
    {
        //c를 누르면 앉고 일어선다
        if (Input.GetKeyDown(KeyCode.C))
        {
            isSitting = !isSitting;

            if (isSitting)
            {
                ccHeight = 0f;
            }
            else
            {
                ccHeight = 2f;
            }
        }

        cc.height = Mathf.Lerp(cc.height, ccHeight, Time.deltaTime * heightChangeSpeed);
        cc.height = Mathf.Clamp(cc.height, 0.01f, 1.42f);
    }

    void ApplyGravity()
    {
        //공중에 있을 때는 중력을 적용받는다. V = V0 + at
        yVelocity += gravityConstant * Time.deltaTime;
        dir.y = yVelocity;
    }

    void Move()
    {
        //vt = 속도x시간 = 방향 X 속력 X 시간 
        cc.Move(dir * moveSpeed * Time.deltaTime);
    }
}
