using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    private float _time = 0.3f;

    private void Start()
    {
        StartCoroutine(DestroyDamageText());
    }

    IEnumerator DestroyDamageText()
    {
        yield return new WaitForSeconds(_time);
        Destroy(gameObject);
    }
}
