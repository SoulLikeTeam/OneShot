using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmCore : MonoBehaviour
{
    /// <summary>
    /// Ʈ������ ��Ģ
    /// </summary>
    [System.Serializable]
    public class TransitionRule
    {
        [Tooltip("���� ����")]
        public FsmState Next;

        [Tooltip("��ȯ�� �߻��� ����")]
        public FsmCondition Cond;

        [Tooltip("�켱����")]
        public int Priority;

        [Tooltip("������ �������� ���� �� ��ȯ�� �߻��ϴ� ���")]
        public bool Not;

        [System.NonSerialized]
        public StateEntity StateEntityOfNext;
    }

    [System.Serializable]
    public class StateEntity
    {
        [Tooltip("���� �������")]
        public FsmState State;

        [Tooltip("��ȯ ��Ģ ����Ʈ")]
        public TransitionRule[] Transitions;
    }


    [Tooltip("���� ���¿� ������� �˻�Ǵ� ��ȯ ��Ģ")]
    public TransitionRule[] GlobalState;

    [Tooltip("�� FSM���� �߰ߵ� ��� ���� �� ��ȯ ��Ģ ���(ù��° ���� == �ʱ� ����)")]
    public StateEntity[] States;

    [Tooltip("���� ����")]
    private StateEntity current;


    /// <summary>
    /// ���� ���¿� �װͿ� ���� ��ȯ ��Ģ ��������
    /// </summary>
    public StateEntity GetCurrentState()
    {
        return current;
    }

    /// <summary>
    /// ���� ���� �޼ҵ�
    /// </summary>
    /// <param name="Next">������ ����</param>
    public void ChangeState(FsmState Next)
    {
        Debug.Log("LEAVE");
        //���� ���������� ����
        leaveCurrentState();
        current = null;

        //Next���°� ���ö����� �ݺ�
        foreach (StateEntity se in States)
        {
            if (Next == se.State)
            {
                //�h�� ���¸� Next ���·� ����
                current = se;
                break;
            }
        }

        //FsmCore�� �������� ���� ����
        if (current == null)
        {
            //���ο� StateEntity ����
            current = new StateEntity();
            //���ο� ������ ���¸� Next�� ����
            current.State = Next;
            current.Transitions = null;
        }

        //���� ���·� ����
        enterCurrentState();
    }

    void Start()
    {
        //���°� �ִٸ�
        if (States.Length > 0)
        {
            //�ʱ� ����
            current = States[0];

            //�ʱ� ������ ������ ������ ���¸� ��Ȱ����
            for (int i = 1; i < States.Length; ++i)
                if (States[i].State != null)
                    States[i].State.enabled = false;

            //���� ���·� ����
            enterCurrentState();
        }
    }


    void LateUpdate()
    {
        //Debug.Log(current.State.);
        //������ ��Ģ �ʱ⼼��
        TransitionRule chosenTransition = null;

        //���� ���¿� ������� �˻�Ǵ� ��ȯ ��Ģ���� ����
        for (int i = 0; i < GlobalState.Length; ++i)
        {
            TransitionRule tr = GlobalState[i];

            //(�˻�Ǵ� ��Ģ�� �ְ� ������ ��Ģ�� ������) �Ǵ� (�˻�Ǵ� ��Ģ�� �켱������)
            //�׸��� ������ �����Ǹ�(XOR�����ڸ� ���Ͽ� Notó���� ��)
            if (tr != null &&
                (chosenTransition == null || tr.Priority > chosenTransition.Priority) &&
                (tr.Cond != null && (tr.Not ^ tr.Cond.IsSatisfied(current != null ? current.State : null, tr.Next))))

                //������ ��Ģ�� ����
                chosenTransition = tr;
        }

        //���� ���¶� ���� ���¿� ���� ��Ģ�� �ִٸ�
        if (current != null && current.Transitions != null)
        {
            //��� ������ �� ��ȸ
            for (int i = 0; i < current.Transitions.Length; ++i)
            {
                //���� �ε����� ��Ģ ����
                TransitionRule tr = current.Transitions[i];

                //(�˻�Ǵ� ��Ģ�� �ְ� ������ ��Ģ�� ������) �Ǵ� (�˻�Ǵ� ��Ģ�� �켱������)
                //�׸��� ������ �����Ǹ�(XOR�����ڸ� ���Ͽ� Notó���� ��)
                if (tr != null &&
                    (chosenTransition == null || tr.Priority > chosenTransition.Priority) &&
                    (tr.Cond != null && (tr.Not ^ tr.Cond.IsSatisfied(current != null ? current.State : null, tr.Next))))

                    //������ ��Ģ�� ����
                    chosenTransition = tr;
            }
        }

        //������ ��Ģ�� �ִٸ�
        if (chosenTransition != null)
        {


            //���� ���¸� ������ ��Ģ�� ���� ���·� ����
            current = chosenTransition.StateEntityOfNext;

            //���� ���°� NULL�̸�(��Ģ�� ���� ���°� X)
            if (current == null)
            {

                // ������ ������ ���� ���°� ���ö����� �ݺ�
                foreach (StateEntity se in States)
                {
                    if (se != null && chosenTransition.Next == se.State)
                    {
                        //Setting
                        chosenTransition.StateEntityOfNext = se;
                        //���� ���¸� ����
                        current = se;
                        break;
                    }
                }

                //���� ���°� NULL�̸�(���� ���°� ����)
                if (current == null)
                {
                    // FsmCore�� ��ϵ��� ���� ���¿� ����

                    //����
                    current = new StateEntity();
                    //����
                    current.State = chosenTransition.Next;
                    current.Transitions = null;
                }
            }

            //���� ���·� ����
            enterCurrentState();
        }
    }

    /// <summary>
    /// ���� ���¸� ������
    /// </summary>
    private void leaveCurrentState()
    {
        //Debug.Log("LEAVE");
        if (current != null && current.State != null)
        {
            current.State.OnStateLeave();
            //���� ���� ��Ȱ��ȭ
            current.State.enabled = false;
        }
    }

    /// <summary>
    /// ���� ���� ������
    /// </summary>
    private void enterCurrentState()
    {
        if (current != null && current.State != null)
        {
            //���� ���� Ȱ��ȭ
            current.State.enabled = true;
            current.State.OnStateEnter();
        }
    }
}