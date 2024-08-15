using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public GameObject skillSlotPrefab; // ��ų ���� ������
    public Transform skillSlotContainer; // ��ų ���� �����̳�
    public Transform equippedSkillsPanel; // ������ ��ų �г�
    public TextMeshProUGUI descriptionNameText; // ���� �г� ��ų �̸� �ؽ�Ʈ
    public TextMeshProUGUI descriptionText; // ���� �г� ��ų ���� �ؽ�Ʈ

    public BaseSkill[] allSkills; // ��� ��ų
    private List<GameObject> equippedSkillSlots = new List<GameObject>(); // ������ ���� ����Ʈ
    private BaseSkill selectedSkill; // ���� ���õ� ��ų

    private void Start()
    {
        DisplaySkills(); // ��ų ǥ��
        CreateSkillSlots(3); // �ִ� 3���� ���� ����
    }

    private void DisplaySkills()
    {
        // ��ų ǥ��: ���÷� ��� ��ų ��ư�� �߰��մϴ�.
        foreach (BaseSkill skill in allSkills)
        {
            GameObject skillSlot = Instantiate(skillSlotPrefab, skillSlotContainer);
            Button skillButton = skillSlot.GetComponentInChildren<Button>();
            TextMeshProUGUI skillNameText = skillSlot.GetComponentInChildren<TextMeshProUGUI>();

            skillNameText.text = skill.GetType().Name; // ��ų �̸��� ����մϴ�.

            skillButton.onClick.AddListener(() => OnSkillButtonClick(skill));
        }
    }

    private void CreateSkillSlots(int count)
    {
        // ���� ���� ����
        foreach (var slot in equippedSkillSlots)
        {
            Destroy(slot);
        }
        equippedSkillSlots.Clear();

        // ���ο� ���� ����
        for (int i = 0; i < count; i++)
        {
            GameObject slot = Instantiate(skillSlotPrefab, equippedSkillsPanel);
            slot.SetActive(true); // ������ �׻� Ȱ��ȭ ���·�
            equippedSkillSlots.Add(slot);
        }
    }

    private void OnSkillButtonClick(BaseSkill skill)
    {
        selectedSkill = skill;
        UpdateDescriptionPanel();
    }

    private void UpdateDescriptionPanel()
    {
        if (selectedSkill != null)
        {
            descriptionNameText.text = selectedSkill.GetType().Name;
            descriptionText.text = selectedSkill.GetType().Name; // ���÷� ��ų�� �̸��� �������� ���
        }
    }

    //public void EquipSkill()
    //{
    //    if (selectedSkill != null)
    //    {
    //        foreach (var slot in equippedSkillSlots)
    //        {
    //            Image equippedSkillImage = slot.GetComponentInChildren<Image>();
    //            if (!equippedSkillImage.gameObject.activeSelf) // �̹����� ��Ȱ��ȭ ������ ���� ã��
    //            {
    //                // ��ų ������ ����
    //                Sprite skillIcon = selectedSkill.sprite; // ��ų�� ���������� ����
    //                equippedSkillImage.sprite = skillIcon;
    //                equippedSkillImage.gameObject.SetActive(true); // ��ų �̹��� Ȱ��ȭ
    //                TextMeshProUGUI equippedSkillNameText = slot.GetComponentInChildren<TextMeshProUGUI>();
    //                equippedSkillNameText.text = selectedSkill.GetType().Name;
    //                return; // ���� �� ����
    //            }
    //        }

    //        Debug.LogWarning("No available slots to equip the skill.");
    //    }
    //}
}
