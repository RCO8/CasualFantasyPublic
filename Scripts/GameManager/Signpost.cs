using System.Net.NetworkInformation;
using UnityEngine;

public class Signpost : MonoBehaviour
{
    public GameObject controlsUI; // Controls UI
    public GameObject SingUI; // ����ǥ UI

    private bool playerInRange = false;
    private RectTransform SingUIRectTransform;
    public float SingUIOffsetY = 50f;

    private void Start()
    {
        controlsUI.SetActive(false); // �ʱ⿡�� UI�� ��Ȱ��ȭ
        SingUIRectTransform = SingUI.GetComponent<RectTransform>();
        UpdateExclamationUIPosition();
    }

    private void Update()
    {
        // �÷��̾ ���� ���� �ְ�, UI�� ��Ȱ��ȭ �Ǿ� ���� ��
        if (playerInRange && !controlsUI.activeSelf)
        {
            ShowControlsUI();

        }
        // �÷��̾ ���� �ۿ� �ְ�, UI�� Ȱ��ȭ �Ǿ� ���� ��
        else if (!playerInRange && controlsUI.activeSelf)
        {
            HideControlsUI();
        }
    }

    private void LateUpdate()
    {
        // ����ǥ UI�� ��ġ�� ������Ʈ
        UpdateExclamationUIPosition();
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
        }
    }

    private void ShowControlsUI()
    {
        controlsUI.SetActive(true); // Controls UI Ȱ��ȭ
        SingUI.SetActive(false);
    }

    private void HideControlsUI()
    {
        controlsUI.SetActive(false); // Controls UI ��Ȱ��ȭ
        SingUI.SetActive(true);
    }

    private void UpdateExclamationUIPosition()
    {
        // ����ǥ UI�� ǥ���� ���� ������Ű��
        Vector3 exclamationWorldPosition = transform.position + new Vector3(0, SingUIOffsetY / 100f, 0); // ������ ����
        Vector3 exclamationScreenPosition = Camera.main.WorldToScreenPoint(exclamationWorldPosition);
        //SingUIRectTransform.position = exclamationScreenPosition;
    }
}
