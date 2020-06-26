using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject crosshair;
    public GameObject bulletInfo;
    public GameObject fadeOut;

    public Text currentBullet;
    public Text totalBullet;

    public Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CrossHairOnAndOff();
        UpdateBulletUI();
        BulletInfoOnAndOff();
        
        if (PlayerDie.isDead && !fadeOut.activeSelf)
        {
            fadeOut.SetActive(true);
        }
    }

    private void UpdateBulletUI()
    {
        currentBullet.text = weapon.currentBulletCount.ToString();
        totalBullet.text = weapon.totalBulletCount.ToString();
    }

    void CrossHairOnAndOff()
    {
        if (!PlayerMove2.isRunning && !Weapon.isZooming && !ControlAnim.runAnimActive && !Weapon.isReloading && !Buttstroke.isButtstroking && !PlayerDie.isDead)
        {
            crosshair.gameObject.SetActive(true);
        }
        else
        {
            crosshair.gameObject.SetActive(false);
        }
    }

    void BulletInfoOnAndOff()
    {
        if (Buttstroke.isButtstroking)
        {
            bulletInfo.SetActive(false);
        }
        else
        {
            bulletInfo.SetActive(true);

        }
    }
}
