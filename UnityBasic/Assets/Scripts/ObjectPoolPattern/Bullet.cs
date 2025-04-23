using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : PooledObject
{
    [SerializeField] GameObject effect;
    [SerializeField] float returnTime;
    [SerializeField] float shotPower;
    [SerializeField] float defaultShotPower;
    [SerializeField] int shotDamage;
    Rigidbody rigid;
    
    float timer;
    bool crashed;
    

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        timer = returnTime;
        rigid.AddForce(transform.forward * shotPower, ForceMode.Impulse);
    }

    // 터짐 효과 & 탄알 설정 초기화
    public void Boom()
    {
        Instantiate(effect, this.transform.position, this.transform.rotation);
        crashed = false;
        rigid.velocity = Vector3.zero;
        shotPower = defaultShotPower;
        objectPool.pool.Add(this.gameObject);
    }

    public void Charged(float gauze)
    {
        shotPower += gauze;
    }

    // Update is called once per frame
    void Update()
    {
        if (rigid.velocity.magnitude > 2) // 속도가 충분히 빠르면
        {
            // 앞방향을 속도방향으로 맞춰준다.
            transform.forward = rigid.velocity;
        }
        
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        if (timer <= 0 || crashed)
            Boom();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (crashed) return;
        crashed = true;

        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(shotDamage);
        }

        gameObject.SetActive(false);
    }
}
