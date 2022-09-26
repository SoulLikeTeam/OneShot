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
    public float eyeSight;              // �þ� ����
    public Transform target;            // �Ǻ��� Ÿ��
    public float attackRange;           // ���� ����

    public BossState curState = BossState.MOVE; // ���� State


    public bool GetFlagAttack
    {
        get
        {
            if (target == null)
                return false;

            float distance = Vector2.Distance(transform.position, target.position);

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
        Debug.Log(curState);
    }

    // Ÿ�� ã��
    public Transform SearchEnemy()
    {
        target = null;

        Collider2D[] findTargets = Physics2D.OverlapCircleAll(transform.position, eyeSight, targetLayerMask);

        if (findTargets.Length > 0)
            target = findTargets[0].transform;


        return target;
    }

}
