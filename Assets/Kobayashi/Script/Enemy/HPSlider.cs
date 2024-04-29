using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSlider : MonoBehaviour
{
    // �ϐ��錾
    // -------------------------------------------------
        public Slider HpSlider;

        Character character;
    // ------------------------------------------------- 
    // ------------------------------------------------- 

    private void Start()
    {
        //Slider�𖞃^���ɂ���B
        HpSlider.value = 1;

        character = this.gameObject.GetComponent<Character>();
    }

    private void Update()
    {
        _hpDown();
    }

    private void _hpDown()
    {
        //�ő�HP�ɂ����錻�݂�HP��Slider�ɔ��f�B
        HpSlider.value = (float)character.status.GetHp() / (float)character.status.MaxHp;
    }

}
