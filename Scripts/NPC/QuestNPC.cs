using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestNPC : MonoBehaviour
{
    public GameObject questDialogueUI;  // ����Ʈ ��ȭ UI
    public GameObject questCompleteUI;  // ����Ʈ �Ϸ� UI
    public GameObject questRewardUI;    // ����Ʈ ���� UI
    public TextMeshProUGUI questDialogueText;  // ����Ʈ ��ȭ �ؽ�Ʈ
    public TextMeshProUGUI questCompleteText;  // ����Ʈ �Ϸ� �ؽ�Ʈ
    public TextMeshProUGUI questRewardText;    // ����Ʈ ���� �ؽ�Ʈ
    public Button acceptButton;         // ����Ʈ ���� ��ư
    public Button closeButton;          // �ݱ� ��ư
    public BaseQuest quest;             // �� NPC�� �ִ� ����Ʈ

    private bool playerInRange = false;
    private bool questAccepted = false;

    private void Start()
    {
        // UI �ʱ� ���� ����
        questDialogueUI.SetActive(false);
        questCompleteUI.SetActive(false);
        questRewardUI.SetActive(false);

        // ��ư Ŭ�� �̺�Ʈ ���
        acceptButton.onClick.AddListener(AcceptQuest);
        closeButton.onClick.AddListener(CloseQuestUI);
    }

    private void Update()
    {
        // �÷��̾ ���� ���� �ְ�, Z Ű�� ������ �� ��ȣ�ۿ� ó��
        if (playerInRange && Input.GetKeyDown(KeyCode.Z))
        {
            if (!questAccepted)
            {
                ShowQuestDialogue();
            }
            else if (questAccepted && !quest.isCompleted)
            {
                ShowQuestDialogue(); // ����Ʈ ���� �Ŀ��� ��ȭ UI�� ��� ������ ����
            }
            else if (questAccepted && quest.isCompleted)
            {
                ShowQuestComplete(); // ����Ʈ �Ϸ� UI�� ��� ������ ����
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            CloseQuestUI();
        }
    }

    private void ShowQuestDialogue()
    {
        questDialogueUI.SetActive(true); 
        questDialogueText.text = quest.questDescription; 
    }

    private void AcceptQuest()
    {
        questAccepted = true;
        quest.StartQuest();
        questDialogueUI.SetActive(false);
    }

    private void ShowQuestComplete()
    {
        questCompleteUI.SetActive(true); 
        questCompleteText.text = "����Ʈ �Ϸ�!";
    }

    public void HideQuestReward()
    {
        questRewardUI.SetActive(false); 
    }

    public void CloseQuestUI()
    {
        questDialogueUI.SetActive(false); 
        questCompleteUI.SetActive(false); 
        questRewardUI.SetActive(false);
    }
}
