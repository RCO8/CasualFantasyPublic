using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleStatUI : MonoBehaviour
{
    public static BattleStatUI instance;

    private PlayerStatHandler _playerStatHandler;
    private EnemyStatHandler _enemyHPSystem;
    
    public TextMeshProUGUI EnemyName;
    public Slider playerHpBar;
    public Slider playerManaBar;
    public Slider enemyHPBar;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _playerStatHandler = CharacterManager.instance.playerStatHandler;
        _enemyHPSystem = CharacterManager.instance.enemy.enemyStatHandler;

        if (CharacterManager.instance.enemy != null)
        {
            //�̸��� EnemyText��
        }
        //������ �ٲ� ��, ��������Ʈ ����
        StartCoroutine(BindStat());
    }

    public void PlayerUpdateUI()
    {
        playerHpBar.value = _playerStatHandler.hpSystem.currentHealth / _playerStatHandler.hpSystem.maxHealth;
        playerManaBar.value = _playerStatHandler.mpSystem.currentMana / _playerStatHandler.mpSystem.maxMana;
        Debug.Log("playerHpBar: " + _playerStatHandler.hpSystem.currentHealth);
        Debug.Log("playerManaBar: " + _playerStatHandler.mpSystem.currentMana);
    }

    public void EnemyUpdateUI()
    {
        enemyHPBar.value = _enemyHPSystem.currentHealth / _enemyHPSystem.stat.maxHealth;
        Debug.Log("enemyHPBar: " + _enemyHPSystem.currentHealth);
    }

    IEnumerator BindStat()
    {
        yield return new WaitUntil(() =>
        {
            if (CharacterManager.instance.enemy.enemyStatHandler != null && CharacterManager.instance.basePlayer != null)
                return true;
            else return false;
        });
        Debug.Log("true");
        CharacterManager.instance.basePlayer.OnChangeStat += PlayerUpdateUI;
        PlayerUpdateUI();
        EnemyUpdateUI();
    }
}
