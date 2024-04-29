using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController_ComboJudgment : MonoBehaviour
{
    // �ϐ��錾
    [SerializeField, Header("�L�����o�X��RectTransform")]
    private RectTransform canvasRectTrans;
    [SerializeField, Header("�Ǐ]��̃I�u�W�F�N�g��Transform")]
    private Transform playerTrans;
    [SerializeField, Header("�I�t�Z�b�g�i���W�����炷�j")]
    private Vector3 offset = new Vector3(0f, 1.5f, 0f);

    // ���g��RectTransform�R���|�[�l���g
    private RectTransform myRectTrans;


    // Start is called before the first frame update
    void Start()
    {
        // �R���|�[�l���g�̎擾
        myRectTrans = this.GetComponent<RectTransform>();
        if ((canvasRectTrans == null) || (playerTrans == null))
        {
            canvasRectTrans = GameObject.Find("UICanvas").GetComponent<RectTransform>();
            playerTrans = GameObject.Find("Player").GetComponent<Transform>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        // �A�^�b�`���ꂽ�I�u�W�F�N�g�̍��W���^�[�Q�b�g�ƂȂ�I�u�W�F�N�g�̃��[���h���W����擾��UI��̍��W�ɕύX��������s��
        myRectTrans.position = RectTransformUtility.WorldToScreenPoint(Camera.main, playerTrans.position + offset);
    }
}
