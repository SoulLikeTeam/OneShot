using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyCondition : AICondition
{
    [SerializeField]
    private KeyCode keyCode;

    private bool isInput;

    public override bool IfCondition(AIState currentState, AIState nextState)
    {
        if (Input.GetKeyDown(keyCode))
        {
            isInput = true;
        }
        else isInput = false;

        return isInput;
    }
}
