using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    UIController_ComboValue comboDisplay;

    private void Awake()
    {
        comboDisplay = GameObject.Find("ComboValue").GetComponent<UIController_ComboValue>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if (ScoreManager.Instance.specialGauge < 11)
            {
                // �Q�[�W�㏸
                ScoreManager.Instance.specialGauge++;
                // �R���{�֐��Ăяo��
                comboDisplay._Combo_Success();

            }
        }
    }

    //protected override void _Gauge_Rising()
    //{
    //
    //}
}
