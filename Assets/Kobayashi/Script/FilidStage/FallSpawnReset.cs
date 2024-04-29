using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSpawnReset : MonoBehaviour
{
    [SerializeField, Header("このオブジェクトに触れた際にテレポートさせたい場所を入れる")]
    Transform TeleportPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = TeleportPoint.position;
        }
    }
}
