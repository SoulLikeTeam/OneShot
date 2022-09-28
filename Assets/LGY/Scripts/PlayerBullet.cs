using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float Speed; // �̵� �ӵ� 

    private void Start()
    {
        Invoke("DestroyBullet", 2); // 2�� �� ������� �Ѿ�
    }
    void Update()
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
        transform.Rotate(Vector2.up * Time.deltaTime); // ???
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
//||^^||//