using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempoMaker : BaseObjectPool
{
    [SerializeField, Header("BGM")]
    private AudioSource audioSource;
    [SerializeField, Header("��������m�[�c")]
    private RectTransform notesTrans;

    [SerializeField, Header("�e")]
    private Canvas parent;
    [SerializeField, Header("BPM")]
    private float BPM;

    private const float START_SECONDS = 0.0f;         // �Đ�����(�����J�n����)
    private float INTERVAL_SECONDS;         // ���̃^�C�~���O��ۑ����Ă���

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

        // ���̌v�Z
        INTERVAL_SECONDS = 60.0f / BPM;

        // �I�u�W�F�N�g�쐬
        Create_Pool(5, "�m�[�c");

        // �Ăяo���֐�,�Ăяo�����Ԏw��,�w�肵���e���|�ŌĂяo��
        InvokeRepeating("PlayBeat", START_SECONDS, INTERVAL_SECONDS);
    }

    /// <summary>
    /// �\�������邽�тɌĂяo�����
    /// </summary>
    private void PlayBeat()
    {
        // ���񂾂��ǂݍ���
        if (!IsCalledOnce)
        {
            audioSource.Play();
            IsCalledOnce = true;
        }

        // �t���O�������Ă���ԁA�O�܂Ńm�[�c��\������
        if (ScoreManager.Instance.specialFireFrag && time >= 4.5f)
        {
            if (ScoreManager.Instance.notesCnt < 3)
            {
                if (!IsCalledOnce2)
                {
                    IsCalledOnce2 = true;
                }

                // �I�u�W�F�N�g�\���܂��͐���
                Get_Obj(notesTrans, "�m�[�c");
            }

            // �R�񐬌�������
            if (ScoreManager.Instance.perfectComboCnt >= 3)
            {
                // �O��A���Ő���������h��
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
