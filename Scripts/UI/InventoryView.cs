using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    public ItemButton[] Items;
    public GameObject InventoryWindow;
    public Transform ItemList;

    [Header("SelectedItem")]
    public GameObject ItemInfoPanel;
    public Image selectedItemImage;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;

    [Header("Button Manager")]
    public GameObject UseButton;
    public GameObject EquipButton;
    public GameObject UnequipButton;
    public GameObject RemoveButton;

    public void UpdateUI(InventoryModel model)
    {
        // Update UI based on the InventoryModel
    }

    public void ClearSelectedText()
    {
        selectedItemImage.sprite = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        UseButton.SetActive(false);
        EquipButton.SetActive(false);
        UnequipButton.SetActive(false);
    }

    public void DisplayItemInfo(ItemSO item)
    {
        selectedItemImage.sprite = item.icon;
        selectedItemImage.gameObject.SetActive(true);
        selectedItemName.text = item.itemName;
        selectedItemDescription.text = item.description;
    }
}