using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    // UI 오브젝트
    public GameObject uiContainer; // UI를 포함하는 오브젝트 (Canvas 포함)
    private GameObject userInfoUI;
    private GameObject inventoryUI;
    private GameObject skillUI;
    private GameObject questUI;
    private GameObject saveUI;
    private GameObject optionUI;

    // Sound 관련
    public Slider masterVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;

    public AudioSource bgmAudioSource;
    public AudioSource[] sfxAudioSources;

    private float masterVolume = 0.5f;
    private float bgmVolume = 0.5f;
    private float sfxVolume = 0.5f;

    // Buttons
    private Button userInfoButton;
    private Button inventoryButton;
    private Button skillButton;
    private Button questButton;
    private Button saveButton;
    private Button optionButton;
    private Button exitButton;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (uiContainer != null)
            {
                AssignUIElements();
            }
            else
            {
                Debug.LogError("UI Container is not assigned! Please assign the UI Container in the inspector.");
            }
        }
        else
        {
            Destroy(gameObject); // 기존 인스턴스가 있을 경우 이 오브젝트 삭제
        }
    }

    private void AssignUIElements()
    {
        if (uiContainer != null)
        {
            Transform canvasTransform = uiContainer.transform.Find("Canvas");
            if (canvasTransform != null)
            {
                userInfoUI = canvasTransform.Find("UserInfoUI")?.gameObject;
                inventoryUI = canvasTransform.Find("InventoryUI")?.gameObject;
                skillUI = canvasTransform.Find("SkillUI")?.gameObject;
                questUI = canvasTransform.Find("QuestUI")?.gameObject;
                saveUI = canvasTransform.Find("SaveUI")?.gameObject;
                optionUI = canvasTransform.Find("OptionUI")?.gameObject;

                // Debug 로그로 UI 요소가 제대로 할당되었는지 확인
                //Debug.Log("UserInfoUI found: " + (userInfoUI != null));
                //Debug.Log("InventoryUI found: " + (inventoryUI != null));
                //Debug.Log("SkillUI found: " + (skillUI != null));
                //Debug.Log("QuestUI found: " + (questUI != null));
                //Debug.Log("SaveUI found: " + (saveUI != null));
                //Debug.Log("OptionUI found: " + (optionUI != null));

                // Sound 관련 설정
                if (optionUI != null)
                {
                    Transform soundTransform = optionUI.transform.Find("Sound");
                    if (soundTransform != null)
                    {
                        Transform soundPanelTransform = soundTransform.Find("SoundPanel");
                        if (soundPanelTransform != null)
                        {
                            Transform masterUITransform = soundPanelTransform.Find("MasterUI");
                            Transform bgmUITransform = soundPanelTransform.Find("BGMUI");
                            Transform sfxUITransform = soundPanelTransform.Find("SFXUI");

                            if (masterUITransform != null)
                            {
                                masterVolumeSlider = masterUITransform.Find("MasterSlider")?.GetComponent<Slider>();
                            }
                            else
                            {
                                Debug.LogError("MasterUI could not be found in the SoundPanel object!");
                            }

                            if (bgmUITransform != null)
                            {
                                bgmVolumeSlider = bgmUITransform.Find("BGMSlider")?.GetComponent<Slider>();
                            }
                            else
                            {
                                Debug.LogError("BGMUI could not be found in the SoundPanel object!");
                            }

                            if (sfxUITransform != null)
                            {
                                sfxVolumeSlider = sfxUITransform.Find("SFXSlider")?.GetComponent<Slider>();
                            }
                            else
                            {
                                Debug.LogError("SFXUI could not be found in the SoundPanel object!");
                            }
                        }
                        else
                        {
                            Debug.LogError("SoundPanel could not be found in the Sound object!");
                        }
                    }
                    else
                    {
                        Debug.LogError("Sound object could not be found in the OptionUI!");
                    }
                }
                else
                {
                    Debug.LogError("OptionUI could not be found in the Canvas object!");
                }
            }
            else
            {
                Debug.LogError("Canvas could not be found in the UI Container!");
            }
        }
        else
        {
            Debug.LogError("UI Container is null!");
        }
    }

    private void Start()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        if (userInfoUI == null || inventoryUI == null || skillUI == null || questUI == null || saveUI == null || optionUI == null)
        {
            Debug.LogError("UI elements are not assigned properly!");
            return;
        }

        HideAllUI();

        if (masterVolumeSlider != null) masterVolumeSlider.value = masterVolume;
        if (bgmVolumeSlider != null) bgmVolumeSlider.value = bgmVolume;
        if (sfxVolumeSlider != null) sfxVolumeSlider.value = sfxVolume;

        if (masterVolumeSlider != null) masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        if (bgmVolumeSlider != null) bgmVolumeSlider.onValueChanged.AddListener(SetBGMVolume);
        if (sfxVolumeSlider != null) sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);

        if (bgmAudioSource != null)
        {
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
        }

        ApplyVolumes();
    }

    public void ShowUserInfoUI()
    {
        Debug.Log("ShowUserInfoUI called");
        SetActiveUI(userInfoUI);
    }

    public void ShowInventoryUI()
    {
        SetActiveUI(inventoryUI);
    }

    public void ShowSkillUI()
    {
        SetActiveUI(skillUI);
    }

    public void ShowQuestUI()
    {
        SetActiveUI(questUI);
    }

    public void ShowSaveUI()
    {
        SetActiveUI(saveUI);
    }

    public void ShowOptionUI()
    {
        SetActiveUI(optionUI);
    }

    private void SetActiveUI(GameObject ui)
    {
        if (ui != null)
        {
            HideAllUI();
            ui.SetActive(true);
        }
        else
        {
            Debug.LogError("UI is null!");
        }
    }

    public void HideAllUI()
    {
        if (userInfoUI != null) userInfoUI.SetActive(false);
        if (inventoryUI != null) inventoryUI.SetActive(false);
        if (skillUI != null) skillUI.SetActive(false);
        if (questUI != null) questUI.SetActive(false);
        if (saveUI != null) saveUI.SetActive(false);
        if (optionUI != null) optionUI.SetActive(false);
    }

    public void CloseUI()
    {
        HideAllUI();
        //Debug.Log("All UI closed.");
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        ApplyVolumes();
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        ApplyVolumes();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        ApplyVolumes();
    }

    private void ApplyVolumes()
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.volume = bgmVolume;
        }

        if (sfxAudioSources != null)
        {
            foreach (AudioSource sfxSource in sfxAudioSources)
            {
                sfxSource.volume = sfxVolume;
            }
        }
    }
}
