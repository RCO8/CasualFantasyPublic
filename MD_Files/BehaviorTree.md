모든 노드들(Sequence, Selector, ActionNode)들은 인터페이스 INode를 모두 상속 받는다.<br>
INode에는 상태(성공, 실패, 진행중)와 실행하는 함수를 포함하고 있다.

첫 번째 보스의 BehaviorTree를 간단하게 표현하면 아래처럼 된다. 

![image_Diagram](https://github.com/user-attachments/assets/09ab6bdc-ef9b-4469-9213-990e948a22ae)

동작 방식은 준비 상태에 돌입하고 준비가 끝나면 Rush상태에 돌입한다.<br>
Rush상태에서 벽에 박으면 Stun상태에 돌입하게 된다.


#코드 뷰
```cs
//BehaviorTreeRunner
public interface INode
{
    public enum ENodeState
    {
        ENS_Running,
        ENS_Success,
        ENS_Failure,
    }

    public ENodeState Evaluate();
}

public class BehaviorTreeRunner
{
    INode _rootNode;

    public BehaviorTreeRunner(INode rootNode)
    {
        _rootNode = rootNode;
    }

    public void Operate()
    {
        _rootNode.Evaluate();
    }
}

//ActionNode
public sealed class ActionNode : INode
{
    Func<INode.ENodeState> _onUpdate = null;

    public ActionNode(Func<INode.ENodeState> onUpdate)
    {
        _onUpdate = onUpdate;
    }

    public INode.ENodeState Evaluate() => _onUpdate?.Invoke() ?? INode.ENodeState.ENS_Failure;
}

//Selector
public class SelectNode : INode
{
    List<INode> childs;

    public SelectNode(List<INode> _childs)
    {
        childs = _childs;
    }

    public INode.ENodeState Evaluate()
    {
        if (childs == null)
            return INode.ENodeState.ENS_Failure;

        foreach (var child in childs)
        {
            switch (child.Evaluate())
            {
                case INode.ENodeState.ENS_Running:
                    return INode.ENodeState.ENS_Running;
                case INode.ENodeState.ENS_Success:
                    return INode.ENodeState.ENS_Success;
            }
        }

        return INode.ENodeState.ENS_Failure;
    }
}

//Sequence
public class SequenceNode : INode
{
    List<INode> childs;

    public SequenceNode(List<INode> _childs)
    {
        childs = _childs;
    }

    public INode.ENodeState Evaluate()
    {
        if (childs == null || childs.Count == 0)
            return INode.ENodeState.ENS_Failure;

        foreach (var child in childs)
        {
            switch (child.Evaluate())
            {
                case INode.ENodeState.ENS_Running:
                    return INode.ENodeState.ENS_Running;
                case INode.ENodeState.ENS_Success:
                    continue;
                case INode.ENodeState.ENS_Failure:
                    return INode.ENodeState.ENS_Failure;
            }
        }

        return INode.ENodeState.ENS_Success;
    }
}
```
