using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    public float speed; // 배경이 이동하는 속도
    public RectTransform[] backgrounds; // UI 배경 이미지 배열

    private float imageWidth; // 이미지의 너비
    private float canvasWidth; // Canvas의 너비
    private float resetOffset; // 재배치 오프셋

    public GameObject soundUI;
    public Button closeButton; // 닫기 버튼

    // 사운드 UI 슬라이더들
    public Slider masterVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;

    // 오디오 소스들
    public AudioSource bgmAudioSource;
    public AudioSource[] sfxAudioSources;

    // 초기 볼륨 값들
    private float masterVolume = 0.5f;
    private float bgmVolume = 0.5f;
    private float sfxVolume = 0.5f;

    void Start()
    {
        // 이미지의 너비를 계산합니다.
        if (backgrounds.Length > 0)
        {
            imageWidth = backgrounds[0].rect.width;
        }

        // Canvas의 너비를 계산합니다.
        canvasWidth = GetComponent<RectTransform>().rect.width;

        // 이미지가 화면을 벗어날 때 리셋되는 오프셋을 설정합니다.
        resetOffset = imageWidth;

        // 배경 이미지들을 초기 위치로 설정합니다.
        for (int i = 0; i < backgrounds.Length; i++)
        {
            Vector2 startPosition = new Vector2(i * imageWidth, backgrounds[i].anchoredPosition.y);
            backgrounds[i].anchoredPosition = startPosition;
        }

        // 슬라이더 초기 값 설정
        masterVolumeSlider.value = masterVolume;
        bgmVolumeSlider.value = bgmVolume;
        sfxVolumeSlider.value = sfxVolume;

        // 슬라이더 이벤트 핸들러 설정
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmVolumeSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);

        // 배경음악 설정 및 재생
        if (bgmAudioSource != null)
        {
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
        }
    }

    void Update()
    {
        // 각 배경을 오른쪽으로 이동시킵니다.
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].anchoredPosition += new Vector2(speed, 0) * Time.deltaTime;

            // 배경이 화면의 오른쪽을 벗어났을 때
            if (backgrounds[i].anchoredPosition.x > canvasWidth)
            {
                // 배경 이미지를 화면 왼쪽으로 이동시킵니다.
                Vector2 newPos = backgrounds[i].anchoredPosition;
                newPos.x -= resetOffset * backgrounds.Length;
                backgrounds[i].anchoredPosition = newPos;
            }
        }

        // 배경이 화면을 벗어나지 않도록 추가적인 위치 조정
        AdjustBackgroundsPosition();
    }

    private void AdjustBackgroundsPosition()
    {
        foreach (RectTransform background in backgrounds)
        {
            // 배경이 화면 왼쪽으로 이동해야 할 경우
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

    // 마스터 볼륨 설정
    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        ApplyVolumes();
    }

    // 배경음악 볼륨 설정
    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        ApplyVolumes();
    }

    // 효과음 볼륨 설정
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        ApplyVolumes();
    }

    // 볼륨 적용 함수
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
