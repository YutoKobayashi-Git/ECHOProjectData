using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController_ComboValue : MonoBehaviour
{
    // �ϐ��錾
    [SerializeField, Header("�R���{�p�̃e�L�X�g")] 
    private TMPro.TMP_Text comboText;
    [SerializeField, Header("�R���{�̐�����")]
    private int comboNum;
    [SerializeField, Header("�R���{�̘A�������̍ő�l")]
    private int comboNumMax;
    [SerializeField, Header("�R���{�������邱�Ƃ��\�Ȏ���")]
    private float comboActiveTime;
    [SerializeField, Header("���Ԍv���p")]
    private float comboCountTime;
    [Header("�R���{�̏���l")] 
    const int stopCombo = 10000;

    
    // Start is called before the first frame update
    void Start()
    {
        // �R���|�[�l���g�̎擾�E������
        comboText = this.GetComponent<TMP_Text>();
        comboText.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // �R���{�������Ă��邩�ǂ����𔻒肵�A�R���{�I�����ɏ��������s��
        if(comboNum !=0)
        {
            comboCountTime += Time.deltaTime;
        }
        if(comboCountTime > comboActiveTime)
        {
            comboText.alpha = 0f;
            comboCountTime = 0f;
            _Combo_failure();
            //Debug.Log("ComboTimer Reset");
        }

        // �e�L�X�g�Ƃ��ĕ\��
        comboText.SetText(comboNum.ToString());
    }

    // �R���{���J�n���鏈���i�v���C���[�����瑀��j
    public void _Combo_Success()
    {
        // �R���{�̏���l��ݒ�
        if(comboNum < stopCombo)
        {
            comboText.alpha = 1f;
            // �^�C�}�[�����Z�b�g���R���{����������\������
            comboCountTime = 0f;
            ++comboNum;
        }
    }
    // �R���{�A�������̍ő�l�̍X�V���s������
    void _Combo_failure()
    {
        if(comboNumMax <= comboNum)
        {
            comboNumMax = comboNum;
        }
        comboNum = 0;
    }
}
