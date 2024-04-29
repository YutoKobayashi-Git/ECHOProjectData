using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGimmick : MonoBehaviour
{
    [SerializeField, Header("BPM")]
    public float BPM;

    [SerializeField, Header("�؂�ւ��̗P�\����")]
    public float Tempo;

    // �Đ�����(�����J�n����)
    public const float START_SECONDS = 0.0f;
    // ���̃^�C�~���O��ۑ����Ă���
    [System.NonSerialized]
    public float INTERVAL_SECONDS;

    public GameObject[] gameObjects;
    // Start is called before the first frame update
    void Awake()
    {
        // ���̌v�Z
        INTERVAL_SECONDS = 60.0f / BPM;
        gameObjects[0].SetActive(false);
        // �Ăяo���֐�,�Ăяo�����Ԏw��,�w�肵���e���|�ŌĂяo��
        Debug.Log(Mathf.Pow(INTERVAL_SECONDS, 2));
        InvokeRepeating("PlayBeat2", START_SECONDS, INTERVAL_SECONDS * 2);
       

    }

    public void PlayBeat2()
    {
        if(gameObjects[0].activeSelf == true || gameObjects[1].activeSelf == false)
        {
            gameObjects[0].SetActive(false);
            gameObjects[1].SetActive(true);
        }
        else
        {
            gameObjects[0].SetActive(true);
            gameObjects[1].SetActive(false);
        }
    }
}
