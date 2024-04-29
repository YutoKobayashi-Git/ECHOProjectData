using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGauge : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ScoreManager.Instance.specialGauge = 10;
        }
    }
}
