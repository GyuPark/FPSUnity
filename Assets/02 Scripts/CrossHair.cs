using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    //크로스헤어UI
    public Image [] crHair;

    float crhair1PosX;
    float crhair2PosX;
    float crhair3PosX;
    float crhair4PosX;

    float j;

    void Start()
    {

    }

    void Update()
    {
        crhair1PosX = crHair[0].GetComponent<RectTransform>().anchoredPosition.x;
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            j = crhair1PosX;
            StartCoroutine(CrossHairMove(true));
        }

        if (Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical"))
        {
            j = crhair1PosX;
            StartCoroutine(CrossHairMove(false));
        }
    }

    IEnumerator CrossHairMove(bool isMove)
    {
        if (isMove == true)
        {

            //움직일 때는 크로스헤어가 커지고 멈출때는 다시 작아져라
            for (float i = j; i <= 40; i++)
            {
                //float x = Mathf.Sin((i - 15) / 25);
                //crhair1PosX = Mathf.Clamp(x, 15, 40);
                crhair1PosX = i;
                crHair[0].rectTransform.anchoredPosition = new Vector3(crhair1PosX, 0, 0);
                crHair[1].rectTransform.anchoredPosition = new Vector3(crhair1PosX * -1, 0, 0);
                crHair[2].rectTransform.anchoredPosition = new Vector3(0, crhair1PosX, 0);
                crHair[3].rectTransform.anchoredPosition = new Vector3(0, -1 * crhair1PosX, 0);

                yield return new WaitForSeconds(0.001f);
            }
        }
        else
        {
            for (float i = j; i >= 15; i--)
            {
                crhair1PosX = i;
                crHair[0].rectTransform.anchoredPosition = new Vector3(crhair1PosX, 0, 0);
                crHair[1].rectTransform.anchoredPosition = new Vector3(crhair1PosX * -1, 0, 0);
                crHair[2].rectTransform.anchoredPosition = new Vector3(0, crhair1PosX, 0);
                crHair[3].rectTransform.anchoredPosition = new Vector3(0, -1 * crhair1PosX, 0);

                yield return new WaitForSeconds(0.001f);
            }

        }
    }
}
