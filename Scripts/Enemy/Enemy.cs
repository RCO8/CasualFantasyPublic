using System.Collections;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public EnemyStatHandler enemyStatHandler {  get; protected set; }
    public EnemyAIController controller { get; protected set; }
    public EnemyAnimationData bossAnimationData { get; private set; }   //�ִϸ��̼� ������
    public BoxCollider2D damageCollider;    //�� ���뿡 ������ ����� ó���� �Ǵ� ������ ���� ���� �Ҵ��Ѵ�.

    public event Action OnChangeStat;                           //���ȹٲ� ��, UI����� ��������Ʈ

    protected virtual void Awake()
    {
        //ĳ��
        enemyStatHandler = GetComponent<EnemyStatHandler>();
        controller = GetComponent<EnemyAIController>();

        //��������Ʈ
        enemyStatHandler.OnDeath += EnemyDeath;     //���� ���� ��
        enemyStatHandler.OnDamage += TakeDamage;    //���� ���ظ� ���� ��

        //�ִϸ��̼� ������ �ʱ�ȭ
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
        //���� ����
        controller.ChangeState(EnemyAIController.EBossState.DEATH);

        StartCoroutine(DestroyThisObject());
    }

    IEnumerator DestroyThisObject()
    {
        yield return new WaitForSeconds(3.0f);

        //�ٽ� �ʵ�� ���ư��� �ڵ� �ۼ�
        SceneDataManager.instance.BackFieldScene();

        //ĳ���� �Ŵ���
        CharacterManager.instance.enemy = null;
    }

    private void TakeDamage(float _damage)
    {
        Vector2 _pos = gameObject.transform.position;
        _pos += Vector2.up * 3.5f;

        Vector2 _viewportPos = Camera.main.WorldToScreenPoint(_pos);

        //������ �ؽ�Ʈ ����
        DamageTextController.instance.CreateDamageText(_viewportPos, _damage);

        //��鸰��
        if (CharacterManager.instance.basePlayer.transform.position.x < gameObject.transform.position.x)
        {
            //�÷��̾ ���ʿ� �ִٸ� ���������� ��鸰��
            gameObject.transform.DOPunchPosition(Vector2.right * 0.15f, 0.1f);
        }
        else
        {
            //�÷��̾ �����ʿ� �ִٸ� �������� ��鸰��
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
