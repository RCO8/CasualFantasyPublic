//이 클래스의 존재 이유는 string 매개변수를 넣을 때, 오타로 인한 오류를 방지하고자 만들었다.
public class Define
{
    //인풋액션에셋 이름
    public const string ACTIONASSET_BATTLE = "Battle";
    public const string ACTIONASSET_FIELD = "Field";

    //태그 이름
    public const string TAG_PLAYER = "Player";
    public const string TAG_GROUND = "Ground";
    public const string TAG_WALL = "Wall";
    public const string TAG_ENEMY = "Enemy";
    public const string TAG_PLAYERATTACK = "PlayerAttack";
    public const string TAG_ENEMYATTACK = "EnemyAttack";

    //씬 이름
    public const string SCENE_BATTLE = "BattleScene";

    //배틀 씬 양 끝 좌표
    public const float BATTLESCENE_RIGHTWALL_X = 7.2f;
    public const float BATTLESCENE_LEFTWALL_X = -6.7f;

    //장비 리스트 순서
    public const int HAIR_NUM = 0;
    public const int HELMET_NUM = 2;
    public const int TOP_BODY_NUM = 3;
    public const int TOP_RIGHT_NUM = 4;
    public const int TOP_LEFT_NUM = 5;
    public const int PANTS_RIGHT_NUM = 6;
    public const int PANTS_LEFT_NUM = 7;
    public const int WEAPON_NUM = 11;
    public const int CLOAK_NUM = 15;
}

//상태 애니메이션 노드들의 이름
public class Define_AnimationNodeName
{
    public const string IDLE = "0_idle";
    public const string RUN = "1_Run";
    public const string STUN = "3_Debuff_Stun";
    public const string DEATH = "4_Death";

    public const string ATTACK_NORMAL = "2_Attack_Normal";
    public const string ATTACK_BOW = "2_Attack_Bow";
    public const string ATTACK_MAGIC = "2_Attack_Magic";

    public const string SKILL_NORMAL = "5_Skill_Normal";
    public const string SKILL_BOW = "5_Skill_Bow";
    public const string SKILL_MAGIC = "5_Skill_Magic";
}

//스테이지1 보스 애니메이션 파라미터
public class Define_Boss1AniParam
{
    public const string RUSH_BOOL = "Run";
    public const string READY_BOOL = "Idle";
    public const string DEATH_BOOL = "Death";
    public const string STUN_BOOL = "Stun";
    public const string RAGE_TRIGGER = "Rage";
}

//스테이지3 보스 애니메이션 파라미터
public class Define_Boss3AniParam
{
    public const string DASH_TRIGGER = "Attack";
    public const string READY_BOOL = "Idle";
    public const string DEATH_BOOL = "Death";
    public const string SMASH_TRIGGER = "Skill";
    public const string SPAWNAXE_TRIGGER = "Spawn";
}

//스테이지4 보스 애니메이션 파라미터
public class Define_Boss4AniParam
{
    public const string JUMPATTACK_TRIGGER = "Attack";
    public const string READY_BOOL = "Idle";
    public const string DEATH_BOOL = "Death";
    public const string DIG_BOOL = "Jump";
    public const string BOUNCE_BOOL = "Jump";
    public const string STUN_BOOL = "Stun";
}