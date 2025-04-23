using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent DangerZoneEnterEvent;

    [SerializeField] int hp;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            gameObject.SetActive(false);
            if (GameManager.Instance.isOnGame)
                GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���̾ �ʹ� �������°� ������, ������Ʈ ������ �ٲٱ�
        if (other.gameObject.layer == LayerMask.NameToLayer("DangerZone"))
        {
            DangerZoneEnterEvent?.Invoke();
            Debug.Log("���豸������");
        }
    }
}
