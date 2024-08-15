using UnityEngine;

[CreateAssetMenu(fileName = "CombatQuest", menuName = "Quest/CombatQuest")]
public class CombatQuest : BaseQuest
{
    public int enemiesRequired; // ��ǥ �� ��
    public int enemiesDefeated; // ���� óġ�� �� ��

    public override void StartQuest()
    {
        isStarted = true;
        isCompleted = false;
        enemiesDefeated = 0; // ����Ʈ ���� �� �� óġ �� �ʱ�ȭ
    }

    public override void CompleteQuest()
    {
        if (enemiesDefeated >= enemiesRequired)
        {
            isCompleted = true;
        }
    }
}
