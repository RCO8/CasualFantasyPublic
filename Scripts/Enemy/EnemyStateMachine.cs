using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : CharacterState
{
    public Enemy enemy { get; }

    //�� ����

    public EnemyStateMachine (Enemy _enemy)
    {
        enemy = _enemy;

        //�� ����
    }
}
