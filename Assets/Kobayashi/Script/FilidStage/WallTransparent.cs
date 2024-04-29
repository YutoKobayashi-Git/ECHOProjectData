using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTransparent : MonoBehaviour
{
    [SerializeField, Header("���̃I�u�W�F�N�g�ɐG�ꂽ�ۂɓ��߂��������ǂ�����")]
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
    /// �ǂ𓧉߂��鏈��
    /// </summary>
    void BackWallTransparent()
    {
        TransparentObject.SetActive(false);
    }

    /// <summary>
    /// �ǂ𓧉߂�߂�����
    /// </summary>
    void ReturnBackWallTransparent()
    {
        TransparentObject.SetActive(true);
    }
}
