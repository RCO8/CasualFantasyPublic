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

    public event Action<float> OnDamage;        //대미지 받을 때
    public event Action<float> OnHeal;          //힐 받을 때
    public event Action OnDeath;                //죽을 때

    private void Awake()
    {
        //캐싱
        _enemy = GetComponent<Enemy>();

        //스탯 초기화
        InitializationStat();
    }

    private void InitializationStat()
    {
        //스탯 초기화
        stat = new EnemyStatData();
        stat.maxHealth = _enemyStatSO.maxHealth;
        stat.attackDamage = _enemyStatSO.attackDamage;
        stat.attackSpeed = _enemyStatSO.attackSpeed;
        stat.moveSpeed = _enemyStatSO.moveSpeed;

        //ID
        id = _enemyStatSO.id;

        //현재 체력 초기화
        currentHealth = stat.maxHealth;
    }

    //대미지 받을 때 함수 ( 힐을 받을 시 매개변수를 음수로 넣어주면 된다 )
    public void TakeDamage(float _damage)
    {
        //대미지나 힐을 받음
        currentHealth -= _damage;

        //힐이면 OnHeal이벤트 발생, 대미지면 OnDamage이벤트 발생
        if (_damage <= 0) OnHeal?.Invoke(_damage);
        if (_damage > 0) OnDamage?.Invoke(_damage);

        //0미만, 최대체력 초과로 넘어가지 않게 조정한다.
        currentHealth = Mathf.Clamp(currentHealth, 0.0f, stat.maxHealth);

        //0이하일 시, 죽을 때 이벤트를 불러온다.
        if (currentHealth <= 0.0f) OnDeath?.Invoke();

        _enemy.UpdateStat();
    }
}
