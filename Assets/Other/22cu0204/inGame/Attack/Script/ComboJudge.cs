using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboJudge : MonoBehaviour
{
    // �ϐ��錾
    private RectTransform RectTrans;
    private GameObject parentObj;
    private GameObject canvasObj;
    private float TimeCount;

    [SerializeField, Header("���݂̃X�P�[��")] Vector3 scale;
    [SerializeField, Header("�U�����̔���̃X�P�[��")] public Vector3[] AttackScale;
    [SerializeField, Header("�X�P�[���̒l�̌����l")] float downScaleValue;
    [SerializeField, Header("�����ɂ���X�P�[��")] float successScaleValue;
    [SerializeField, Header("����̃X�P�[�������������鎞�Ԃ̒l")] float downScaleTime;
    [SerializeField, Header("�������E���s���ɕ\��������G�t�F�N�g")] GameObject[] judgeEffect;
    [SerializeField, Header("�v���C���[�̃I�u�W�F�N�g")] PlayerFireSaka playerFire;

    // Start is called before the first frame update
    void Start()
    {
        // �A�^�b�`���ꂽ�I�u�W�F�N�g�̃R���|�[�l���g�����擾����
        playerFire = GameObject.Find("Player").GetComponent<PlayerFireSaka>();
        parentObj = transform.parent.gameObject;
        canvasObj = parentObj.transform.parent.gameObject;
        RectTrans = this.GetComponent<RectTransform>();
        scale = RectTrans.localScale;
        parentObj.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.Instance.AttackJudgeFrag)
        {
            // ���Ԃ̌v��
            TimeCount += Time.deltaTime;

            if (TimeCount >= downScaleTime)
            {
                scale = new Vector3(scale.x - downScaleValue, scale.y - downScaleValue, 1f);
                RectTrans.localScale = scale;
                TimeCount = 0f;
            }

            // �C���Q�[�����ɂ���i�s���Ǘ�����I�u�W�F�N�g�̕ϐ��ɂ���ď������s��������
            // �I�u�W�F�N�g�̃X�P�[�������ɖ߂��i�s���Ǘ�����I�u�W�F�N�g�̕ϐ���ω�������
            if (RectTrans.localScale.x <= 0f)
            {
                _Next_JudgeScale(AttackScale[0]);
                Instantiate(judgeEffect[1], parentObj.transform.position, Quaternion.identity, canvasObj.transform);
                playerFire._Phase_Reset();
                ScoreManager.Instance.AttackJudgeFrag = false;
                parentObj.gameObject.SetActive(false);
                TimeCount = 0f;
            }
        }
    }

    // ���݂̃X�P�[���𔻒肵�A2~N�i�ڂ̍U���𔭐������邩���Ǘ�����ϐ���ύX���鏈��
    public void _Attack_Judge()
    {
        if (RectTrans.localScale.x <= successScaleValue)
        {
            ScoreManager.Instance.AttackFrag = true;
            Instantiate(judgeEffect[0], parentObj.transform.position, Quaternion.identity, canvasObj.transform);
            Debug.Log("iiyo");
        }
        else
        {
            ScoreManager.Instance.AttackFrag = false;
            Instantiate(judgeEffect[1], parentObj.transform.position, Quaternion.identity, canvasObj.transform);
            playerFire.inputStopTime = 1f;
        }
        ScoreManager.Instance.AttackJudgeFrag = false;
        parentObj.gameObject.SetActive(false);
    }


    public void _Next_JudgeScale(Vector2 _NextScale)
    {
        scale = _NextScale;

        RectTrans.localScale = scale;
    }
}
