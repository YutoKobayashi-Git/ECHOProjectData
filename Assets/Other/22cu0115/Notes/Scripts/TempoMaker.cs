using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempoMaker : BaseObjectPool
{
    [SerializeField, Header("BGM")]
    private AudioSource audioSource;
    [SerializeField, Header("生成するノーツ")]
    private RectTransform notesTrans;

    [SerializeField, Header("親")]
    private Canvas parent;
    [SerializeField, Header("BPM")]
    private float BPM;

    private const float START_SECONDS = 0.0f;         // 再生時間(処理開始時間)
    private float INTERVAL_SECONDS;         // 拍のタイミングを保存しておく

    private Animator animator;
    private bool IsCalledOnce;
    private bool IsCalledOnce2;
    private float time;

    private List<GameObject> notesList = new List<GameObject>();


    // Start is called before the first frame update
    private void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;

        // 拍の計算
        INTERVAL_SECONDS = 60.0f / BPM;

        // オブジェクト作成
        Create_Pool(5, "ノーツ");

        // 呼び出す関数,呼び出し時間指定,指定したテンポで呼び出す
        InvokeRepeating("PlayBeat", START_SECONDS, INTERVAL_SECONDS);
    }

    /// <summary>
    /// 表迫が来るたびに呼び出される
    /// </summary>
    private void PlayBeat()
    {
        // 初回だけ読み込む
        if (!IsCalledOnce)
        {
            audioSource.Play();
            IsCalledOnce = true;
        }

        // フラグが立っている間、三つまでノーツを表示する
        if (ScoreManager.Instance.specialFireFrag && time >= 4.5f)
        {
            if (ScoreManager.Instance.notesCnt < 3)
            {
                if (!IsCalledOnce2)
                {
                    IsCalledOnce2 = true;
                }

                // オブジェクト表示または生成
                Get_Obj(notesTrans, "ノーツ");
            }

            // ３回成功したら
            if (ScoreManager.Instance.perfectComboCnt >= 3)
            {
                // 三回連続で成功したら派生
                animator.SetTrigger("Attack04_trg");
                ScoreManager.Instance.perfectComboCnt = 0;
            }

            ScoreManager.Instance.notesCnt++;
        }
    }

    private void Update()
    {
        if (ScoreManager.Instance.specialFireFrag)
        {
            time += Time.deltaTime;
        }
        else
        {
            IsCalledOnce2 = false;
            time = 0;
        }
    }
}
