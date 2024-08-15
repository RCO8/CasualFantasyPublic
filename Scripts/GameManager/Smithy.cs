using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Smithy : MonoBehaviour
{

    public Button enhanceButton; // ��ȭ ��ư

    public GameObject enhancementUI; // ��ȭ UI
    public GameObject enhancementResultUI; // ��ȭ ��� UI
    public Slider enhancementProgressSlider; // ��ȭ ���� ������
    public TextMeshProUGUI enhancementResultText; // ��ȭ ��� �ؽ�Ʈ

    private int currentEnhancementLevel = 0; // ���� ��ȭ ����
    private int baseSuccessChance = 100; // ó�� ��ȭ ���� Ȯ��
    private int[] successChances = { 70, 60, 50, 40, 30, 20, 10, 10, 5 }; // ��ȭ ���� Ȯ�� �迭
    private int enhancementCost = 10; // ��ȭ ���
    private int enhancementValue = 10; // ��ȭ ��ġ

    private void Start()
    {
        enhanceButton.onClick.AddListener(StartEnhancementProcess);
        enhancementProgressSlider.gameObject.SetActive(false); // �ʱ⿡�� ������ ����
        enhancementResultUI.SetActive(false); // �ʱ⿡�� ��ȭ ��� UI ����
    }

    public void StartEnhancementProcess()
    {
        enhancementProgressSlider.value = 0; // ��ȭ ���� ������ �ʱ�ȭ
        enhancementUI.SetActive(true);
        enhancementProgressSlider.gameObject.SetActive(true);
        StartCoroutine(EnhancementProgress());
    }

    public IEnumerator EnhancementProgress()
    {
        float duration = 3.0f; // �������� ���� �ð�
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            enhancementProgressSlider.value = elapsed / duration;
            yield return null;
        }

        enhancementProgressSlider.gameObject.SetActive(false);
        EnhanceItem();
    }

    private void EnhanceItem()
    {
        int roll = Random.Range(1, 101); // 1���� 100 ������ ���� ����
        bool success = roll <= GetSuccessChance();

        if (success)
        {
            currentEnhancementLevel++;
            enhancementValue += currentEnhancementLevel == 10 ? 20 : 10; // 10���� 20% ����
            // ��ȭ ��� ����
            if (currentEnhancementLevel == 10)
            {
                enhancementCost += 20; // 9������ 10�� ���� ��� ��� 20 ����
            }
            else
            {
                enhancementCost += 10; // �������� ��� 10 ����
            }
            enhancementResultText.text = "��ȭ ����!!";
        }
        else
        {
            enhancementResultText.text = "������ ���� �̲�������";
        }
        ShowEnhancementResult();
    }

    private void ShowEnhancementResult()
    {
        enhancementResultUI.SetActive(true);
        StartCoroutine(HideEnhancementResultAfterDelay());
    }

    private IEnumerator HideEnhancementResultAfterDelay()
    {
        yield return new WaitForSeconds(3.0f); // 3�� ���
        enhancementResultUI.SetActive(false); // ��ȭ ��� UI �����
        enhancementUI.SetActive(true); // ��ȭ UI ���̱�
    }

    public void CloseEnhancementResult()
    {
        enhancementResultUI.SetActive(false);
        enhancementUI.SetActive(false);
    }

    private int GetSuccessChance()
    {
        if (currentEnhancementLevel < successChances.Length)
        {
            return baseSuccessChance - successChances[currentEnhancementLevel];
        }
        else
        {
            return 5; // 10��° ��ȭ �õ� �Ŀ��� �ּ� 5% ���� Ȯ�� ����
        }
    }
}
