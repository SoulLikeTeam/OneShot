using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    //private List<AIState> _stateList;

    [SerializeField]
    private GameObject _target;

    private AIState _beforeState;
    [SerializeField]
    private AIState _currentState;

    public void ChangeState(AIState state)
    {
        _beforeState = _currentState;
        _currentState = state;
        _beforeState.OnStateLeave();
        _currentState.OnStateEnter();
    }

    //private void Awake()
    //{
    //    transform.GetComponentsInChildren<AIState>(_stateList);
    //}

    private void LateUpdate()
    {
        if (_target != null)
        {
            _currentState.TakeAAction();

            //AIState nextState = null;
            ConditionPair nextCondition = null;
            foreach (ConditionPair pair in _currentState._transitionList)
            {
                bool isTransition = true;
                for (int i = 0; i < pair.conditionList.Count; i++)
                {
                    if (pair.conditionList[i].IfCondition(_currentState, pair.nextState))
                    {
                        isTransition = true;
                    }
                    else isTransition = false;
                }

                if (isTransition == true)
                {
                    if (nextCondition == null)
                    {
                        nextCondition = pair;
                    }
                    else
                    {
                        if (pair.priority > nextCondition.priority)
                        {
                            nextCondition = pair;
                        }
                    }
                }
            }

            if(nextCondition != null)
            {
                ChangeState(nextCondition.nextState);
            }

        }
        else
        {
            return;
        }
    }

}