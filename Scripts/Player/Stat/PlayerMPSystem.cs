using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMPSystem : MonoBehaviour
{
    private PlayerStatHandler statHandler;

    public event Action<float> OnUseMana;        //���� ����� ��
    public event Action<float> OnFillMana;       //���� �� ��

    public float currentMana { get; set; }
    //public float maxMana => statHandler.currentStats.maxHealth;
    public float maxMana { get; set; }

    private void Awake()
    {
        statHandler = GetComponent<PlayerStatHandler>();
    }

    private void Start()
    {
        maxMana = statHandler.currentStats.maxMana;
        currentMana = maxMana;
    }

    public void UseMana(float _mana)
    {
        if (_mana <= 0) OnFillMana?.Invoke(_mana);
        else
        {
            if (currentMana < _mana) return;
            else OnUseMana?.Invoke(_mana);
        }

        //���� ���
        currentMana -= _mana;

        //0�̸�, �ִ븶�� �ʰ��� �Ѿ�� �ʰ� �����Ѵ�.
        currentMana = Mathf.Clamp(currentMana, 0.0f, maxMana);

        CharacterManager.instance.basePlayer.UpdateStat();
    }
}
