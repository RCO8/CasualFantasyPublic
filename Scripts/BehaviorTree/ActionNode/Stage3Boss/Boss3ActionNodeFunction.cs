
public abstract class Boss3ActionNodeFunction : EnemyActionNodeFunction
{
    protected Boss3_Battle enemy;
    protected Boss3AIController controller;

    public Boss3ActionNodeFunction(Boss3_Battle _enemy, Boss3AIController _con) : base(_enemy, _con)
    {
        enemy = _enemy;
        controller = _con;
    }
}
