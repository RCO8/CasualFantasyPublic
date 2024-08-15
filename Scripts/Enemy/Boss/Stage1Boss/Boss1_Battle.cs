using UnityEngine;
using System;

public class Boss1_Battle : Enemy
{
    public GameObject obstacle;                     //��ֹ� ������Ʈ

    public bool isRageMode { get; private set; } = false;       //�ݳ� ��� Ȯ�ο�
    public bool isGround { get; private set; } = false;         //���� �ִ��� Ȯ�ο�

    protected override void Awake()
    {
        base.Awake();

        //ĳ���� �Ŵ��� ����
        CharacterManager.instance.enemy = this;

        //�ݳ� ��������Ʈ
        enemyStatHandler.OnDamage += ChangeRageMode;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.gameObject.CompareTag(Define.TAG_WALL))
        {
            Debug.Log("TAG_WALL");

            //���� ���·� �����Ѵ�.
            controller.ChangeState(EnemyAIController.EBossState.STUN);
        }

        //���� ������ isGround = true�� �����. ( BT�� ���� ���� )
        if (collision.gameObject.CompareTag(Define.TAG_GROUND))
        {
            isGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //���� ������ isGround = true�� �����. ( BT�� ���� ���� )
        if (other.gameObject.CompareTag(Define.TAG_GROUND))
        {
            isGround = false;
        }
    }

    private void ChangeRageMode(float _none)
    {
        if(enemyStatHandler.currentHealth < enemyStatHandler.stat.maxHealth * 0.5f)
        {
            //���� ����
            enemyStatHandler.OnDamage -= ChangeRageMode;
            enemyStatHandler.stat.attackSpeed *= 1.5f;
            enemyStatHandler.stat.attackDamage *= 1.5f;
            isRageMode = true;
            controller.animator.speed = 1.5f;

            //�ݳ� ���·� ����
            controller.ChangeState(EnemyAIController.EBossState.RAGE);
        }
    }

    public void MakeObstacle(Vector2 _position)
    {
        Instantiate(obstacle, _position, Quaternion.identity).
            GetComponent<Boss1Obstacle>().damage = enemyStatHandler.stat.attackDamage;
    }


}
