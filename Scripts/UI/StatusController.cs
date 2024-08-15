using UnityEngine;

public class StatusUIController : MonoBehaviour
{
    public StatusView view;
    private StatusModel model;

    private void Start()
    {
        if (CharacterManager.instance == null)
        {
            Debug.LogError("CharacterManager.instance is null!");
            return;
        }

        var playerStatHandler = CharacterManager.instance.playerStatHandler;

        if (playerStatHandler == null)
        {
            Debug.LogError("playerStatHandler is null!");
            return;
        }

        model = new StatusModel(
            playerStatHandler.currentStats,
            playerStatHandler.hpSystem,
            playerStatHandler.mpSystem
        );

        RefreshUI();
        CharacterManager.instance.basePlayer.OnChangeStat += RefreshUI;
    }

    private void OnDestroy()
    {
        if (CharacterManager.instance != null)
        {
            CharacterManager.instance.basePlayer.OnChangeStat -= RefreshUI;
        }
    }

    private void RefreshUI()
    {
        if (model != null)
        {
            view.UpdateUI(model);
        }
    }
}
