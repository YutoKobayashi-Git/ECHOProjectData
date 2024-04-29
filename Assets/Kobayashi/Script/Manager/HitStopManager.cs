using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopManager : SingletonMonoBehaviour<HitStopManager>
{
    private readonly static float TimeStopNumber = 0f;
    private readonly static float TimeStartNumber = 1f;

    /// <summary>
    /// ヒットストップを開始する
    /// </summary>
    /// <param name="duration"></param>
    public void StartHitStop(float duration)
    {
        StartCoroutine(HitStopCoroutine(duration));
    }

    /// <summary>
    /// ヒットストップのDelay
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator HitStopCoroutine(float duration)
    {
        // ヒットストップの開始
        Time.timeScale = TimeStopNumber;

        // 指定した時間だけ停止
        yield return new WaitForSecondsRealtime(duration);

        // ヒットストップの終了
        Time.timeScale = TimeStartNumber;
    }
}
