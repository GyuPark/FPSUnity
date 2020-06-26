using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Player가 피격당하면 
//1. 위로 튀었다가 돌아온다
//2. 카메라가 순간적으로 blur 되었다가 돌아온다
//3. 피격 순간 날아온 총알의 방향에 따라 혈흔으로 방향을 암시한다. -못함 ㅠㅠ

public class PlayerHit : MonoBehaviour
{
    public AnimationCurve playerHitXCurve;
    public AnimationCurve playerHitYCurve;
    public AnimationCurve playerHitZCurve;
    public AnimationCurve playerHitACurve;

    float xValue;
    float yValue;
    float zValue;
    float aValue;

    public static bool playerHit;

    public static float hp;
    [HideInInspector] public bool hit;
    float timer;

    public Image hitBlur;

    // Start is called before the first frame update
    void Start()
    {
        playerHit = false;
        hp = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerDie.isDead)
        {
            return;
        }

        Pop();
        Blur();
        hit = false;

        Die();
    }

    private void Pop()
    {
        //피격되었을 때
        if (hit)
        {
            playerHit = true;
            timer = 0f;
            hp -= 0.04f;
        }

        if (playerHit)
        {
            timer += Time.deltaTime;
        }

        if (timer > 1.5f)
        {
            playerHit = false;
        }

        xValue = playerHitXCurve.Evaluate(timer);
        yValue = playerHitYCurve.Evaluate(timer);
        zValue = playerHitZCurve.Evaluate(timer);
        transform.localEulerAngles = new Vector3(xValue, yValue, zValue);
    }

    private void Blur()
    {
        aValue = playerHitACurve.Evaluate(timer);
        hitBlur.color = new Color(hitBlur.color.r, hitBlur.color.g, hitBlur.color.b, aValue);
    }

    void Die()
    {
        if (hp < 0)
        {
            PlayerDie.isDead = true;
        }
    }
}
