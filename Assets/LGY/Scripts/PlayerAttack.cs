using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bullet;
    public Transform pos;

    public float cooldown;
    private float curtime;

    void Update()
    {
        /*Vector2 len =Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);*/ // <== 이거 왜있는거였지

        if (curtime <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Instantiate(bullet, pos.position, transform.rotation);
            }
            curtime = cooldown;
        }
        curtime -= Time.deltaTime;
    }
}
