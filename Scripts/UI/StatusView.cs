using TMPro;
using UnityEngine;

public class StatusView : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI ManaText;
    public TextMeshProUGUI AtkDmgText;
    public TextMeshProUGUI AtkSpdText;
    public TextMeshProUGUI GoldText;

    public void UpdateUI(StatusModel model)
    {
        if (model == null || model.Stats == null || model.HPSystem == null || model.MPSystem == null)
        {
            return;
        }

        NameText.text = model.Stats.playerName ?? "นฺรแน่";
        HPText.text = $"{model.HPSystem.currentHealth}/{model.Stats.maxHealth}";
        ManaText.text = $"{model.MPSystem.currentMana}/{model.Stats.maxMana}";
        AtkDmgText.text = $"{model.Stats.attackDamage}";
        AtkSpdText.text = $"{model.Stats.attackSpeed}";
        GoldText.text = $"{model.Stats.goldAmount}";
    }
}
