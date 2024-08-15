using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss1ActionNodeFunction : EnemyActionNodeFunction
{
    protected Boss1_Battle enemy;
    protected Boss1AIController controller;

    public Boss1ActionNodeFunction(Boss1_Battle _enemy, Boss1AIController _con) : base(_enemy, _con)
    {
        enemy = _enemy;
        controller = _con;
    }
}
