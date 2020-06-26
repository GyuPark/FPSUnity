using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    float value;
    public float freq;
    public float amplitude;

    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        value = Mathf.Sin(freq*Time.time)*amplitude;
        transform.position = startPos + new Vector3(0, value, 0);
    }
}
