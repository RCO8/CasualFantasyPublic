using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance { get; set; }


    //지금 조종하고 있는 플레이어
    public BasePlayer basePlayer { get; set; }
    //배틀하고 있는 적
    public Enemy enemy { get; set; }
    //플레이어 스탯
    public PlayerStatHandler playerStatHandler { get; set; }
    //플레이어 장비 스프라이트
    public SPUM_SpriteList spum_SpriteList {  get; set; }
    //배틀 플레이어
    public GameObject battlePlayer;
    //필드 플레이어
    public GameObject fieldPlayer;
    //필드에 있는 NPC들
    public Dictionary<int, NPC> npcDic = new Dictionary<int, NPC>();
    //플레이어가 처음 시작할 포지션
    public Vector2 startPlayerPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        playerStatHandler = FindObjectOfType<PlayerStatHandler>();
        //spum_SpriteList 초기화 ( 저장 시스템 생기면 수정 )
        //spum_SpriteList = battlePlayer.GetComponent<Player_Field>().spum_SpriteList;

        battlePlayer = Instantiate(battlePlayer, transform);
        battlePlayer.SetActive(false);
        fieldPlayer = transform.Find("Player_Field").gameObject;

    }

    public void BattleMode()
    {
        battlePlayer.SetActive(true);
        fieldPlayer.SetActive(false);

        battlePlayer.transform.position = new Vector2(-4.5f, -3.9f);

        //UIManager.instance.gameObject.SetActive(false);
    }

    public void FieldMode()
    {
        battlePlayer.SetActive(false);
        fieldPlayer.SetActive(true);

        //UIManager.instance.gameObject.SetActive(true);
    }

    public void NextField(Vector2 _pos) //다음 필드로 넘어갈 때
    {
        fieldPlayer.transform.position = _pos;
    }

    private void LateUpdate()
    {
        if (basePlayer is Player_Field)
        {
            var pos = basePlayer.transform.position;
            Camera.main.transform.position = new Vector3(pos.x, pos.y, Camera.main.transform.position.z);
        }
    }
}
