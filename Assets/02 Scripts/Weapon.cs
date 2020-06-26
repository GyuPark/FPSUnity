using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목표 1 : 가늠자 조준

public class Weapon : MonoBehaviour
{
    public WeaponSway ws;
    public WeaponHolderMove wm;

    public Transform shootPoint;
    public float offset;
    public float fireRange;

    public ParticleSystem particleLauncher;
    public GameObject muzzleEffect;

    public GameObject stoneEffect;
    public GameObject bloodEffect;

    //ragdoll 이 받아갈 총알 충격위치
    [HideInInspector] public Vector3 bulletImpactPos;


    [HideInInspector]
    public static bool isReloading;
    [HideInInspector]
    public static bool isShooting;
    [HideInInspector]
    public static bool isZooming;

    [HideInInspector]
    public static bool enemyHit;

    private Vector3 originalPos;
    private Quaternion originalRot;
    public Vector3 aimPos;
    public float aodSpeed;


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        shootPoint.transform.position = Camera.main.transform.position + new Vector3(0, offset, 0);
        fieldOfView = 60.0f;
        Camera.main.fieldOfView = fieldOfView;
        originalPos = transform.localPosition;

        totalBulletCount = 200;
        currentBulletCount = bulletPerMag;
        hasAmmo = true;

        bulletImpactPos = Vector3.zero;
    }

    void Update()
    {
        if (PlayerDie.isDead)
        {
            return;
        }

        //print("isReloading :" + isReloading);
        //print("isZooming :" + isZooming);
        //print("isShooting:" + isShooting);

        BulletStateUpdate();
        AimDownSights();
        shootPoint.transform.position = Camera.main.transform.position + new Vector3(0, offset, 0);
    }

    private void BulletStateUpdate()
    {
        if (currentBulletCount == bulletPerMag)
        {
            hasFullAmmo = true;
        }
        else
        {
            hasFullAmmo = false;
        }

        if (currentBulletCount == 0)
        {
            hasAmmo = false;
        }
        else
        {
            hasAmmo = true;
        }
    }

    private float fieldOfView;
    public float zoomSpeed;

    private void AimDownSights()
    {
        //장전 중이지 않을 때만 조준 및 사격 가능
        if (!isReloading)
        {
            //조준하고 쏠 때
            if (Input.GetButton("Fire2") && !Buttstroke.isButtstroking) //개머리판으로 칠 때는 zoom되지 않도록
            {
                isZooming = true;
                transform.localPosition = Vector3.Lerp(transform.localPosition, aimPos, Time.deltaTime * aodSpeed);
                fieldOfView = Mathf.Lerp(fieldOfView, 50.0f, Time.deltaTime * zoomSpeed);
                ws.amount = 0.02f;
                ws.SmoothAmount(8);

                if (Input.GetButton("Fire1") && !ControlAnim.runAnimActive && hasAmmo)
                {
                    isShooting = true;
                    transform.localPosition = Vector3.Lerp(transform.localPosition, aimPos - new Vector3(0, 0, 0.07f), Time.deltaTime * aodSpeed);
                    wm.Stable(55f, 0.002f);
                    FireBullet();
                }
                else
                {
                    isShooting = false;
                    wm.Stable(2f, 0.001f);
                }
            }

            //조준 안하고쏠때
            else
            {
                isZooming = false;
                transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos, Time.deltaTime * (aodSpeed - 9));
                fieldOfView = Mathf.Lerp(fieldOfView, 60.0f, Time.deltaTime * zoomSpeed);
                ws.SmoothAmount(6);
                ws.amount = 0.04f;

                if ((Input.GetButton("Fire1")) && hasAmmo)
                {
                    isShooting = true;
                    //총꺼내기도 전에 떨림 방지
                    if (!ControlAnim.runAnimActive)
                    {
                        transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos - new Vector3(0, 0, 0.07f), Time.deltaTime * (aodSpeed - 9));
                        wm.Stable(70f, 0.0045f);
                        FireBullet();
                        particleLauncher.Emit(1);
                    }
                }
                else
                {
                    wm.Stable(3f, 0.005f);
                    isShooting = false;
                }
            }
            Camera.main.fieldOfView = fieldOfView;
        }
        else //장전 중일 때는 사격 및 조준 상태가 아니다.
        {
            isShooting = false;
            isZooming = false;
            wm.Stable(2f, 0.001f);
        }
    }


    float fireTimer; //발사타이머
    public float fireRate; //발사주기

    public static bool hasAmmo; //총알이 남았는가?
    public static bool hasFullAmmo; //총알이 가득 찼는가?
    [HideInInspector]
    public int totalBulletCount;
    [HideInInspector]
    public int currentBulletCount;
    public int bulletPerMag;


    void FireBullet()
    {
        //사용자가 바라보는 방향으로 총을 쏘자
        RaycastHit hit;

        //시간은 흐른다
        fireTimer += Time.deltaTime;



        //연사
        if (fireTimer > fireRate)
        {
            fireTimer = 0f;
            currentBulletCount--;

            if (currentBulletCount == 0)
            {
                hasAmmo = false;
            }
            else
            {
                hasAmmo = true;
            }
            //총구에서 레이가 나가는 거라 정조준하지 않을 때는 정확하지 않을 걸..?
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, fireRange))
            {
                //Debug.Log(hit.transform.name + "Found");
                
                //파티클
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    if (bloodEffect != null)
                    {
                        GameObject blood = Instantiate(bloodEffect);
                        blood.transform.position = hit.point;
                        blood.transform.forward = hit.normal;
                        Destroy(blood, 1.0f);
                    }
                    bulletImpactPos = hit.point;
                }
                else
                {
                    if (stoneEffect != null)
                    {
                        GameObject stone = Instantiate(stoneEffect);
                        stone.transform.position = hit.point;
                        stone.transform.forward = hit.normal;
                        Destroy(stone, 1.0f);
                    }
                    
                }



                //솔씨코드
                ////총에 맞은 대상이 Enemy스크립트를 가지고 있느냐?
                SOL_Ememy1 enemy = hit.transform.GetComponent<SOL_Ememy1>();
                if (enemy != null)//스크립트를 가지고 있으면 혹은 에너미가 살아있으면
                {
                    if (isZooming)
                    {
                        X.hit = true;
                    }
                    enemy.Damage(15, 30);
                    
                }
            }
        }
    }


}
