using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public InventoryView view;
    private InventoryModel model;

    private void Start()
    {
        model = new InventoryModel();
        Initialize();
    }

    private void Initialize()
    {
        // Initialize inventory and setup view
        view.UpdateUI(model);
    }

    public void AddItem(ItemSO item)
    {
        model.AddItem(item);
        view.UpdateUI(model);
    }

    public void RemoveItem(ItemSO item)
    {
        model.RemoveItem(item);
        view.UpdateUI(model);
    }

    public void SelectItem(int index)
    {
        if (index >= 0 && index < model.Items.Count)
        {
            model.SelectedItem = model.Items[index];
            view.DisplayItemInfo(model.SelectedItem);
        }
    }

    public void UseItem()
    {
        if (model.SelectedItem != null)
        {
            // Use item logic
            RemoveItem(model.SelectedItem);
        }
    }
}
