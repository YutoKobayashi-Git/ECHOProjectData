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

    [SerializeField, Header("�Ö����N��������t���O(�V�[���J�n��)")]
    static public bool fade_In;

    [SerializeField, Header("�Ö����N��������t���O(�V�[���I����)")]
    static public bool fade_Out;

    [SerializeField, Header("�t�F�[�h�C�������I���̃t���O")]
    static public bool SceneStartflag;

    [SerializeField, Header("�t�F�[�h�A�E�g�����I���̃t���O")]
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
