using UnityEngine;

public class SaveLoadController : MonoBehaviour
{
    public SaveLoadView view;
    private SaveLoadModel model;

    private void Start()
    {
        model = new SaveLoadModel();
        Initialize();
    }

    private void Initialize()
    {
        // Initialize save/load view
    }

    public void SaveGame(int slotIndex, PlayerData data)
    {
        model.SaveData(slotIndex, data);
        view.ShowSaveStatus("Save Complete!");
        // Update slots text
    }

    public void LoadGame(int slotIndex)
    {
        PlayerData data = model.LoadData(slotIndex);
        if (data != null)
        {
            // Update UI with loaded data
            view.ShowLoadStatus($"Name: {data.playerName}\nCompleted Quests: {data.completedQuests}");
        }
    }
}