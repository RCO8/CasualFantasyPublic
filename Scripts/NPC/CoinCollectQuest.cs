using UnityEngine;

[CreateAssetMenu(fileName = "CoinCollectQuest", menuName = "Quest/CoinCollectQuest")]
public class CoinCollectQuest : BaseQuest
{
    public int coinsRequired; // ��ǥ ���� (���� ��)
    public int coinsCollected; // ���� ������ ���� ��

    public override void StartQuest()
    {
        isStarted = true;
        isCompleted = false;
        coinsCollected = 0; // ����Ʈ ���� �� ���� ���� �� �ʱ�ȭ
    }

    public override void CompleteQuest()
    {
        if (coinsCollected >= coinsRequired)
        {
            isCompleted = true;
        }
    }
}


