using System.Collections.Generic;

public class SelectNode : INode
{
    //�ڽ� ����
    List<INode> childs;

    public SelectNode(List<INode> _childs)
    {
        //�����ڸ� ���� �ڽ� ���� ����
        childs = _childs;
    }

    public INode.ENodeState Evaluate()
    {
        //�ڽ� ������ ���ٸ� ���и� ��ȯ��
        if (childs == null)
            return INode.ENodeState.ENS_Failure;

        foreach (var child in childs)
        {
            //�ڽ� ������ ��� ��ȸ�Ѵ�. ��带 ������ ���� ���̰ų� �����̸� return�ϰ� ���� �ڽ� ���� �Ѿ��.
            switch (child.Evaluate())
            {
                case INode.ENodeState.ENS_Running:
                    return INode.ENodeState.ENS_Running;
                case INode.ENodeState.ENS_Success:
                    return INode.ENodeState.ENS_Success;
            }
        }

        //�ڽ� ������ �� ���и� ��ȯ�Ѵٸ� �� Select��嵵 ���и� ��ȯ��
        return INode.ENodeState.ENS_Failure;
    }
}
