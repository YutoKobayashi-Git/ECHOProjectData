using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField, Header("すべてのステージライト")] 
    public GameObject blinking;

    [SerializeField, Header("再度ライトをつけるまでの時間")]
    private float OnLightTime = 11.0f;

    /// <summary>
    /// ライトを消す
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
    /// ライト再度つける
    /// </summary>
    /// <returns></returns>
    IEnumerator _OnLight()
    {
        yield return new WaitForSeconds(OnLightTime);
        blinking.SetActive(true);
    }

}
