using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class StateMove : State<BossFSM>
{
    private CharacterController characterController;
    private Transform monsterTransform;
    private float monsterSpeed = 10.0f;

    public BossState state = BossState.MOVE;

    public override void OnAwake()
    {
        characterController = stateMachineClass.GetComponent<CharacterController>();
        monsterTransform = stateMachineClass.GetComponent<Transform>();
    }

    public override void OnStart()
    {
        stateMachineClass.curState = BossState.MOVE;
    }

    public override void OnUpdate(float deltaTime)
    {
        Transform target = stateMachineClass.SearchEnemy();
        
        if(target)
        {
            stateMachineClass.curState = BossState.MOVE;

            Vector2 dir = target.position - monsterTransform.position;

            
            monsterTransform.Translate(dir.normalized * Time.deltaTime);
        }
        else
        {
            stateMachine.ChangeState<StateIdle>();
        }
    }
    


}