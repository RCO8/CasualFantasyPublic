using UnityEngine;

public class SmithyNPC : MonoBehaviour
{
    public GameObject smithyUI; // ���尣 UI

    private bool playerInRange = false;

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
            HideSmithyUI(); // �������� ������ UI �����
        }
    }

    private void Update()
    {
        // �÷��̾ ���� ���� �ְ�, Z Ű�� ������ �� ��ȣ�ۿ� ó��
        if (playerInRange && Input.GetKeyDown(KeyCode.Z))
        {
            InteractWithNPC();
        }
    }

    public void ShowSmithyUI()
    {
        smithyUI.SetActive(true); // ���尣 UI Ȱ��ȭ
    }

    public void HideSmithyUI()
    {
        smithyUI.SetActive(false); // ���尣 UI ��Ȱ��ȭ
    }

    private void InteractWithNPC()
    {
        // ��ȣ�ۿ� ó�� ���� �߰�
        Debug.Log("Interacting with NPC");

        // UI Ȱ��ȭ
        ShowSmithyUI();
    }
}
