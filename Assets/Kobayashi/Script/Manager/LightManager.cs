using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField, Header("���ׂẴX�e�[�W���C�g")] 
    public GameObject blinking;

    [SerializeField, Header("�ēx���C�g������܂ł̎���")]
    private float OnLightTime = 11.0f;

    /// <summary>
    /// ���C�g������
    /// </summary>
    public void StartBlinking()
    {
        Blinking();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Blinking()
    {
        blinking.SetActive(false);
        StartCoroutine(_OnLight());
    }

    /// <summary>
    /// ���C�g�ēx����
    /// </summary>
    /// <returns></returns>
    IEnumerator _OnLight()
    {
        yield return new WaitForSeconds(OnLightTime);
        blinking.SetActive(true);
    }

}
