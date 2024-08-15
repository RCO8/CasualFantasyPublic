
public class Boss3Smash : Boss3ActionNodeFunction
{
    //스킬이 나갈 시간
    private float _shootSkillTime = 0.3f;
    //처음인지
    private bool _isFirst = true;

    public Boss3Smash(Boss3_Battle _enemy, Boss3AIController _con) : base(_enemy, _con)
    {
    }

    public override INode.ENodeState CheckNode()
    {
        //상태 확인
        return CheckFunction(EnemyAIController.EBossState.SMASH);
    }

    public INode.ENodeState SmashNode()
    {
        //애니메이션 재생 중인지 확인 ( 재생이 끝났다면 ENS_Success을 반환 )
        if (IsPlayingAnimation(Define_AnimationNodeName.SKILL_MAGIC) == 2)
        {
            //플레이어를 쳐다본다
            enemy.LookPlayer(CharacterManager.instance.battlePlayer.gameObject);

            //참격 발사
            enemy.SpawnSmashObject();

            //옵션 초기화
            _isFirst = true;

            //StandBy 상태 변경
            controller.ChangeState(EnemyAIController.EBossState.STANDBY);

            return INode.ENodeState.ENS_Success;
        }

        //처음 이 노드를 시작하면 애니메이션 재생
        if(_isFirst)
        {
            _isFirst = false;

            //플레이어를 쳐다본다
            enemy.LookPlayer(CharacterManager.instance.battlePlayer.gameObject);

            //Smash 애니메이션 재생
            controller.animator.SetTrigger(enemy.bossAnimationData.SkillParameterHash);
        }

        return INode.ENodeState.ENS_Running;
    }
}
