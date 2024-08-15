using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerStatHandler playerStatHandler;
    public JsonControll saveData;

    [SerializeField] private TextMeshProUGUI checkSaveText;
    private bool checkFile = false;

    // 오브젝트 풀
    public ArrowPool ArrowPool;
    void Start()
    {
        // 싱글톤 인스턴스 설정 및 파괴 방지
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        // CharacterManager의 playerStatHandler를 설정하거나 확인하는 코드
        CharacterManager characterManager = CharacterManager.instance;
        saveData = new JsonControll();

        checkSaveText.gameObject.SetActive(false);
    }

    public void SaveData(int i)
    {
        checkFile = saveData.SaveFile(i);
        StartCoroutine(ShowText("저장하기 ", checkFile));
        UIManager.instance.CloseUI();
    }

    public void LoadData(int i)
    {
        checkFile = saveData.LoadFile(i);
        StartCoroutine(ShowText("불러오기 ", checkFile));
        UIManager.instance.CloseUI();
    }

    IEnumerator ShowText(string type, bool state)
    {
        checkSaveText.gameObject.SetActive(true);
        checkSaveText.text = type;

        if(state)
        {
            checkSaveText.text += "성공";
            checkSaveText.color = Color.green;
        }
        else
        {
            checkSaveText.text += "실패";
            checkSaveText.color = Color.red;
        }

        yield return new WaitForSeconds(1f);
        checkSaveText.gameObject.SetActive(false);
    }
}
