using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ragdoll에 rigidbody 충격 ㄱㄱ
public class RagdollBulletImpact : MonoBehaviour
{
    public float bulletImpactForce;
    Rigidbody rb;

    Vector3 impactPos;


    private void OnEnable()
    {
        //rigidbody 가져오기
        rb = GetComponent<Rigidbody>();

        if (rb.IsSleeping())
        {
            rb.WakeUp();
        }

        //bulletimpactPos는 player에게서 가져오는 것
        impactPos = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Weapon>().bulletImpactPos;


        

    }

    private void Update()
    {
        //if (impactPos != null)
        //{
        //    print(impactPos);
        //    rb.AddExplosionForce(bulletImpactForce, impactPos, 1.0f);
        //}
        //else
        //{
        //    print("ImpactPos Null");
        //}
    }
}
