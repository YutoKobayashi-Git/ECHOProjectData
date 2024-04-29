using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    // 敵を倒したときのスコア
    [SerializeField] TextMeshProUGUI scoreLabel = default;
    private int eliminateScore = 0;
    
    // いずれprivate指定子に変更して直接値をいじれないようにする
    // Perfectの数を保持する
    public int perfect;
    // Missの数
    public int miss;
    // クリアタイム
    private float clearTime;


    // 技のゲージ
    public int specialGauge = 0;
    // 技のボタンが押されたか
    public bool specialFireFrag = false;
    // ノーツが出現する数
    public int notesCnt = 0;
    // Perfectが何回連続で出てるかカウントする
    public int perfectComboCnt = 0;

    //以下石塚
    public bool EXAttackJudgeFrag;
    public bool EXAttackFrag;
    public bool AttackJudgeFrag;
    public bool AttackFrag;

    void Start()
    {
        if (scoreLabel != null)
        {
            scoreLabel.text = "" + eliminateScore;
        }
    }

    /// <summary>
    /// クリアタイム
    /// </summary>
    /// <returns></returns>
    public double GetCrearTime()
    {
        return (double)clearTime;
    }

    /// <summary>
    /// スコア初期化
    /// </summary>
    public void InitializeScore()
    {

        eliminateScore = 0;
        specialGauge = 0;
        perfect = 0;
        miss = 0;
    }

    /// <summary>
    /// スコア設定
    /// </summary>
    /// <param name="scoreValue"></param>
    public void Set_Score(int scoreValue)
    {
        if (scoreLabel != null)
        {
            eliminateScore = scoreValue;
        }
    }

    /// <summary>
    /// スコア加算
    /// </summary>
    /// <param name="scoreValue">加算する値</param>
    public void Add_Score(int scoreValue)
    {
        if (scoreLabel != null)
        {
            eliminateScore += scoreValue;
            scoreLabel.text = "" + eliminateScore;
        }
    }

    /// <summary>
    /// 現在のスコアの数値を獲得する
    /// </summary>
    /// <param name="name">eliminateScore,perfet,miss</param>
    /// <returns></returns>
    public int Get_Score(string name)
    {
        if(name == "eliminateScore")
        {
            return eliminateScore;
        }
        else if(name == "perfect")
        {
            return perfect;
        }
        else if (name == "miss")
        {
            return miss;
        }
        return 0;
    }

    // デバッグ用
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Add_Score(100);
        }

        if (SceneManager.GetActiveScene().name == "Main" || SceneManager.GetActiveScene().name == "Field")
        {
            clearTime += Time.deltaTime;
        }

        if (scoreLabel == null)
        {
            if (GameObject.FindGameObjectWithTag("Score") != null)
            {
                scoreLabel = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
                scoreLabel.text = "" + eliminateScore;
            }
        }
        
    }
}
