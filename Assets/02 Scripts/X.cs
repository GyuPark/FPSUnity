using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class X : MonoBehaviour
{
    Image image;
    float alphaValue;


    public static bool hit;
    float timer;
    public AnimationCurve animCurve;

    // Start is called before the first frame update
    void Start()
    {
        hit = false;
        timer = 0;
        alphaValue = 0;
        image = GetComponent<Image>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            timer = 0;
            hit = false;
        }

        if (alphaValue > 0.01f)
        {
            timer += Time.deltaTime;
        }

        alphaValue = animCurve.Evaluate(timer);
        image.color = new Color(image.color.r, image.color.g, image.color.b, alphaValue);
    }
}
