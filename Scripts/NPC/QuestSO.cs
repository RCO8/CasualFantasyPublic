using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest/Quest")]
public class Quest : ScriptableObject
{
    public string questTitle;
    public string questDescription;
    public int coinsRequired; // ��ǥ ���� (���� �Ǵ� ��)
    public bool isCompleted; // ����Ʈ �Ϸ� ����
    public string questStartDescription; // ����Ʈ ���� ��ȭ ����
    public string questCompletionDescription; // ����Ʈ �Ϸ� ��ȭ ����

    // ����Ʈ�� �Ϸ�� ����
    public void CompleteQuest()
    {
        isCompleted = true;
    }

    // ����Ʈ�� �����ϰų� ����
    public void StartQuest()
    {
        isCompleted = false;
    }
}
