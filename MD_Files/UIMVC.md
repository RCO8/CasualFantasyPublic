# UI 사용 예 
![image_UI_View](https://github.com/user-attachments/assets/36e3db63-cb70-4025-a0da-590ff9363a3c)



⚠️트러블슈팅
* UIManager에 UI 오브젝트들이 할당되어 씬 이동 시에도 유지가 되고 기능이 작동 되어야 하는데 할당이 되지 않아 Find 함수를 써서 할당은 되었으나 UI요소를 찾지 못해 기능이 안되는 오류가 발생

❗해결방법
* 여러차례 할당을 시켜도 찾을 수 없다는 오류가 떠 오브젝트 위치를 바꾸다 UIManager에 자식으로 UI오브젝트를 넣고 Find 위치,UI 이름, 전체적인 코드 등을 다시 확인하고 수정, 실행하여 문제를 해결
  
![image_Inspector](https://github.com/user-attachments/assets/38f679e5-8659-4a20-9fe3-51e28e47310d)

#코드 샘플
  ```cs
  
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
                /*
                Debug.Log("UserInfoUI found: " + (userInfoUI != null));
                Debug.Log("InventoryUI found: " + (inventoryUI != null));
                Debug.Log("SkillUI found: " + (skillUI != null));
                Debug.Log("QuestUI found: " + (questUI != null));
                Debug.Log("SaveUI found: " + (saveUI != null));
                Debug.Log("OptionUI found: " + (optionUI != null));
                */

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
                        }
                    }
                }
            }
        }
    }
  ```
