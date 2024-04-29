using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTransparent : MonoBehaviour
{
    [SerializeField, Header("このオブジェクトに触れた際に透過させたい壁を入れる")]
    GameObject TransparentObject;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BackWallTransparent();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ReturnBackWallTransparent();
        }
    }

    /// <summary>
    /// 壁を透過する処理
    /// </summary>
    void BackWallTransparent()
    {
        TransparentObject.SetActive(false);
    }

    /// <summary>
    /// 壁を透過を戻す処理
    /// </summary>
    void ReturnBackWallTransparent()
    {
        TransparentObject.SetActive(true);
    }
}
