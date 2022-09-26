using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class StateAttack : State<BossFSM>
{
    private CharacterController characterController;
    private Transform monsterTransform;

    public BossState state = BossState.ATTACK;

    public override void OnAwake()
    {
        characterController = stateMachineClass.GetComponent<CharacterController>();
        monsterTransform = stateMachineClass.GetComponent<Transform>();
    }

    public override void OnStart()
    {
        stateMachineClass.curState = BossState.ATTACK;
        // ����
    }

    public override void OnUpdate(float deltaTime)
    {
        Transform target = stateMachineClass.SearchEnemy();

        if(target)
        {
            // �¿�����ϱ�

        }
        else
        {
            stateMachine.ChangeState<StateIdle>();
        }
    }

    public override void OnHitEvent()
    {
        stateMachine.ChangeState<StateIdle>();
    }
}