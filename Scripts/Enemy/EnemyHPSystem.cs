using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPSystem : MonoBehaviour
{
    private Enemy _enemy;

    public event Action<float> OnDamage;        //����� ���� ��
    public event Action<float> OnHeal;          //�� ���� ��
    public event Action OnDeath;                //���� ��

    public float currentHealth { get; set; }
    public float maxHealth;

    void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    void Start()
    {
        //maxHealth = _enemy.stat.maxHealth;
        currentHealth = maxHealth;
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
        currentHealth = Mathf.Clamp(currentHealth, 0.0f, maxHealth);

        //0������ ��, ���� �� �̺�Ʈ�� �ҷ��´�.
        if (currentHealth <= 0.0f) OnDeath?.Invoke();

        _enemy.UpdateStat();
    }
}
