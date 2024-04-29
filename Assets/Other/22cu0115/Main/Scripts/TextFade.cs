using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;

    [SerializeField]
    private Image spRend;
    private float alpha = 0;

    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spRend.color = new Color(spRend.color.r, spRend.color.g, spRend.color.b, alpha);

        if(alpha >= 1)
        {
            flag = true;
        }
        else if(alpha <= 0)
        {
            flag = false;
        }

        if (flag)
        {
            alpha -= speed * Time.deltaTime;
        }
        else
        {
            alpha += speed * Time.deltaTime;
        }

        
    }
}
