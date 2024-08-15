using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // TwoQuestNPC�� ã�Ƽ�
            TwoQuestNPC questNPC = FindObjectOfType<TwoQuestNPC>();

            // TwoQuestNPC�� �����ϰ�, ����Ʈ�� ���۵Ǿ����� �Ϸ���� ���� �������� Ȯ��
            if (questNPC != null && questNPC.quest is BaseQuest baseQuest && baseQuest.isStarted && !baseQuest.isCompleted)
            {
                gameObject.SetActive(true);
                // ������ ���� ó��
                questNPC.CollectCoin(gameObject);
                // ���� ������Ʈ ����
                Destroy(gameObject);
            }
        }
    }
}
