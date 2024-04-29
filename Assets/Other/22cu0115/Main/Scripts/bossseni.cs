using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bossseni : MonoBehaviour
{
    // Fade_In_Fade_Out fade_In_Fade_Out;
    // Start is called before the first frame update
    void Start()
    {
        //fade_In_Fade_Out = GameObject.Find("BlackoutCurtain").GetComponent<Fade_In_Fade_Out>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                SceneManager.LoadScene("Field");
            }
            if (SceneManager.GetActiveScene().name == "Field")
            { 
                SceneManager.LoadScene("MainEvent");  
            }

            //fade_In_Fade_Out.fade_Out = true;
            //if (fade_In_Fade_Out.nextSceneflag) { 
            //SceneManager.LoadScene("Main");
            //}
        }
    }
}
