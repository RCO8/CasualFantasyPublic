using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    public float speed; // ����� �̵��ϴ� �ӵ�
    public RectTransform[] backgrounds; // UI ��� �̹��� �迭

    private float imageWidth; // �̹����� �ʺ�
    private float canvasWidth; // Canvas�� �ʺ�
    private float resetOffset; // ���ġ ������

    public GameObject soundUI;
    public Button closeButton; // �ݱ� ��ư

    // ���� UI �����̴���
    public Slider masterVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;

    // ����� �ҽ���
    public AudioSource bgmAudioSource;
    public AudioSource[] sfxAudioSources;

    // �ʱ� ���� ����
    private float masterVolume = 0.5f;
    private float bgmVolume = 0.5f;
    private float sfxVolume = 0.5f;

    void Start()
    {
        // �̹����� �ʺ� ����մϴ�.
        if (backgrounds.Length > 0)
        {
            imageWidth = backgrounds[0].rect.width;
        }

        // Canvas�� �ʺ� ����մϴ�.
        canvasWidth = GetComponent<RectTransform>().rect.width;

        // �̹����� ȭ���� ��� �� ���µǴ� �������� �����մϴ�.
        resetOffset = imageWidth;

        // ��� �̹������� �ʱ� ��ġ�� �����մϴ�.
        for (int i = 0; i < backgrounds.Length; i++)
        {
            Vector2 startPosition = new Vector2(i * imageWidth, backgrounds[i].anchoredPosition.y);
            backgrounds[i].anchoredPosition = startPosition;
        }

        // �����̴� �ʱ� �� ����
        masterVolumeSlider.value = masterVolume;
        bgmVolumeSlider.value = bgmVolume;
        sfxVolumeSlider.value = sfxVolume;

        // �����̴� �̺�Ʈ �ڵ鷯 ����
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmVolumeSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);

        // ������� ���� �� ���
        if (bgmAudioSource != null)
        {
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
        }
    }

    void Update()
    {
        // �� ����� ���������� �̵���ŵ�ϴ�.
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].anchoredPosition += new Vector2(speed, 0) * Time.deltaTime;

            // ����� ȭ���� �������� ����� ��
            if (backgrounds[i].anchoredPosition.x > canvasWidth)
            {
                // ��� �̹����� ȭ�� �������� �̵���ŵ�ϴ�.
                Vector2 newPos = backgrounds[i].anchoredPosition;
                newPos.x -= resetOffset * backgrounds.Length;
                backgrounds[i].anchoredPosition = newPos;
            }
        }

        // ����� ȭ���� ����� �ʵ��� �߰����� ��ġ ����
        AdjustBackgroundsPosition();
    }

    private void AdjustBackgroundsPosition()
    {
        foreach (RectTransform background in backgrounds)
        {
            // ����� ȭ�� �������� �̵��ؾ� �� ���
            if (background.anchoredPosition.x < -imageWidth)
            {
                Vector2 newPos = background.anchoredPosition;
                newPos.x += resetOffset * backgrounds.Length;
                background.anchoredPosition = newPos;
            }
        }
    }

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Town1");
    }

    public void ShowSoundUI()
    {
        soundUI.SetActive(true);
    }

    public void CloseAllUI()
    {
        soundUI.SetActive(false);
    }

    // ������ ���� ����
    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        ApplyVolumes();
    }

    // ������� ���� ����
    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        ApplyVolumes();
    }

    // ȿ���� ���� ����
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        ApplyVolumes();
    }

    // ���� ���� �Լ�
    private void ApplyVolumes()
    {
        AudioListener.volume = masterVolume;

        if (bgmAudioSource != null)
        {
            bgmAudioSource.volume = bgmVolume * masterVolume;
        }

        foreach (AudioSource sfxAudioSource in sfxAudioSources)
        {
            if (sfxAudioSource != null)
            {
                sfxAudioSource.volume = sfxVolume * masterVolume;
            }
        }
    }
}
