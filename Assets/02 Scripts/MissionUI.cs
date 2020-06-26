using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissionUI : MonoBehaviour
{
    RectTransform rectTransform;
    public Text text;
    string txt;
    public float typeSpeed;

    float width;
    float height;
    public float finalWidth;
    public float finalHeight;
    public float widthSpeed;
    public float heightSpeed;

    bool txtTyped;

    private void OnEnable()
    {
        width = 0f;
        height = 50f;
        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(width, height);

        txt = text.text;
        text.text = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!txtTyped)
        {
            width = Mathf.Lerp(width, finalWidth, Time.deltaTime * widthSpeed);
            rectTransform.anchoredPosition = new Vector3(width, 0, 0);
        }
        if (!txtTyped && width / finalWidth > 0.95f)
        {
            height = Mathf.Lerp(height, finalHeight, Time.deltaTime * heightSpeed);
        }
        rectTransform.sizeDelta = new Vector2(width, height);

        if (!txtTyped && width / finalWidth > 0.99f && height / finalHeight > 0.99f && !txtTyped)
        {
            text.text = txt;
            txtTyped = true;
        }

        if (txtTyped)
        {
            Invoke("NewHeight", 5f);
            Destroy(gameObject, 10f);
        }
    }

    void NewHeight()
    {
        height = Mathf.Lerp(height, 0, Time.deltaTime * heightSpeed);
    }

}


/*
 * Objective : 
Escape from the building... Survive your way to the exit!
*/
