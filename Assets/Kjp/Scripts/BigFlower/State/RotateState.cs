using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateState : AIState
{
    [SerializeField]
    private float _rotSpeed = 30f;
    [SerializeField]
    private bool isLeft = false;

    private BigFlower bigFlower;

    private void Start()
    {
        bigFlower = GetComponentInParent<BigFlower>();
    }

    public override void OnStateEnter()
    {
        transform.DOKill();

        if(bigFlower == null)
            bigFlower = GetComponentInParent<BigFlower>();

        foreach (GameObject go in bigFlower.RootList)
        {
            Animator animator = go.GetComponent<Animator>();
            animator.SetBool("isLeft", isLeft);
            animator.SetBool("isStop", false);
        }

        //foreach (GameObject go in bigFlower.RootList)
        //{
        //    go.transform.DORotate(new Vector3(0, 0, _rotSpeed * (isLeft ? 1 : -1)), 0.3f).SetEase(Ease.Linear).SetRelative().SetLoops(-1, LoopType.Incremental);
        //}
    }

    public override void OnStateLeave()
    {
        transform.DOKill();
        foreach (GameObject go in bigFlower.RootList)
        {
            Animator animator = go.GetComponent<Animator>();
            animator.SetBool("isStop", true);
        }
    }

    public override void TakeAAction()
    {
        foreach (GameObject go in bigFlower.RootList)
        {
            //go.transform.DORotate(new Vector3(0, 0, _rotSpeed * (isLeft ? 1 : -1)), 0.3f).SetEase(Ease.Linear).SetRelative().SetLoops(-1, LoopType.Incremental);
            //go.transform.rotation = Quaternion.Euler(0, 0, go.transform.rotation.z + _rotSpeed);
            go.transform.Rotate(new Vector3(0, 0, _rotSpeed * (isLeft ? 1 : -1)));
        }
    }
}
