using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour
{
    // �ϐ��錾
    [Header("���C�t�̍ő�l"), SerializeField]
    private int life_Max;
    [Header("�Q�[�W�̃T�C�Y"), SerializeField]
    private Vector2 lifeGauge_Size;
    [Header("�Q�[�W�̃g�����X�t�H�[���i���W���j"), SerializeField]
    private RectTransform lifeGauge_RectTransform;

    [Header("���C�t�̌��ݒl")]
    public int Life;
    [Header("���݂̃Q�[�W�̃T�C�Y�{��")]
    public float _total;

    // Start is called before the first frame update
    void Start()
    {
        // �A�^�b�`���ꂽ�̗̓Q�[�W�p�I�u�W�F�N�g�̍��W�����擾
        lifeGauge_RectTransform = this.GetComponent<RectTransform>();
        lifeGauge_RectTransform.sizeDelta = lifeGauge_Size;
        Life = life_Max;
    }

    // Update is called once per frame
    void Update()
    {
    }
    // �_���[�W���󂯂��ۂ̃Q�[�W�̕ύX�i�v���C���[������Ăяo���j 
    public void Life_Damage(int _Damege)
    {
        // �̗͂��c���Ă���ꍇ�ɏ������s��
        if(Life > 0)
        {
            // �Ǘ����Ă���life���_���[�W�����炷
            Life -= _Damege;
            // ���݂̃Q�[�W�̃T�C�Y�{�����v�Z
            _total = (float)Life / (float)life_Max;
            lifeGauge_RectTransform.sizeDelta = new Vector2(lifeGauge_Size.x * _total, lifeGauge_Size.y);
            lifeGauge_RectTransform.localPosition = new Vector3(lifeGauge_RectTransform.localPosition.x - (float)_Damege / 2f, lifeGauge_RectTransform.localPosition.y, lifeGauge_RectTransform.localPosition.z);
        }
    }

    // ���݂�HP
    public int Get_Hp()
    {
        return Life;
    }
}
