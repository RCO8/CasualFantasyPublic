public interface INode
{
    //상태 enum
    public enum ENodeState
    {
        ENS_Running,    //실행 중
        ENS_Success,    //성공
        ENS_Failure,    //실패
    }

    //인터페이스에서 Node의 상태와 노드가 어떤 상태인지를 반환하는 메소드
    public ENodeState Evaluate();
}

public class BehaviorTreeRunner
{
    //루트 노드
    INode _rootNode;

    public BehaviorTreeRunner(INode rootNode)
    {
        //생성자를 통해 루트 노드 설정
        _rootNode = rootNode;
    }

    //루트 노드 실행
    public void Operate()
    {
        _rootNode.Evaluate();
    }
}