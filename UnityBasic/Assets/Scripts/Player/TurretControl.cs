using System.Collections;
using UnityEngine;

public class TurretControl : MonoBehaviour
{
    float rotateState_H = 0;
    //float rotateState_V = 0;

    [SerializeField] float rotateSpeed;
    [SerializeField] float shotGauge;
    [SerializeField] float maxGauge = 10;
    [SerializeField] float minGauge = 1;
    [SerializeField] float shotCoolTime;
    bool shotCool = false;

    Coroutine chargineCor;
    Coroutine shotCoolCor;

    [SerializeField] FixedJoystick joy;
    void Update()
    {
        // 탱크 머리 돌아가기
        if (Input.GetKey(KeyCode.A))
            rotateState_H = -1;
        else if (Input.GetKey(KeyCode.D))
            rotateState_H = 1;
        else
            rotateState_H = 0;

        /*if (joy.Horizontal < 0)
            rotateState_H = -1;
        else if (joy.Horizontal > 0)
            rotateState_H = 1;
        else
            rotateState_H = 0;*/

        // 대포 발사
        if (Input.GetKey(KeyCode.W))
        {
            if (!shotCool)
                gameObject.GetComponent<Turret>().Shot();

            if (shotCoolCor == null)
                shotCoolCor = StartCoroutine(ShotCoolDown());
        }

        // 차지샷
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //shotGauge 올라가는 코루틴
            Debug.Log("차징 시작");

            if (chargineCor == null)
                chargineCor = StartCoroutine(ChargingCoroutine());
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (!shotCool)
                gameObject.GetComponent<Turret>().ChargeShot(shotGauge);

            if (shotCoolCor == null)
                shotCoolCor = StartCoroutine(ShotCoolDown());

            StopCoroutine(chargineCor);
            
            shotGauge = 0;
            chargineCor = null;
        }
    }

    IEnumerator ShotCoolDown()
    {
        shotCool = true;
        yield return new WaitForSeconds(shotCoolTime);
        shotCool = false;
        shotCoolCor = null;
    }
    IEnumerator ChargingCoroutine()
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime * 20 * maxGauge;
            yield return new WaitForSeconds(0.1f);
            shotGauge = Mathf.Clamp(timer, minGauge, maxGauge);
            Debug.Log($"{shotGauge}, {timer}");
        }
    }

    private void FixedUpdate()
    {
        if (rotateState_H != 0)
            transform.Rotate(Vector3.up, rotateState_H * Time.deltaTime * rotateSpeed);
    }


}
