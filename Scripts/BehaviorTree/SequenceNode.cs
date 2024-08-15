using System.Collections.Generic;

public class SequenceNode : INode
{
    //�ڽ� ����
    List<INode> childs;

    public SequenceNode(List<INode> _childs)
    {
        //�����ڸ� ���� �ڽ� ���� ����
        childs = _childs;
    }

    public INode.ENodeState Evaluate()
    {
        //�ڽ� ������ ���ٰų� �������� ������ ���и� ��ȯ��
        if (childs == null || childs.Count == 0)
            return INode.ENodeState.ENS_Failure;

        foreach (var child in childs)
        {
            //�ڽ� ������ ��� ��ȸ�Ѵ�. ��带 ������ ���� ���̰ų� �����̸� return�ϰ� ���� �ڽ� ���� �Ѿ��.
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

        //�ڽ� ������ �� ������ ��ȯ�Ѵٸ� �� Sequence��嵵 ������ ��ȯ��
        return INode.ENodeState.ENS_Success;
    }
}
