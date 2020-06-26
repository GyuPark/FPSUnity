using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// !isReloading && !isSitting
// V-key를 누르고 && RaycastHit이 Enemy를 검출하고 && RaycastHit.distance <= strokeRange;
// 단계
// 0. Disable PlayerMove2 and RotateObj ,, EnemyTransform 쪽으로 몸을 튼다
// 1. 카메라와 총 동시에 들고 → 타임느려지고 → FieldOfView Out

// 2. 시간 점점 빠르게 흐르기 + field of view in + 내려찍는 애니메이션 + 고개 숙이기
// 3. 0.5초 후 Splatter blood on camera lens

// duration 2.5초 남짓
// 4. 모든 것이 원래상태로 돌아오고 나서 PlayerMove2 RotateObj 다시 active

public class Buttstroke : MonoBehaviour
{
    public GameObject player;
    GameObject enemy; //ray로 검출되는 enemy

    public float rotSpeed; //enemy를 향해 몸을 트는 속도
    RaycastHit hit; //enemy 검출할 rayHit


    PlayerMove2 pm;
    RotateObj ro;
    Weapon wp;
    WeaponSway ws;
    WeaponHolderMove wm;
    WeaponWalkShake wws;
    CameraShake cs;
    Animator anim;

    CameraAnim ca;

    public float strokeRange;
    public static bool isButtstroking;
    public static bool hasTurnedTowardsTheEnemy;

    //Raycast ignores Player Layer..Check in the inspector
    public LayerMask layerMask;


    // Start is called before the first frame update
    void Start()
    {
        GetTheComponents();
        Init();
    }

    void Init()
    {
        isButtstroking = false;
        hasTurnedTowardsTheEnemy = false;
    }

    void GetTheComponents()
    {
        anim = GetComponent<Animator>();
        pm = GetComponentInParent<PlayerMove2>();
        ro = GetComponentInParent<RotateObj>();
        wm = GetComponentInParent<WeaponHolderMove>();
        ws = GetComponentInParent<WeaponSway>();
        wp = GetComponentInParent<Weapon>();
        wws = GetComponentInParent<WeaponWalkShake>();
        cs = GetComponentInParent<CameraShake>();
        ca = GetComponentInParent<CameraAnim>();
    }




    // Update is called once per frame
    void Update()
    {
        if (PlayerDie.isDead)
        {
            return;
        }

        //print(Weapon.isReloading);
        EnableOrDisableComponents();
        MeshOnAndOff();
        TurnTowardEnemy();



        //장전하는 중이 아니라면 && V 키를 눌렀을 때
        if (!Weapon.isReloading && !PlayerMove2.isSitting && Input.GetKeyDown(KeyCode.V) && !isButtstroking)
        {

            //Enemy가 근접공격사거리 안에 들어와 있다면, 사용하는 layerMask에는 enemy만 체크
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, strokeRange, layerMask))
            {
                print(hit.transform.name);
                enemy = hit.transform.gameObject;
                //Buttstroking! 근접공격 개시!
                isButtstroking = true;
                anim.SetTrigger("Buttstroke");
                ControlAnim.runAnimActive = false;

            }
        }
    }

    void EnableOrDisableComponents()
    {
        if (isButtstroking)
        {
            pm.enabled = false;
            ro.enabled = false;
            wm.enabled = false;
            ws.enabled = false;
            wp.enabled = false;
            wws.enabled = false;
            cs.enabled = false;

            ca.enabled = true;
        }
        else
        {
            pm.enabled = true;
            ro.enabled = true;
            wm.enabled = true;
            ws.enabled = true;
            wp.enabled = true;
            wws.enabled = true;
            cs.enabled = true;

            ca.enabled = false;
        }
    }
    void MeshOnAndOff()
    {
        //print("isbutstroking: "+ isButtstroking);
        //print("runanimactive " + ControlAnim.runAnimActive);
        //print("isrunning " + PlayerMove2.isRunning);

        //근접 공격시 기존의 매쉬를 꺼준다

        GameObject gun = transform.Find("male01Mesh").gameObject;
        GameObject arms = transform.Find("Game_engine").gameObject;
        GameObject buttstrokeMesh = transform.Find("ButtStrokeGunAndArm").gameObject;

        if (isButtstroking)
        {
            gun.SetActive(false);
            arms.SetActive(false);

            if (ControlAnim.runAnimActive == false)
            {
                buttstrokeMesh.SetActive(true);

            }
        }
        else
        {
            gun.SetActive(true);
            arms.SetActive(true);
            buttstrokeMesh.SetActive(false);
        }
    }

    void TurnTowardEnemy()
    {
        Vector3 dir = Vector3.zero;

        if (enemy != null)
        {
            dir = (enemy.transform.position + new Vector3(0,0.5f,0)) - player.transform.position; //enemy를 향한 방향
            dir.Normalize();
        }

        //근접공격시작하고 에너미쪽으로 완전히 몸을 틀지 않았을 때
        if (isButtstroking && !hasTurnedTowardsTheEnemy)
        {
            player.transform.forward = Vector3.Lerp(player.transform.forward, dir, Time.deltaTime * rotSpeed);
        }

        float angle = Vector3.Angle(dir, player.transform.forward);


        //player의 몸이 enemy를 향해 거의 다 돌았을 때
        //player.transform.forward와 dir 사이의 각이 1도보다 작을 때
        if (isButtstroking && !hasTurnedTowardsTheEnemy && angle < 1.0f)
        {
            hasTurnedTowardsTheEnemy = true; //언제 다시 false? isbutstroking false 될 때
        }

    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward*3);
    //}

    public void ButtstrokeEnemy()
    {
        hit.transform.gameObject.GetComponent<SOL_Ememy1>().Damage(100, 1000);
    }
}
