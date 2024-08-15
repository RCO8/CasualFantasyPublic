using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss4ActionNodeFunction : EnemyActionNodeFunction
{
    protected Boss4_Battle enemy;
    protected Boss4AIController controller;

    public Boss4ActionNodeFunction(Boss4_Battle _enemy, Boss4AIController _con) : base(_enemy, _con)
    {
        enemy = _enemy;
        controller = _con;
    }
}
