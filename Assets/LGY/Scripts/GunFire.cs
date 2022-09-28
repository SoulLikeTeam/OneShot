using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    void Update()
    {
        Vector2 len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);

        /*if (transform.rotation.z == 90)
        {
            transform.Rotate(transform.right * -1);
            //x값에 -1을 주어 좌우반전
        }
        else if (transform.rotation.z == 0|| transform.rotation.z==-90)
        {
            transform.Rotate(transform.right * 1);
            //x값에 1을 주어 원위치
        }*/
    }
}
