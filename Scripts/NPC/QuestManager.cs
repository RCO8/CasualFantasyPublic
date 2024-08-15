using UnityEngine;
using TMPro;
using System.IO;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public GameObject questUI; // ��ü ����Ʈ UI
    public TextMeshProUGUI questTitleText; // ����Ʈ ���� �ؽ�Ʈ
    public TextMeshProUGUI questDescriptionText; // ����Ʈ ���� �ؽ�Ʈ
    public GameObject questEntryPrefab; // ����Ʈ �׸� ������
    public Transform questListContent; // ����Ʈ ��� ������ ����
    public BaseQuest[] quests; // ��� ����Ʈ�� ���� �迭

    private string saveFilePath;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� �����ǵ��� ����
            saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        foreach (BaseQuest quest in quests)
        {
            AddQuestEntry(quest);
        }
    }

    public void UpdateQuestDescription(string description)
    {
        questDescriptionText.text = description;
    }

    public void UpdateQuestTitle(string title)
    {
        questTitleText.text = title;
    }

    public void ToggleQuestUI(bool show)
    {
        questUI.SetActive(show);
    }

    public void AddQuestEntry(BaseQuest quest)
    {
        // ����Ʈ �׸� ������ �ν��Ͻ� ����
        GameObject questEntryObject = Instantiate(questEntryPrefab, questListContent);
        QuestEntry questEntry = questEntryObject.GetComponent<QuestEntry>();

        // ����Ʈ �׸� ������ ����
        questEntry.SetData(quest);
    }

    public void CompleteQuest(BaseQuest quest)
    {
        quest.CompleteQuest();
        // ������Ʈ�� ����Ʈ ���¸� UI�� �ݿ�
        foreach (Transform child in questListContent)
        {
            QuestEntry entry = child.GetComponent<QuestEntry>();
            if (entry.quest == quest)
            {
                entry.UpdateQuestUI();
            }
        }
    }
}
