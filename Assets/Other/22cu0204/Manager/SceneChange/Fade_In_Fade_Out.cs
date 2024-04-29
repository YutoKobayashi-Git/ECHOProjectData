using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 新規追加分
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade_In_Fade_Out : MonoBehaviour
{
    // [SerializeField, Header("暗幕用のオブジェクト")]
    // private GameObject blackoutCurtain;

    [SerializeField, Header("暗幕を起動させるフラグ(シーン開始時)")]
     public bool fade_In;

    [SerializeField, Header("暗幕を起動させるフラグ(シーン終了時)")]
     public bool fade_Out;

    [SerializeField, Header("フェードイン処理終了のフラグ")]
     public bool SceneStartflag;

    [SerializeField, Header("フェードアウト処理終了のフラグ")]
     public bool nextSceneflag;

    [SerializeField, Header("暗幕のアルファ値の減少値")]
    private float fade_InValue;

    [SerializeField, Header("暗幕のアルファ値の増加値")]
    private float fade_OutValue;

    [SerializeField, Header("暗幕のカラーの現在値")]
    private Image blackoutCurtainColor;

    [SerializeField, Header("スピード")]
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
