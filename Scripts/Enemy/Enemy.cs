using System.Collections;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public EnemyStatHandler enemyStatHandler {  get; protected set; }
    public EnemyAIController controller { get; protected set; }
    public EnemyAnimationData bossAnimationData { get; private set; }   //애니메이션 데이터
    public BoxCollider2D damageCollider;    //적 몸통에 맞으면 대미지 처리가 되는 공격이 있을 때만 할당한다.

    public event Action OnChangeStat;                           //스탯바뀔 때, UI변경용 델리게이트

    protected virtual void Awake()
    {
        //캐싱
        enemyStatHandler = GetComponent<EnemyStatHandler>();
        controller = GetComponent<EnemyAIController>();

        //델리게이트
        enemyStatHandler.OnDeath += EnemyDeath;     //적이 죽을 때
        enemyStatHandler.OnDamage += TakeDamage;    //적이 피해를 입을 때

        //애니메이션 데이터 초기화
        bossAnimationData = new EnemyAnimationData();
        bossAnimationData.Initialize();
    }

    protected virtual void Start()
    {
        OnChangeStat += BattleStatUI.instance.EnemyUpdateUI;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Define.TAG_PLAYER))
        {
            Player_Battle _player = CharacterManager.instance.basePlayer as Player_Battle;

            if (_player != null)
            {
                _player.TakeDamage_KnockBack(enemyStatHandler.stat.attackDamage);
            }
        }
    }

    public virtual void EnemyDeath()
    {
        //상태 변경
        controller.ChangeState(EnemyAIController.EBossState.DEATH);

        StartCoroutine(DestroyThisObject());
    }

    IEnumerator DestroyThisObject()
    {
        yield return new WaitForSeconds(3.0f);

        //다시 필드로 돌아가는 코드 작성
        SceneDataManager.instance.BackFieldScene();

        //캐릭터 매니저
        CharacterManager.instance.enemy = null;
    }

    private void TakeDamage(float _damage)
    {
        Vector2 _pos = gameObject.transform.position;
        _pos += Vector2.up * 3.5f;

        Vector2 _viewportPos = Camera.main.WorldToScreenPoint(_pos);

        //데미지 텍스트 띄우기
        DamageTextController.instance.CreateDamageText(_viewportPos, _damage);

        //흔들린다
        if (CharacterManager.instance.basePlayer.transform.position.x < gameObject.transform.position.x)
        {
            //플레이어가 왼쪽에 있다면 오른쪽으로 흔들린다
            gameObject.transform.DOPunchPosition(Vector2.right * 0.15f, 0.1f);
        }
        else
        {
            //플레이어가 오른쪽에 있다면 왼쪽으로 흔들린다
            gameObject.transform.DOPunchPosition(Vector2.left * 0.15f, 0.1f);
        }
    }

    public void UpdateStat()
    {
        OnChangeStat?.Invoke();
    }

    public void LookPlayer(GameObject _gameObject)
    {
        Vector2 nowScale = gameObject.transform.localScale;
        if (_gameObject.transform.position.x > gameObject.transform.position.x)
        {
            nowScale.x = Mathf.Abs(nowScale.x) * -1f;
        }
        else
        {
            nowScale.x = Mathf.Abs(nowScale.x) * 1f;
        }
        gameObject.transform.localScale = nowScale;
    }
}
