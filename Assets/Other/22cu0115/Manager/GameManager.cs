using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField]
    private Fade_In_Fade_Out fade_flag;
    private float time;

    public enum SceneName
    {
        Title = 0,
        Tutorial,
        Field,
        Main,
        Thursday,
        WinResult,
        LoseResult
    }
    public bool NormalAttack;
    public bool Jump;
    public bool Avoid;
    public bool SpecialAttack;
    private void Start()
    {
        Application.targetFrameRate = 60;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// シーンが読み込まれたら
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == (int)SceneName.Title)
        {
            ScoreManager.Instance.InitializeScore();
        }

        if (SceneManager.GetActiveScene().name == "Tutorial" || SceneManager.GetActiveScene().name == "Title")
        {
            NormalAttack = false;
            Jump = false;
            Avoid = false;
            SpecialAttack = false;
        }
        else
        {
            NormalAttack = true;
            Jump = true;
            Avoid = true;
            SpecialAttack = true;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        SceneControl();


        // デバッグ用
        if (Input.GetKey(KeyCode.F1))
        {
            SceneManager.LoadScene("Title");
        }
        if (Input.GetKey(KeyCode.F2))
        {
            SceneManager.LoadScene("Tutorial");
        }
        if (Input.GetKey(KeyCode.F3))
        {
            SceneManager.LoadScene("Field");
        }
        if (Input.GetKey(KeyCode.F4))
        {
            SceneManager.LoadScene("Main");
        }
        if (Input.GetKey(KeyCode.F5))
        {
            SceneManager.LoadScene("WinResult");
        }
        if (Input.GetKey(KeyCode.F6))
        {
            SceneManager.LoadScene("MainEvent");
        }
        if (Input.GetKey(KeyCode.K))
        {
            NormalAttack = true;
            Jump = true;
            Avoid = true;
            SpecialAttack = true;
        }
    }

    private void SceneControl()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            time += Time.deltaTime;
            if (Input.anyKey && time >= 2)
            {
                SoundManager.Instance.Play("Select");
                SceneManager.LoadScene("Tutorial");
                //fade_flag.fade_Out = true;
            }
            if (fade_flag.nextSceneflag)
            {
            }
        }

        if (SceneManager.GetActiveScene().name == "WinResult")
        {
            time += Time.deltaTime;
            if (Input.anyKey && time >= 2)
            {
                SceneManager.LoadScene("Title");
                // fade_flag.fade_Out = true;
            }
            if (fade_flag.nextSceneflag)
            {

            }
        }
        if (SceneManager.GetActiveScene().name == "LoseResult")
        {
            time += Time.deltaTime;
            if (Input.anyKey && time >= 2)
            {
                SceneManager.LoadScene("Title");
                // fade_flag.fade_Out = true;
            }
            if (fade_flag.nextSceneflag)
            {

            }
        }
    }
    public void LoadScene(SceneName name)
    {
        SceneManager.LoadScene("WinResult");
    }

    public void LoadScene2(SceneName name)
    {
        SceneManager.LoadScene("LoseResult");
    }
}
