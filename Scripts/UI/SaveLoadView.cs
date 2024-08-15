using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadView : MonoBehaviour
{
    public GameObject saveStatusImage;
    public TextMeshProUGUI saveStatusText;
    public TextMeshProUGUI loadStatusText;
    public GameObject saveSlotPrefab;
    public GameObject loadSlotPrefab;
    public Transform saveSlotContainer;
    public Transform loadSlotContainer;

    public void UpdateSlotText(int slotIndex, string slotText)
    {
        // Update slot UI based on the slotText
    }

    public void ShowSaveStatus(string message)
    {
        saveStatusText.text = message;
        saveStatusImage.SetActive(true);
    }

    public void ShowLoadStatus(string message)
    {
        loadStatusText.text = message;
    }
}