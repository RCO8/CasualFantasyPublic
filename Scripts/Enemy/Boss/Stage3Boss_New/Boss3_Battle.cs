using DG.Tweening;
using UnityEngine;

public class Boss3_Battle : Enemy
{
    //���Ž� ������Ʈ
    [SerializeField] private GameObject _smashObject;
    //���� ������Ʈ
    [SerializeField] private GameObject _axeObject;

    //��þ����� ��, �ִϸ��̼� �ð�
    private float _dashAttackTime = 0.4f - 0.05f;

    //������ �������� Ȯ�ο� ����
    public bool isJumpEnd { get; private set; } = false;


    protected override void Awake()
    {
        base.Awake();

        //ĳ���� �Ŵ��� ����
        CharacterManager.instance.enemy = this;
    }

    public void SpawnSmashObject()
    {
        float _rl = 1.0f;                       //��: -1, ��: 1
        float _x = 1.3f;                        //x��ǥ ����
        float _y = 1.3f;                        //y��ǥ ����
        float _dur = 1.0f;                     //������Ʈ ���������� ���ư��� �ð�
        float _range = 15.0f;
        Vector2 _bossPos = transform.position;  //���� ��ġ
        Vector2 _spawnPos;                      //������Ʈ ��ȯ�� ��ġ
        Vector2 _dir;                           //������Ʈ�� ���� ����

        //������ �ٶ󺸰� ������
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

        //��ü ��ȯ
        var _obj = Instantiate(_smashObject, _spawnPos, Quaternion.Euler(0.0f, 0.0f, -90.0f)).GetComponent<Boss1Obstacle>();

        //�׳� ���� ��
        if (Random.Range(0, 2) == 0)
        {
            _obj.MoveInQuadObject(_dir, _dur);
            _obj.transform.DORotate(new Vector3(0.0f, 0.0f, 90.0f * _rl), 0.01f, RotateMode.Fast);
        }
        //���ƿ��� ������ ���� ��
        else
        {
            _obj.MoveYoYoObject(_dir, _dur);
            _obj.transform.DORotate(new Vector3(0.0f, 0.0f, 720.0f), _dur, RotateMode.FastBeyond360);
        }

        //����� ����
        _obj.damage = enemyStatHandler.stat.attackDamage;
    }

    public void JumpToPlayer()
    {
        //�÷��̾� ������Ʈ
        GameObject _player = CharacterManager.instance.basePlayer.gameObject;

        //���� ����
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
