using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSpawnReset : MonoBehaviour
{
    [SerializeField, Header("���̃I�u�W�F�N�g�ɐG�ꂽ�ۂɃe���|�[�g���������ꏊ������")]
    Transform TeleportPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = TeleportPoint.position;
        }
    }
}
