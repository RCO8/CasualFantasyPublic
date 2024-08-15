using UnityEngine;
using System;

public class BasePlayer : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    public Animator animator;
    public PlayerStateMachine stateMachine;
    public PlayerAnimationData animationData;
    public SPUM_SpriteList spum_SpriteList;
    public RectTransform rectTransform;

    public event Action OnChangeStat;       //���ȹٲ� ��, UI����� ��������Ʈ
    public event Action OnChangeEquip;      //��� �ٱ� ��, ����� ��������Ʈ

    protected virtual void Awake()
    {
        stateMachine = new PlayerStateMachine(this);
        animationData = new PlayerAnimationData();
        animationData.Initialize();
    }

    protected virtual void Start()
    {
        //�̹� ĳ���Ͱ� �����ϸ� �� ������Ʈ�� �ı��Ѵ�.
        if (CharacterManager.instance.basePlayer != null)
        {
            Debug.Log(CharacterManager.instance.basePlayer.gameObject);
            //Destroy(CharacterManager.instance.basePlayer.gameObject);

        }
        else
        {
            Debug.Log("dont Destroy");
            //CharacterManager.instance.basePlayer = this;
            //CharacterManager.instance.spum_SpriteList = spum_SpriteList;    //temp
            //spum_SpriteList = CharacterManager.instance.spum_SpriteList;
        }
        CharacterManager.instance.basePlayer = this;
        CharacterManager.instance.spum_SpriteList = spum_SpriteList;    //temp
        spum_SpriteList = CharacterManager.instance.spum_SpriteList;
    }

    protected virtual void Update()
    {
        stateMachine.Update();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    public void UpdateStat()
    {
        OnChangeStat?.Invoke();
    }

    public void UpdateEquip()
    {
        OnChangeEquip?.Invoke();
    }
}
