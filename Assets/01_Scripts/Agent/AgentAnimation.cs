using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public abstract class AgentAnimation : MonoBehaviour
{
    protected Animator _animator = null;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnAniamtionTrigger(string name)
    {
        _animator.SetTrigger(name);
    }
    public void FlagAnimation(string name, bool flag = true)
    {
        _animator.SetBool(name, flag);
    }
    public void SetFloatAnimation(string name,float value)
    {
        _animator.SetFloat(name, value);
    }
}
