using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmoWeaponPrefab : WeaponBaseClass
{
    private readonly float DestroyTimer = 5.0f;

    private void OnTriggerStay(Collider other)
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(DestroyTime());
    }

    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(DestroyTimer);
        Destroy(gameObject);
    }

}
