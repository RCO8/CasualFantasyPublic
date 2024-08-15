using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Player_Field : BasePlayer
{
    public PlayerController_Field controller;
    public PlayerMovement_Field playerMovement;

    public bool isTrigger { get; private set; }
    public GameObject npc { get; private set; }


    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Define.TAG_ENEMY))//���� �ƴ� npc�� �����ϰ� ���� ����
        {
            isTrigger = true;
            npc = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(Define.TAG_ENEMY))//���� �ƴ� npc�� �����ϰ� ���� ����
        {
            isTrigger = false;
            npc = null;
        }
    }

    //������ ������ �Ű������� �־������
    public void ChangeEquip(int _equipNum)
    {
        //spum_SpriteList._itemList[_equipNum] = 

        //���� �ÿ��� 
    }
}
