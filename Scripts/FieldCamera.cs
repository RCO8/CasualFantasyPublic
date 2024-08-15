using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCamera : MonoBehaviour
{
    private Transform playerPosition;
    private Vector3 camPos = Vector3.zero;

    private void Start()
    {
        playerPosition = CharacterManager.instance.fieldPlayer.transform;
        camPos.z -= 10;
    }

    private void LateUpdate()
    {
        if(CharacterManager.instance.fieldPlayer != null)
        {
            transform.position = playerPosition.position + camPos;
        }
    }
}
