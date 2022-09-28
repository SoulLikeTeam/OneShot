using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float dashPower;
    [SerializeField] private Image coolDownImage;
    [SerializeField] private LayerMask mask;

    #region dashCameraShake
    [Header("__CameraShake__")]
    [SerializeField] private float cameraShakeDuration = 0.1f;
    [SerializeField] private float cameraShakeStrength = 0.5f;
    [SerializeField] private int cameraShakeVibrato = 100;
    [SerializeField] private float cameraShakeRandomness = 0f;
    #endregion

    private int dashCount = 2;
    private bool isDash;
    private bool isDashCoolDown;
    private Camera mainCamera;
    private Rigidbody2D playerRigid;
    private Vector2 dir;
    private Vector2 lastDashPos = Vector2.one;
    private IEnumerator dashReCoolDownCo;

    private void Awake()
    {

        mainCamera = FindObjectOfType<Camera>();
        playerRigid = GetComponent<Rigidbody2D>();
        dashReCoolDownCo = DashReCoolDownCO();

    }

    private void Update()
    {

        Dash();
        if(isDash == false) Move();

    }

    private void Dash()
    {

        dashCount = Mathf.Clamp(dashCount, 0, 2);

        if (Input.GetKeyDown(KeyCode.Space) && isDashCoolDown == false)
        {

            dashCount--;
            isDash = true;
            dir = DashPosSet();            
            if (dashCount != 0) StartCoroutine(DashCO());
            if (dashCount == 1) StartCoroutine(dashReCoolDownCo);

        }

        if (isDash == true)
        {

            transform.position = Vector2.MoveTowards(transform.position, dir, dashPower * Time.deltaTime);

        }

        if (transform.position == (Vector3)dir && isDash == true && dashCount != 0)
        {

            isDash = false;            

        }
        else if(dashCount == 0 && transform.position == (Vector3)dir)
        {

            mainCamera.DOShakePosition(cameraShakeDuration, cameraShakeStrength, cameraShakeVibrato, cameraShakeRandomness);
            StartCoroutine(DashDelayCO());
            StopCoroutine(dashReCoolDownCo);

            Collider2D col = Physics2D.OverlapBox(transform.position, new Vector2(2f, 2f), 0, mask);

            if (col != null)
            {

                DashAttack(col);

            }

        }

    }

    private void DashAttack(Collider2D col)
    {

        Debug.Log(col.gameObject.name);

    }

    private void Move()
    {

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        bool value = inputX != 0 || inputY != 0;

        float slowSpeed = value ? 1f : 0.5f;

        Vector2 dir = new Vector2(inputX, inputY);
        playerRigid.velocity = dir * speed * slowSpeed;

        float inputRawX = Input.GetAxisRaw("Horizontal");
        float inputRawY = Input.GetAxisRaw("Vertical");

        if(inputRawX != 0 && inputRawY == 0)
        {

            lastDashPos = new Vector2(inputRawX, inputRawY);

        }
        else if(inputRawY != 0 && inputRawX == 0)
        {

            lastDashPos = new Vector2(inputRawX, inputRawY);

        }
        else if(inputRawY != 0 && inputRawX != 0)
        {

    
            lastDashPos = new Vector2(inputRawX, inputRawY);

        }
    }

    private Vector2 DashPosSet()
    {

        Vector2 dir;

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        if(inputX == 0 && inputY == 0)
        {

            inputX = lastDashPos.x;
            inputY = lastDashPos.y;

        }
        
        dir = new Vector2(transform.position.x + (1.5f * inputX), transform.position.y + (1.5f * inputY));

        return dir;

    }

    IEnumerator DashCO()
    {
        
        isDashCoolDown = true;
        yield return new WaitForSeconds(0.3f);
        isDashCoolDown = false;

    }

    IEnumerator DashDelayCO()
    {

        dashCount = 2;
        isDashCoolDown = true;
        
        while(coolDownImage.fillAmount != 1)
        {

            coolDownImage.fillAmount += 0.01f;
            yield return new WaitForSeconds(0.01f);

        }

        coolDownImage.fillAmount = 0;

        isDashCoolDown = false;

    }

    IEnumerator DashReCoolDownCO()
    {

        yield return new WaitForSeconds(5);
        if(dashCount == 1 && isDashCoolDown == false)
        {

            dashCount = 2;

        }

    }

}
