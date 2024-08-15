using DG.Tweening;
using UnityEngine;

public class Boss3_Battle : Enemy
{
    //스매시 오브젝트
    [SerializeField] private GameObject _smashObject;
    //도끼 오브젝트
    [SerializeField] private GameObject _axeObject;

    //대시어택할 때, 애니메이션 시간
    private float _dashAttackTime = 0.4f - 0.05f;

    //점프가 끝났는지 확인용 변수
    public bool isJumpEnd { get; private set; } = false;


    protected override void Awake()
    {
        base.Awake();

        //캐릭터 매니저 설정
        CharacterManager.instance.enemy = this;
    }

    public void SpawnSmashObject()
    {
        float _rl = 1.0f;                       //우: -1, 좌: 1
        float _x = 1.3f;                        //x좌표 조정
        float _y = 1.3f;                        //y좌표 조정
        float _dur = 1.0f;                     //오브젝트 목적지까지 날아가는 시간
        float _range = 15.0f;
        Vector2 _bossPos = transform.position;  //보스 위치
        Vector2 _spawnPos;                      //오브젝트 소환될 위치
        Vector2 _dir;                           //오브젝트가 날라갈 방향

        //왼쪽을 바라보고 있으면
        if (transform.localScale.x > 0)
        {
            _x *= -1.0f;
            _spawnPos = _bossPos + new Vector2(_x, _y);
            _dir = _spawnPos - new Vector2(_range, 0.0f);
        }
        else
        {
            _rl = -1.0f;
            _spawnPos = _bossPos + new Vector2(_x, _y);
            _dir = _spawnPos + new Vector2(_range, 0.0f);
        }

        //물체 소환
        var _obj = Instantiate(_smashObject, _spawnPos, Quaternion.Euler(0.0f, 0.0f, -90.0f)).GetComponent<Boss1Obstacle>();

        //그냥 던질 때
        if (Random.Range(0, 2) == 0)
        {
            _obj.MoveInQuadObject(_dir, _dur);
            _obj.transform.DORotate(new Vector3(0.0f, 0.0f, 90.0f * _rl), 0.01f, RotateMode.Fast);
        }
        //돌아오는 도끼를 던질 때
        else
        {
            _obj.MoveYoYoObject(_dir, _dur);
            _obj.transform.DORotate(new Vector3(0.0f, 0.0f, 720.0f), _dur, RotateMode.FastBeyond360);
        }

        //대미지 설정
        _obj.damage = enemyStatHandler.stat.attackDamage;
    }

    public void JumpToPlayer()
    {
        //플레이어 오브젝트
        GameObject _player = CharacterManager.instance.basePlayer.gameObject;

        //점프 시작
        float _x = Mathf.Clamp(_player.transform.position.x, Define.BATTLESCENE_LEFTWALL_X - 1.0f,
            Define.BATTLESCENE_RIGHTWALL_X + 0.5f);
        float _y = gameObject.transform.position.y;
        Vector2 targetPos = new Vector2(_x, _y);
        transform.DOJump(targetPos, 0.1f, 1, _dashAttackTime).OnStart(SetJumpStart)
            .OnComplete(SetJumpEnd);
    }

    private void SetJumpStart()
    {
        isJumpEnd = false;
    }

    private void SetJumpEnd()
    {
        isJumpEnd = true;
    }
}
