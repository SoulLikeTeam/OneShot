using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float Speed; // 이동 속도 

    private void Start()
    {
        Invoke("DestroyBullet", 2); // 2초 뒤 사라지는 총알
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