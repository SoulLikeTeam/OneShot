using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyCondition : AICondition
{
    [SerializeField]
    private KeyCode keyCode;

    public override bool IfCondition(AIState currentState, AIState nextState)
    {
        bool input;
        if (Input.GetKeyDown(keyCode))
        {
            input = true;
        }
        else
        {
            input = false;
        }
        return input;
    }
}
