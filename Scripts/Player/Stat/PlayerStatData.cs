using System;

//public enum EStat
//{
//    MaxHP,
//    MaxMana,
//    AttackDamage,
//    AttackSpeed,
//    MoveSpeed
//}

//public enum EStatType
//{
//    Add,
//    Mul,
//    Ovr
//}

[Serializable]
public class PlayerStatData
{
    public float maxHealth;
    public float maxMana;
    public float attackDamage;  
    public float attackSpeed;
    public float moveSpeed;
    // �߰��� �ʵ�
    public string playerName; // �÷��̾� �̸�
    public int goldAmount;  // �÷��̾� ���� �ݾ�
}
