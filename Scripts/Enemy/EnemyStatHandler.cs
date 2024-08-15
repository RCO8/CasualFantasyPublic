using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatHandler : MonoBehaviour
{
    public EnemyStatData stat { get; private set; }
    public float currentHealth { get; private set; }
    [SerializeField] private EnemyStatSO _enemyStatSO;
    private Enemy _enemy;

    //ID
    public int id { get; private set; }

    public event Action<float> OnDamage;        //����� ���� ��
    public event Action<float> OnHeal;          //�� ���� ��
    public event Action OnDeath;                //���� ��

    private void Awake()
    {
        //ĳ��
        _enemy = GetComponent<Enemy>();

        //���� �ʱ�ȭ
        InitializationStat();
    }

    private void InitializationStat()
    {
        //���� �ʱ�ȭ
        stat = new EnemyStatData();
        stat.maxHealth = _enemyStatSO.maxHealth;
        stat.attackDamage = _enemyStatSO.attackDamage;
        stat.attackSpeed = _enemyStatSO.attackSpeed;
        stat.moveSpeed = _enemyStatSO.moveSpeed;

        //ID
        id = _enemyStatSO.id;

        //���� ü�� �ʱ�ȭ
        currentHealth = stat.maxHealth;
    }

    //����� ���� �� �Լ� ( ���� ���� �� �Ű������� ������ �־��ָ� �ȴ� )
    public void TakeDamage(float _damage)
    {
        //������� ���� ����
        currentHealth -= _damage;

        //���̸� OnHeal�̺�Ʈ �߻�, ������� OnDamage�̺�Ʈ �߻�
        if (_damage <= 0) OnHeal?.Invoke(_damage);
        if (_damage > 0) OnDamage?.Invoke(_damage);

        //0�̸�, �ִ�ü�� �ʰ��� �Ѿ�� �ʰ� �����Ѵ�.
        currentHealth = Mathf.Clamp(currentHealth, 0.0f, stat.maxHealth);

        //0������ ��, ���� �� �̺�Ʈ�� �ҷ��´�.
        if (currentHealth <= 0.0f) OnDeath?.Invoke();

        _enemy.UpdateStat();
    }
}
