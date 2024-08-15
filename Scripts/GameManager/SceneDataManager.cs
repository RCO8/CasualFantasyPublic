using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

enum EField
{
    Battle,
    Return,
    Potal
}

public class SceneDataManager : MonoBehaviour
{
    private static SceneDataManager _instance;

    public static SceneDataManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("SceneDataManager").AddComponent<SceneDataManager>();
            }
            return _instance;
        }
    }

    private string _beforeFieldScene;
    private Vector2 _position;
    private int _id;
    private EField eField;

    [SerializeField] private Image _image;
    [SerializeField] private Text _percentText;
    [SerializeField] private Slider _progressSlider;    //슬라이더 로딩

    private BasePlayer _player;
    private GameObject _enemyObj;

    public string SceneName { get; private set; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance == this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        //DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += LoadedScreen; // 이벤트에 추가
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= LoadedScreen; // 이벤트에서 제거
    }

    private void SaveSceneName()
    {
        _beforeFieldScene = SceneManager.GetActiveScene().name;
        SceneName = _beforeFieldScene;
    }

    private void SpawnObjectBattle()
    {
        if (NPCDataManager.instance.npcDataDictionary.ContainsKey(_id))
        {
            GameObject _enemy = NPCDataManager.instance.npcDataDictionary[_id].prefab;
            _enemyObj = Instantiate(_enemy, transform);
            _enemyObj.gameObject.transform.position = new Vector2(4.5f, -3.9f);
            //_enemyObj.gameObject.GetComponent<Enemy>().enemyStatSO.id = _id;
        }
    }

    public void EnterBattleScene(int _npcID)
    {
        eField = EField.Battle;
        _id = _npcID;
        SaveSceneName();

        FadeScreen(Define.SCENE_BATTLE);
    }

    public void BackFieldScene()
    {
        eField = EField.Return;
        if (NPCDataManager.instance.npcDataDictionary.ContainsKey(_id))
        {
            NPCDataManager.instance.npcDataDictionary[_id].isClear = true;
            Destroy(_enemyObj);
        }
        FadeScreen(_beforeFieldScene);
    }

    public void NextFieldScene(string _scene, Vector2 _pos)
    {
        //Todo : _scene라는 씬 이름을 받아서 다음 씬으로 넘어갈 때 어디 위치에서 시작할건지
        eField = EField.Potal;
        _position = _pos;
        FadeScreen(_scene);
    }

    private void FadeScreen(string _scene)
    {
        _image.gameObject.SetActive(true);

        _image.DOFade(1, 0.5f).OnComplete(
            () => {
                StartCoroutine("LoadScene", _scene);
            });
    }

    private void LoadedScreen(Scene _scene, LoadSceneMode _mode)
    {
        switch (eField)
        {
            case EField.Battle:
                CharacterManager.instance.BattleMode();
                SpawnObjectBattle();
                break;
            case EField.Return:
                CharacterManager.instance.FieldMode();
                break;
            case EField.Potal:
                CharacterManager.instance.FieldMode();
                CharacterManager.instance.NextField(_position);
                break;
        }

        _image.DOFade(0, 0.5f).OnStart(
            () =>
            {
                _percentText.gameObject.SetActive(false);
                _progressSlider.gameObject.SetActive(false);
            })
        .OnComplete(
            () =>
            {
                _image.gameObject.SetActive(false);
            });
    }

    IEnumerator LoadScene(string _scene)
    {
        _percentText.gameObject.SetActive(true);
        _progressSlider.gameObject.SetActive(true);

        AsyncOperation async = SceneManager.LoadSceneAsync(_scene);
        async.allowSceneActivation = false;

        float _time = 0.0f;
        float _per = 0.0f;

        while (!(async.isDone))
        {
            yield return null;
            _time += Time.deltaTime;

            if(_per >= 90.0f)
            {
                _per = Mathf.Lerp(_per, 100.0f, _time);

                if (_per >= 100.0f)
                {
                    async.allowSceneActivation = true;
                }
            }
            else
            {
                _per = Mathf.Lerp(_per, async.progress * 100f, _time);
                if (_per >= 90.0f) _time = 0.0f;
            }

            _progressSlider.value = _per * 0.01f;
            _percentText.text = _per.ToString("0") + "%"; //로딩 퍼센트 표기
        }
    }
}
