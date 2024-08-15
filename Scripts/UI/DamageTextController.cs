using UnityEngine;
using UnityEngine.UI;

public class DamageTextController : MonoBehaviour
{
    [SerializeField] private GameObject _damageText;
    [SerializeField] private GameObject _canvas;

    public static DamageTextController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreateDamageText(Vector2 _textPos, float _dam)
    {
        //데미지 텍스트 소환
        GameObject _damageTextObject = Instantiate(_damageText, _textPos, Quaternion.identity, _canvas.transform);

        //데미지 텍스트의 텍스트 변경
        _damageTextObject.GetComponent<Text>().text = Mathf.RoundToInt(_dam).ToString();

    }
}
