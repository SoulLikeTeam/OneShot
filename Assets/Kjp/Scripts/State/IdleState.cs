using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AIState
{
    public override void TakeAAction()
    {
        Debug.Log("Idle");
    }
}
