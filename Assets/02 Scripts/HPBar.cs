using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        image.fillAmount = PlayerHit.hp;
    }
}
