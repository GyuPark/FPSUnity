using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnim : MonoBehaviour
{
    public Weapon weapon;
    Animator anim;
    public static bool runAnimActive;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerDie.isDead)
        {
            return;
        }

        ButtStroke();
        Run();
        Reload();
    }

    void ButtStroke()
    {
        //butstroking 중이고 에너미를 향해 몸을 돌렸다면, 애니메이션을 실행할 차례
        if (Buttstroke.isButtstroking)
        {
            anim.SetBool("Running", false);
            anim.SetBool("Buttstroking", true);
        }
        else
        {
            anim.SetBool("Buttstroking", false);
        }
    }

    private void Run()
    {
        //isrunning == true와 Horizontal >0 일 때
        if (PlayerMove2.isRunning && Input.GetAxis("Vertical") > 0)
        {
            //print("itbetterwork");
            //animation이 발동하고
            anim.SetBool("Running", true);
        }
        //뛰지 않는 중이거나 
        else if (!PlayerMove2.isRunning || Buttstroke.isButtstroking)
        {
            anim.SetBool("Running", false);
            //animation이 꺼지고??
            //position, rotation 000으로 돌아온다 ?? aim down sights false로 오지 않음?
        }
    }

    private void Reload()
    {
        //장전중이라면
        if (Weapon.isReloading)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            int reloadState = Animator.StringToHash("Base Layer.Reload");

            if (stateInfo.fullPathHash == reloadState)
            {
                //장전 애니메이션 시간 완전 경과시
                if (stateInfo.normalizedTime > 0.99f)
                {
                    Weapon.isReloading = false;


                    //장전시 total이 25이상일 때
                    if (weapon.totalBulletCount >= weapon.bulletPerMag)
                    {
                        weapon.totalBulletCount -= (weapon.bulletPerMag - weapon.currentBulletCount);
                        weapon.currentBulletCount += (weapon.bulletPerMag - weapon.currentBulletCount);
                    }
                    else
                    {
                        weapon.currentBulletCount += weapon.totalBulletCount;
                        weapon.totalBulletCount -= weapon.totalBulletCount;
                    }
                }
            }
        }
        //총알이 가득차지 않은 상태이고, 장전하지 않고 있을 때 R을 누르면
        else if (Input.GetKeyDown(KeyCode.R) && !Weapon.isReloading && !Weapon.hasFullAmmo && weapon.totalBulletCount > 0)
        {
            ControlAnim.runAnimActive = false;
            Weapon.isReloading = true;
            anim.SetTrigger("Reload");
        }
    }

    void RunAnimActiveToTrue()
    {
        runAnimActive = true;
    }
    void RunAnimActiveToFalse()
    {
        runAnimActive = false;
    }

    void EndButtstroke()
    {
        Buttstroke.isButtstroking = false;
        Buttstroke.hasTurnedTowardsTheEnemy = false;
        runAnimActive = false;
    }
}
