using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    enum Scene
    {
        Title,
        Tutorial0,
        Tutorial,
        Field,
        Result
    };

    Scene scenes;

    [SerializeField, Header("暗幕を起動させるフラグ(シーン開始時)")]
    static public bool fade_In;

    [SerializeField, Header("暗幕を起動させるフラグ(シーン終了時)")]
    static public bool fade_Out;

    [SerializeField, Header("フェードイン処理終了のフラグ")]
    static public bool SceneStartflag;

    [SerializeField, Header("フェードアウト処理終了のフラグ")]
    static public bool nextSceneflag;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //        if(SceneManager.GetActiveScene().name == "Title")
        //        {
        //            if (Input.anyKey)
        //            {
        //                fade_flag.fade_Out = true;
        //            }
        //            if (fade_flag.nextSceneflag)
        //            {
        //                SceneManager.LoadScene("Tutorial0");
        //            }
        //        }
        //        if (SceneManager.GetActiveScene().name == "Tutorial0")
        //        {
        //            if (Input.anyKey)
        //            {
        //                fade_flag.fade_Out = true;
        //            }
        //            if (fade_flag.nextSceneflag)
        //            {
        //                SceneManager.LoadScene("Field");
        //            }
        //        }
        //        if (SceneManager.GetActiveScene().name == "Result")
        //        {
        //            if (Input.anyKey)
        //            {
        //                fade_flag.fade_Out = true;
        //            }
        //            if (fade_flag.nextSceneflag)
        //            {
        //                SceneManager.LoadScene("Title");
        //            }
        //        }
    }
}
