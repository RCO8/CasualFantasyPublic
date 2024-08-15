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
        //������ �ؽ�Ʈ ��ȯ
        GameObject _damageTextObject = Instantiate(_damageText, _textPos, Quaternion.identity, _canvas.transform);

        //������ �ؽ�Ʈ�� �ؽ�Ʈ ����
        _damageTextObject.GetComponent<Text>().text = Mathf.RoundToInt(_dam).ToString();

    }
}
