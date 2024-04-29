using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �V�K�ǉ���
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade_In_Fade_Out : MonoBehaviour
{
    // [SerializeField, Header("�Ö��p�̃I�u�W�F�N�g")]
    // private GameObject blackoutCurtain;

    [SerializeField, Header("�Ö����N��������t���O(�V�[���J�n��)")]
     public bool fade_In;

    [SerializeField, Header("�Ö����N��������t���O(�V�[���I����)")]
     public bool fade_Out;

    [SerializeField, Header("�t�F�[�h�C�������I���̃t���O")]
     public bool SceneStartflag;

    [SerializeField, Header("�t�F�[�h�A�E�g�����I���̃t���O")]
     public bool nextSceneflag;

    [SerializeField, Header("�Ö��̃A���t�@�l�̌����l")]
    private float fade_InValue;

    [SerializeField, Header("�Ö��̃A���t�@�l�̑����l")]
    private float fade_OutValue;

    [SerializeField, Header("�Ö��̃J���[�̌��ݒl")]
    private Image blackoutCurtainColor;

    [SerializeField, Header("�X�s�[�h")]
    private float speed = 3;

    private float countTime;
    // Start is called before the first frame update
    void Start()
    {
        blackoutCurtainColor = this.gameObject.GetComponent<Image>();
        fade_In = true;
        nextSceneflag = false;
    }

    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime;
        
        if(countTime < speed)
        {
            if (fade_In)
            {
                _fadeIn();
            }
            else if (fade_Out)
            {
                _fadeOut();
            }

            countTime = 0f;
        }
    }

    public void _fadeIn()
    {
        if(blackoutCurtainColor.color.a > 0f)
        {
            blackoutCurtainColor.color = new Color(0f, 0f, 0f, blackoutCurtainColor.color.a - fade_InValue);
        }
        else
        {
            fade_In = false;
        }
    }

    public void _fadeOut()
    {
        if (blackoutCurtainColor.color.a < 1f)
        {
            blackoutCurtainColor.color = new Color(0f, 0f, 0f, blackoutCurtainColor.color.a + fade_OutValue);
        }
        else
        {
            fade_Out = false;
            nextSceneflag = true;
        }
    }
}
