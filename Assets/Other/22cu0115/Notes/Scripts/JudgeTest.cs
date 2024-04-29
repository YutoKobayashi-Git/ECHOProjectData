using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class JudgeTest : BaseObjectPool
{
    //�ϐ��̐錾
    [SerializeField, Header("����̃v���n�u")]
    private GameObject judgeObj;

    // �v���C���[�ɔ����`����Q�[���I�u�W�F�N�g
    [SerializeField, Header("Perfect")]
    private RectTransform messageTrans;     

    Animator animator;
    GameObject[] notesObj;      // �m�[�c�v���n�u
    int cnt = 1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // �I�u�W�F�N�g�쐬
        Create_Pool(5,"�p�[�t�F�N�g");
        Create_Pool(5,"�~�X");
        Create_Pool(5,"�X���b�V��");

        // ����I�u�W�F�N�g��\��
        judgeObj.SetActive(false);
        notesObj = GameObject.FindGameObjectsWithTag("Notes");
    }

    // InputActions
    public void OnFire(InputAction.CallbackContext context)
    {
        if (notesObj == null) return;
        // �����ꂽ�u�Ԃ���
        if (!context.performed) return;
        // �����ꂽ�u�Ԃɔ���̃I�u�W�F�N�g�ƃm�[�c�̋�����
        // �����Ă���ɉ���������̃e�L�X�g��\������
        for (int i = 0; i < notesObj.Length; ++i)
        {
            var dis = Vector3.Distance(judgeObj.transform.position, notesObj[i].transform.position);
            if (cnt % 2 == 0) return;
            //Debug.Log(cnt);
            //Debug.Log(dis);
            if (dis <= 400f)
            {
                cnt += 2;
                // �I�u�W�F�N�g�\���܂��͐���
                Get_Obj(messageTrans, "�p�[�t�F�N�g");

                // Sound
                SoundManager.Instance.Play("Zangeki");
                SoundManager.Instance.Play("Destruction");

                animator.SetTrigger("SP_trg");

                notesObj[i].SetActive(false);
                ScoreManager.Instance.perfect++;
                ScoreManager.Instance.perfectComboCnt++;
            }
            else if (dis <= 600f && dis > 450f)
            {
                cnt += 2;
                // �I�u�W�F�N�g�\���܂��͐���
                Get_Obj(messageTrans, "�~�X");

                // Sound
                SoundManager.Instance.Play("Zangeki");

                notesObj[i].SetActive(false);
                ScoreManager.Instance.miss++;
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // ���݂��Ă���A�C�e���̐���������
        notesObj = GameObject.FindGameObjectsWithTag("Notes");

        if (ScoreManager.Instance.specialFireFrag)
        {
            judgeObj.SetActive(true);
        }
        else
        {
            cnt = 1;
            SetObjActiveFalse("�X���b�V��");
            SetObjActiveFalse("�p�[�t�F�N�g");
            judgeObj.SetActive(false);
        }
    }
}
