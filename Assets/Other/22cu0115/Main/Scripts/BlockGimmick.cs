using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGimmick : MonoBehaviour
{
    [SerializeField, Header("BPM")]
    public float BPM;

    [SerializeField, Header("切り替えの猶予時間")]
    public float Tempo;

    // 再生時間(処理開始時間)
    public const float START_SECONDS = 0.0f;
    // 拍のタイミングを保存しておく
    [System.NonSerialized]
    public float INTERVAL_SECONDS;

    public GameObject[] gameObjects;
    // Start is called before the first frame update
    void Awake()
    {
        // 拍の計算
        INTERVAL_SECONDS = 60.0f / BPM;
        gameObjects[0].SetActive(false);
        // 呼び出す関数,呼び出し時間指定,指定したテンポで呼び出す
        Debug.Log(Mathf.Pow(INTERVAL_SECONDS, 2));
        InvokeRepeating("PlayBeat2", START_SECONDS, INTERVAL_SECONDS * 2);
       

    }

    public void PlayBeat2()
    {
        if(gameObjects[0].activeSelf == true || gameObjects[1].activeSelf == false)
        {
            gameObjects[0].SetActive(false);
            gameObjects[1].SetActive(true);
        }
        else
        {
            gameObjects[0].SetActive(true);
            gameObjects[1].SetActive(false);
        }
    }
}
