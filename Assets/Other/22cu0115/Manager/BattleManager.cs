using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    BossEnemy bossEnemy = null;
    Player player = null;

    private bool isOnce;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null) return;
        if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossEnemy>() == null) return;

        bossEnemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossEnemy>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        // シーンロード
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if (isOnce) return;

        if (bossEnemy != null)
        {
            if (bossEnemy.GetBossHp() <= 0)
            {
                Debug.Log("しんだ");
                isOnce = true;
                StartCoroutine(WinDelayMethod(4.0f));
            }
        }

        if(player != null)
        {
            if(player.GetPlayerHp() <= 0)
            {
                Debug.Log("しんだ");
                isOnce = true;
                StartCoroutine(LoseDelayMethod(4.0f));
            }
        }
    }

    /// <summary>
    /// シーンが読み込まれたら
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null) return;
        
        bossEnemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossEnemy>();
    }

    private IEnumerator WinDelayMethod(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameManager.Instance.LoadScene(GameManager.SceneName.WinResult);
    }
    private IEnumerator LoseDelayMethod(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameManager.Instance.LoadScene2(GameManager.SceneName.LoseResult);
    }
}
