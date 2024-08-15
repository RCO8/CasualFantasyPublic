using UnityEngine;
using System;

public class Boss1_Battle : Enemy
{
    public GameObject obstacle;                     //장애물 오브젝트

    public bool isRageMode { get; private set; } = false;       //격노 모드 확인용
    public bool isGround { get; private set; } = false;         //땅에 있는지 확인용

    protected override void Awake()
    {
        base.Awake();

        //캐릭터 매니저 설정
        CharacterManager.instance.enemy = this;

        //격노 델리게이트
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

            //스턴 상태로 변경한다.
            controller.ChangeState(EnemyAIController.EBossState.STUN);
        }

        //땅에 있으면 isGround = true로 만든다. ( BT로 수정 예정 )
        if (collision.gameObject.CompareTag(Define.TAG_GROUND))
        {
            isGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //땅에 있으면 isGround = true로 만든다. ( BT로 수정 예정 )
        if (other.gameObject.CompareTag(Define.TAG_GROUND))
        {
            isGround = false;
        }
    }

    private void ChangeRageMode(float _none)
    {
        if(enemyStatHandler.currentHealth < enemyStatHandler.stat.maxHealth * 0.5f)
        {
            //스탯 증가
            enemyStatHandler.OnDamage -= ChangeRageMode;
            enemyStatHandler.stat.attackSpeed *= 1.5f;
            enemyStatHandler.stat.attackDamage *= 1.5f;
            isRageMode = true;
            controller.animator.speed = 1.5f;

            //격노 상태로 변경
            controller.ChangeState(EnemyAIController.EBossState.RAGE);
        }
    }

    public void MakeObstacle(Vector2 _position)
    {
        Instantiate(obstacle, _position, Quaternion.identity).
            GetComponent<Boss1Obstacle>().damage = enemyStatHandler.stat.attackDamage;
    }


}
