using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerAttack : MonoBehaviour
{
    private Player_Battle _player;

    public bool first = true;           //ù ��������
    public int combo = 0;               //�� ��° �޺�����
    public bool onNextCombo = false;    //Ÿ�ֿ̹� �°� ���� ���� �޺� �Ѿ �� �ִ���
    public bool isAttacking = false;    //����������

    private void Awake()
    {
        _player = GetComponent<Player_Battle>();
    }

    private void Start()
    {
        _player.controller.OnBattleAttackEvent += Attack;
    }

    public void Attack()
    {
        isAttacking = true;

        if (first)
        {
            _player.animator.SetTrigger(_player.animationData.AttackParameterHash);
            first = false;
            combo++;
        }
        else if (combo != 3) onNextCombo = true;
    }
    public void ComboAttack(int _combo)
    {
        if (_combo == 3)
        {
            EndCombo();
            return;
        }

        onNextCombo = false;
        _player.animator.SetFloat(_player.animationData.ComboStateParameterHash, (float)combo);
        _player.animator.SetTrigger(_player.animationData.AttackParameterHash);
        combo++;
    }

    public void EndCombo()
    {
        _player.animator.SetFloat(_player.animationData.ComboStateParameterHash, 0.0f);
        first = true;
        combo = 0;
        onNextCombo = false;
        isAttacking = false;
    }
}
