using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentRenderer : MonoBehaviour
{
    protected SpriteRenderer _spriteRenderer = null;

    protected virtual void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeFaceOfSprite(bool value)
    {
        _spriteRenderer.flipX = value;
    }
    public void ChangeOrderInLayer(int num)
    {
        _spriteRenderer.sortingLayerID = num;
    }
}
