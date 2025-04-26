using UnityEngine;

public class Arrow : Pooled_Obj
{
    float speed;
    public float Speed { get { return speed; } set { speed = value; } }
    
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // ȭ�� ������ Ȱ��ȭ �ɶ�, �߰��� ��� ���⿡
    protected override void OnPooledEnable()
    {
        base.OnPooledEnable();

        // ȭ�� �߻� ����
        if (audioSource != null) audioSource.Play();
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.GmState != GameState.OnGame) return;

        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (transform.position.magnitude > 3.5f)
        {
            gameObject.SetActive(false);
        }
    }

}
