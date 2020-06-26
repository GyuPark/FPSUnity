using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateSwat : MonoBehaviour
{
    private void OnEnable()
    {
        GameObject player = GameObject.Find("Player");
        transform.position = player.transform.position - new Vector3(0,1f,0);
        transform.rotation = player.transform.rotation;


    }

    private void Update()
    {
        
    }
}
