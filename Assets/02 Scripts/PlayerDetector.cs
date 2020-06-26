using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetector : MonoBehaviour
{
    MissionManager missionManager;

    // Start is called before the first frame update
    void Start()
    {
        missionManager = GameObject.Find("MissionManager").GetComponent<MissionManager>();
        missionManager.mission = MissionManager.Missions.ESCAPE;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "RALLYPOINT")
        {
            missionManager.mission = MissionManager.Missions.RALLYPOINT;
        }
        else if (gameObject.name == "SUCCESS")
        {
            missionManager.mission = MissionManager.Missions.SUCCESS;
        }
    }
}
