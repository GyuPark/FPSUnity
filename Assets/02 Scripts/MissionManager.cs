using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
    public GameObject escapeUI;
    public GameObject rallyUI;
    public GameObject successUI;

    public enum Missions
    {
        ESCAPE, //시작하면
        RALLYPOINT, //열쇠로 문따고 나가면
        SUCCESS
    }

    public Missions mission;

    void Start()
    {
        mission = Missions.ESCAPE;
    }

    void Update()
    {
        switch (mission)
        {
            case Missions.ESCAPE:
                if (escapeUI != null && !escapeUI.activeSelf)
                {
                    escapeUI.SetActive(true);
                }
                break;
            case Missions.RALLYPOINT:
                if (rallyUI != null && !rallyUI.activeSelf)
                {
                    rallyUI.SetActive(true);
                }
                break;
            case Missions.SUCCESS:
                if (successUI != null && !successUI.activeSelf)
                {
                    successUI.SetActive(true);
                    StartCoroutine("Success");
                }
                break;
            default:
                break;
        }

        print(mission);
    }

    IEnumerator Success()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(3);
    }

}
