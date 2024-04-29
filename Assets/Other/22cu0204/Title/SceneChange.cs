using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
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

    [SerializeField, Header("フェードイン・アウト用のオブジェクト")]
    private Fade_In_Fade_Out fade_flag;
    // Start is called before the first frame update
    void Start()
    {
        fade_flag = GameObject.Find("BlackoutCurtain").GetComponent<Fade_In_Fade_Out>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Title")
        {
            if (Input.anyKey)
            {
                fade_flag.fade_Out = true;
            }
            if (fade_flag.nextSceneflag)
            {
                SceneManager.LoadScene("Tutorial0");
            }
        }
        if (SceneManager.GetActiveScene().name == "Tutorial0")
        {
            if (Input.anyKey)
            {
                fade_flag.fade_Out = true;
            }
            if (fade_flag.nextSceneflag)
            {
                SceneManager.LoadScene("Tutorial");
            }
        }
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            if (Input.anyKey)
            {
                fade_flag.fade_Out = true;
            }
            if (fade_flag.nextSceneflag)
            {
                SceneManager.LoadScene("Field");
            }
        }
        if (SceneManager.GetActiveScene().name == "Field")
        {
            if (Input.anyKey)
            {
                fade_flag.fade_Out = true;
            }
            if (fade_flag.nextSceneflag)
            {
                SceneManager.LoadScene("Result");
            }
        }
        if (SceneManager.GetActiveScene().name == "Result")
        {
            if (Input.anyKey)
            {
                fade_flag.fade_Out = true;
            }
            if (fade_flag.nextSceneflag)
            {
                SceneManager.LoadScene("Title");
            }
        }
    }
}
