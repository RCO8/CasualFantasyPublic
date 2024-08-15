using System.Collections;
using UnityEngine;

public class Boss3Bomb : MonoBehaviour
{
    private Rigidbody2D rgdby;
    private float playerDistance;
    private float randomDistance;

    private void Awake()
    {
        rgdby = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 dir)
    {
        //�÷��̾� �������� ��ȯ
        playerDistance = Vector2.Distance(transform.position ,CharacterManager.instance.basePlayer.transform.position);

        randomDistance = Random.Range(playerDistance / 2, playerDistance);   //���� �÷��̾� ��ġ�ݿ��� �÷��̾��� ����ġ����

        rgdby.AddForce(dir * randomDistance, ForceMode2D.Impulse);   //�������� ������ �Ÿ�
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
