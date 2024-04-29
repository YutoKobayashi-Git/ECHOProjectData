using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRelease : MonoBehaviour
{
    public BoxCollider[] boxCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
        }
    }
}
