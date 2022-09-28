using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState
{
    IDLE,
    MOVE,
    ATTACK
}

public class BossFSM : MonoBehaviour
{
    private StateMachine<BossFSM> fsmManager;

    public LayerMask targetLayerMask;
    public float eyeSight;              // 시야 범위
    public Transform target;            // 판별된 타겟
    public float attackRange;           // 공격 범위

    public BossState curState = BossState.MOVE; // 현재 State


    public bool GetFlagAttack
    {
        get
        {
            if (target == null) return false;

            float distance = Vector2.Distance(transform.position, target.position);

            bool isTrue = distance <= attackRange;

            return (distance <= attackRange);
        }
    }

    private void Start()
    {
        fsmManager = new StateMachine<BossFSM>(this, new StateIdle());
        fsmManager.AddStateList(new StateMove());
        fsmManager.AddStateList(new StateAttack());
    }

    private void Update()
    {
        fsmManager.OnUpdate(Time.deltaTime);
    }

    // 타겟 찾기
    public Transform SearchEnemy()
    {
        target = null;

        Collider2D[] findTargets = Physics2D.OverlapCircleAll(transform.position, eyeSight, targetLayerMask);

        if (findTargets.Length > 0) target = findTargets[0].transform;

        return target;
    }

}
