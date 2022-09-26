using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateIdle : State<BossFSM>
{
    public BossState state = BossState.IDLE;

    public override void OnStart()
    {
        stateMachineClass.curState = BossState.IDLE;
    }
    public override void OnUpdate(float deltaTime)
    {
        Transform target = stateMachineClass.SearchEnemy();

        if(target)
        {
            if(stateMachineClass.GetFlagAttack)
            {
                stateMachine.ChangeState<StateAttack>();
            }
            else
            {
                stateMachine.ChangeState<StateMove>();
            }
        }
    }
}
