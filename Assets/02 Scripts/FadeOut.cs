using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//목표 : die하면 UImanager에서 enable 된다. fade out이 마무리되어갈 때 즈음 scene을 새롭게 로드한다
public class FadeOut : MonoBehaviour
{
    public float speed;
    float newAlpha;
    Image image;
    public AnimationCurve fadeCurve;

    float t;

    private void OnEnable()
    {
        image = GetComponent<Image>();
        newAlpha = 0f;
        t = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        newAlpha = fadeCurve.Evaluate(t);
        image.color = new Color(0, 0, 0, newAlpha);

        if (newAlpha > 0.95f)
        {
            StartCoroutine("Fade");
        }
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(3.5f);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
