using UnityEngine;

public class EnemyAnimationData
{
    //공용
    [SerializeField] private string idleParameterName = Define_Boss1AniParam.READY_BOOL;
    [SerializeField] private string deathParameterName = Define_Boss1AniParam.DEATH_BOOL;
    [SerializeField] private string rageParameterName = Define_Boss1AniParam.RAGE_TRIGGER;
    [SerializeField] private string runParameterName = Define_Boss1AniParam.RUSH_BOOL;
    [SerializeField] private string stunParameterName = Define_Boss1AniParam.STUN_BOOL;
    [SerializeField] private string attackParameterName = Define_Boss3AniParam.DASH_TRIGGER;
    [SerializeField] private string skillParameterName = Define_Boss3AniParam.SMASH_TRIGGER;

    //스테이지3 보스
    [SerializeField] private string spawnParameterName = Define_Boss3AniParam.SPAWNAXE_TRIGGER;

    //스테이지4 보스
    [SerializeField] private string jumpParameterName = Define_Boss4AniParam.JUMPATTACK_TRIGGER;    //스매시
    [SerializeField] private string bounceParameterName = Define_Boss4AniParam.BOUNCE_BOOL;         //바운스
    [SerializeField] private string digParameterName = Define_Boss4AniParam.DIG_BOOL;               //땅 파기

    //공용
    public int IdleParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }
    public int DeathParameterHash { get; private set; }
    public int StunParameterHash { get; private set; }
    public int RageParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int SkillParameterHash { get; private set; }

    //스테이지3 보스
    public int SpawnParameterHash { get; private set; }

    //스테이지4 보스
    public int JumpParameterHash { get; private set; }  //스매시
    public int BounceParameterHash { get; private set; } //바운스
    public int DigParameterHash { get; private set; }   //땅 파기

    public virtual void Initialize()
    {
        //공용
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);
        DeathParameterHash = Animator.StringToHash(deathParameterName);
        StunParameterHash = Animator.StringToHash(stunParameterName);
        RageParameterHash = Animator.StringToHash(rageParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        SkillParameterHash = Animator.StringToHash(skillParameterName);

        //스테이지3 보스
        SpawnParameterHash = Animator.StringToHash(spawnParameterName);

        //스테이지4 보스
        JumpParameterHash = Animator.StringToHash(jumpParameterName);
        BounceParameterHash = Animator.StringToHash(bounceParameterName);
        DigParameterHash = Animator.StringToHash(digParameterName);
    }
}
