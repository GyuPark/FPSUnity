using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//목표1 : FPSPlayerVelocity 클래스의 GetVelocity를 사용하여 velocity를 계산한다.
//필요속성1 : player속도, 중력상수

//목표1.5 : Camera가 바라보는 방향이 Player의 Forward가 된다.

//목표2 : velocity를 사용해 jump하고
// 2.1 : velocity.y = jumpPower 로 점프한다
// 2.2 : isGrounded 가 참일 때와 space키를 눌렀을 때
// 2.3 : jump 하고 착지하기 전까지 방향 전환이 불가능하다.
// 2.4 : jump 하고 착지하기 전까지 jump 하는 순간의 x,z 축 방향을 유지한다.
// 필요속성2 : jumpPower, jump확인bool변수, jump하는 순간의 velocity를 저장할 Vector3

//목표3 : velocity를 사용해 Move한다
// 3.1 : Move.cc(velocity*Time);


public class PlayerMove : MonoBehaviour
{
    public float playerSpeed;
    public float gravityConstant;
    public float jumpPower;

    //jump 확인, jump 순간의 속도
    bool hasJumped;
    Vector3 hasJumpedVelocity;

    CharacterController cc;
    FPSPlayerVelocity fpsVelocity;

    void Start()
    {
        hasJumpedVelocity = Vector3.zero;
        cc = GetComponent<CharacterController>();
        fpsVelocity = new FPSPlayerVelocity();
    }

    void Update()
    {
        
        //목표1 : FPSPlayerVelocity 클래스를 사용해 velocity(Vector3)를 계산하고 방향을 Main Camera로 한다.
        CalculateVelocity();
        //목표2 : velocity를 사용해 jump하고
        Jump();
        //목표3 : velocity를 사용해 Move한다
        Move();
    }

    //실시간 v 계산
    void CalculateVelocity()
    {
        fpsVelocity.GetVelocityCamTrDirection(playerSpeed, gravityConstant);
    }

    //목표2 : velocity를 사용해 jump하고
    void Jump()
    {
        //바닥에 있을때 
        // -> 점프 중이 아닌 상태로 만든다
        //이걸 밑에 두면 한 프레임을 주지 못해서 아직 땅에 있는 것으로 된다.
        if (cc.isGrounded)
        {
            hasJumped = false;
        }

        // 2.2 : 지면에 있을 때 스페이스키를 눌러 점프할 수 있다.
        if (cc.isGrounded && Input.GetButtonDown("Jump"))
        {
            // 2.1 : velocity.y = jumpPower 로 점프한다
            fpsVelocity.velocity.y = jumpPower;

            //착지하기 전까지 hasJumped는 true이다.
            hasJumped = true;

            //jump 하는 순간의 x, z 축 방향을 저장
            hasJumpedVelocity.x = fpsVelocity.velocity.x;
            hasJumpedVelocity.z = fpsVelocity.velocity.z;
        }

        // 점프중일때 방향 전환이 불가능하게 한다.
        // 2.3 : jump 하고 착지하기 전까지 방향 전환이 불가능하다.
        if (hasJumped)
        {
            // 2.4 : jump 하고 착지하기 전까지 jump 하는 순간의 x,z 축 방향을 유지한다.
            fpsVelocity.velocity.x = hasJumpedVelocity.x;
            fpsVelocity.velocity.z = hasJumpedVelocity.z;
        }



        //목표 : 지면에 닿을 때만 점프할 수 있고, 점프한 순간부터 착지하기 전까지는 점프 순간의 velocity를 유지한다.

    }

    void Move()
    {
        //cc.Move(displacement = dir * speed)
        cc.Move(fpsVelocity.velocity * Time.deltaTime);
    }
}
