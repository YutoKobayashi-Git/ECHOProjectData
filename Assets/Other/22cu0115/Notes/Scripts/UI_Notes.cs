using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Notes : MonoBehaviour
{
    [SerializeField, Header("����̃v���n�u")]
    private GameObject judgeObj;
    //�m�[�c�̃X�s�[�h��ݒ�
    public int NoteSpeed = 10;
    // ���Ԍv��
    float time;
    RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        // �ړ�
        rect.localPosition += new Vector3(-NoteSpeed * Time.deltaTime, 0, 0);

        time += Time.deltaTime;

        if (time >= 5)
        {
            gameObject.SetActive(false);
            time = 0;
        }

    }

    public void fire()
    {

        var dis = Vector3.Distance(judgeObj.transform.position, gameObject.transform.position);

        //Debug.Log(cnt);
        //Debug.Log(dis);
        if (dis <= 200f)
        {
            //notesObj[i].SetActive(false);
            gameObject.SetActive(false);


        }
    }
}
