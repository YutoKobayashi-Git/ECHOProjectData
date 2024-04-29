using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PushLight : MonoBehaviour
{
    [SerializeField,Header("�_�ŃX�s�[�h")]
    private float speed = 3;

    [SerializeField,Header("0 : �@��������� , 1 : �p�[�t�F�N�g�̂Ƃ��Ɍ���")]
    private RawImage[] rend;

    [SerializeField, Header("��ʃt���b�V��")]
    private Image image;

    private float[] alpha = new float[3];
    private int oldCnt;

    public void OnFire(InputAction.CallbackContext context)
    {
        // �����ꂽ�u�Ԃ���
        if (!context.performed) return;

        // ����
        ColorChange(rend[0] , 0);
    }

    // Update is called once per frame
    void Update()
    {
        // �A���t�@�l����
        for (int i = 0; i < alpha.Length; ++i)
        {
            alpha[i] -= speed * Time.deltaTime;
        }

        // �␳
        for (int i = 0; i < rend.Length; ++i)
        {
            if (!(rend[i].color.a < 0))
            {
                rend[i].color = new Color(rend[i].color.r, rend[i].color.r, rend[i].color.r, alpha[i]);
            }
        }
        if(!(image.color.a < 0))
        {
            image.color = new Color(image.color.r, image.color.r, image.color.r, alpha[2]);
        }

        // �p�[�t�F�N�g�̂Ƃ�
        if (oldCnt != ScoreManager.Instance.perfectComboCnt)
        {
            oldCnt = ScoreManager.Instance.perfectComboCnt;
            FlashLight(image, 2);
            ColorChange(rend[1] , 1);
        }

        // ������
        if (!ScoreManager.Instance.specialFireFrag)
        {
            oldCnt = 0;
        }

    }

    // �A���t�@�l�ύX
    void ColorChange(RawImage image , int num)
    {
        alpha[num] = 1f;
        image.color = new Color(image.color.r, image.color.r, image.color.r, alpha[num]);
    }
    void FlashLight(Image image, int num)
    {
        alpha[num] = 1f;
        image.color = new Color(image.color.r, image.color.r, image.color.r, alpha[num]);
    }
}
