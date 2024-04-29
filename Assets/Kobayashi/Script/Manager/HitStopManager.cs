using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopManager : SingletonMonoBehaviour<HitStopManager>
{
    private readonly static float TimeStopNumber = 0f;
    private readonly static float TimeStartNumber = 1f;

    /// <summary>
    /// �q�b�g�X�g�b�v���J�n����
    /// </summary>
    /// <param name="duration"></param>
    public void StartHitStop(float duration)
    {
        StartCoroutine(HitStopCoroutine(duration));
    }

    /// <summary>
    /// �q�b�g�X�g�b�v��Delay
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator HitStopCoroutine(float duration)
    {
        // �q�b�g�X�g�b�v�̊J�n
        Time.timeScale = TimeStopNumber;

        // �w�肵�����Ԃ�����~
        yield return new WaitForSecondsRealtime(duration);

        // �q�b�g�X�g�b�v�̏I��
        Time.timeScale = TimeStartNumber;
    }
}
