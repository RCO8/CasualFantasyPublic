using System.Diagnostics;

public interface IState
{
    //�� ���·� ������ ��
    public void Enter();
    //�� ���¿��� ���� ��
    public void Exit();
    //Update
    public void Update();
    //���� ���� �� Update
    public void PhysicsUpdate();
}

public abstract class CharacterState
{
    public IState currentState;

    public void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }
}
