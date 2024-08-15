using System.Collections.Generic;
using UnityEngine;

public class InventoryModel
{
    public List<ItemSO> Items { get; private set; }
    public ItemSO SelectedItem { get; set; }

    public InventoryModel()
    {
        Items = new List<ItemSO>();
    }

    public void AddItem(ItemSO item)
    {
        // Add item logic
        Items.Add(item);
    }

    public void RemoveItem(ItemSO item)
    {
        // Remove item logic
        Items.Remove(item);
    }
}

