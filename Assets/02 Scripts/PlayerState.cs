using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player의 상태를 관리한다

public class PlayerState : MonoBehaviour
{
    public enum playerState
    {
        IDLE,
        WALK,
        RUN,
        CROUCH,
    }
}
