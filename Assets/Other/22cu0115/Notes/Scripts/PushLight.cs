using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PushLight : MonoBehaviour
{
    [SerializeField,Header("点滅スピード")]
    private float speed = 3;

    [SerializeField,Header("0 : 叩いたら光る , 1 : パーフェクトのときに光る")]
    private RawImage[] rend;

    [SerializeField, Header("画面フラッシュ")]
    private Image image;

    private float[] alpha = new float[3];
    private int oldCnt;

    public void OnFire(InputAction.CallbackContext context)
    {
        // 押された瞬間だけ
        if (!context.performed) return;

        // 光る
        ColorChange(rend[0] , 0);
    }

    // Update is called once per frame
    void Update()
    {
        // アルファ値減少
        for (int i = 0; i < alpha.Length; ++i)
        {
            alpha[i] -= speed * Time.deltaTime;
        }

        // 補正
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

        // パーフェクトのとき
        if (oldCnt != ScoreManager.Instance.perfectComboCnt)
        {
            oldCnt = ScoreManager.Instance.perfectComboCnt;
            FlashLight(image, 2);
            ColorChange(rend[1] , 1);
        }

        // 初期化
        if (!ScoreManager.Instance.specialFireFrag)
        {
            oldCnt = 0;
        }

    }

    // アルファ値変更
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
