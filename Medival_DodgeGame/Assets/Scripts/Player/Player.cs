using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //private PlayerController playerController;
    private GameManager gm;

    private void Awake()
    {
        //playerController = GetComponent<PlayerController>();
        GameManager.Instance.player = gameObject;
    }

    private void Start()
    {
        gm = GameManager.Instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Arrow"))
        {
            // 화살 피격! 게임 종료
            gm.GameOver();
        }
    }
}
