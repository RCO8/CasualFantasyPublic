using DG.Tweening;
using UnityEngine;

public class Boss1Obstacle : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(Define.TAG_PLAYER))
        {
            CharacterManager.instance.basePlayer.GetComponent<Player_Battle>().TakeDamage_KnockBack(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag(Define.TAG_GROUND) || collision.gameObject.CompareTag(Define.TAG_WALL))
        {
            Destroy(gameObject);
        }
    }

    //������ �ӵ�
    public void MoveLinearObject(Vector2 _dir, float _dur)
    {
        transform.DOMove(_dir, _dur).SetEase(Ease.Linear).OnComplete(() => Destroy(gameObject));
    }

    //���� ������
    public void MoveInQuadObject(Vector2 _dir, float _dur)
    {
        transform.DOMove(_dir, _dur).SetEase(Ease.InCirc).OnComplete(() => Destroy(gameObject));
    }

    //���� ������
    public void MoveOutQuadObject(Vector2 _dir, float _dur)
    {
        transform.DOMove(_dir, _dur).SetEase(Ease.OutCirc).OnComplete(() => Destroy(gameObject));
    }

    //�ٽ� ���ƿ�
    public void MoveYoYoObject(Vector2 _dir, float _dur)
    {
        _dir.x -= _dir.x * 0.5f;
        transform.DOMove(_dir, _dur * 2.0f).SetLoops(2, LoopType.Yoyo).OnComplete(() => Destroy(gameObject));
    }
}
