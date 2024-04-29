using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    // �G��|�����Ƃ��̃X�R�A
    [SerializeField] TextMeshProUGUI scoreLabel = default;
    private int eliminateScore = 0;
    
    // ������private�w��q�ɕύX���Ē��ڒl��������Ȃ��悤�ɂ���
    // Perfect�̐���ێ�����
    public int perfect;
    // Miss�̐�
    public int miss;
    // �N���A�^�C��
    private float clearTime;


    // �Z�̃Q�[�W
    public int specialGauge = 0;
    // �Z�̃{�^���������ꂽ��
    public bool specialFireFrag = false;
    // �m�[�c���o�����鐔
    public int notesCnt = 0;
    // Perfect������A���ŏo�Ă邩�J�E���g����
    public int perfectComboCnt = 0;

    //�ȉ��Β�
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
    /// �N���A�^�C��
    /// </summary>
    /// <returns></returns>
    public double GetCrearTime()
    {
        return (double)clearTime;
    }

    /// <summary>
    /// �X�R�A������
    /// </summary>
    public void InitializeScore()
    {

        eliminateScore = 0;
        specialGauge = 0;
        perfect = 0;
        miss = 0;
    }

    /// <summary>
    /// �X�R�A�ݒ�
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
    /// �X�R�A���Z
    /// </summary>
    /// <param name="scoreValue">���Z����l</param>
    public void Add_Score(int scoreValue)
    {
        if (scoreLabel != null)
        {
            eliminateScore += scoreValue;
            scoreLabel.text = "" + eliminateScore;
        }
    }

    /// <summary>
    /// ���݂̃X�R�A�̐��l���l������
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

    // �f�o�b�O�p
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
