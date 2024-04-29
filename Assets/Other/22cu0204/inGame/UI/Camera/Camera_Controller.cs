using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    // Low/medium/high
    enum ShakeType
    {
        Low = 0,
        medium,
        high,
        microvibration
    };
    
    // �ϐ��錾
    [SerializeField, Header("�K�p����I�u�W�F�N�g")]
    GameObject cameraObj;

    [SerializeField, Header("�h��̃��[�h")]
    ShakeType cameraDirection;

    [SerializeField, Header("�h�炷�傫���Fx��.y��")]
    Vector2[] shakeSize;

    [SerializeField, Header("�h�炷�Ԋu")]
    float shakeInterval;

    [SerializeField, Header("�h�炷�b��")]
    float shakeCount;

    [SerializeField, Header("���̏ꏊ")]
    private Vector3 cameraDefaultPosition;

    [SerializeField, Header("�e�X�g�p�t���O")]
    bool shakeFlag;
    
    private bool ValueSetFlag;

    private float reverseSize = 1;

    private float IntervalCount;
    private float shaketimeCount;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeFlag)
        {
            IntervalCount += Time.deltaTime;
            shaketimeCount += Time.deltaTime;
            // �w�肳�ꂽ���[�h�̗h��������s
            if (shakeCount >= shaketimeCount)
            {
                if (IntervalCount >= shakeInterval)
                {
                    _camera_Shake();
                    IntervalCount = 0f;
                    shakeCount -= 1;
                }
            }
            else
            {
                cameraObj.transform.position = cameraDefaultPosition;
                shakeFlag = false;
            }
        }
        else
        {
            ValueSetFlag = true;
        }
    }

    private void _shake_X(float _size_X)
    {
        cameraObj.transform.position = new Vector3(cameraDefaultPosition.x + _size_X * reverseSize, cameraDefaultPosition.y, cameraDefaultPosition.z);
    }

    private void _shake_Y(float _size_Y)
    {
        cameraObj.transform.position = new Vector3(cameraDefaultPosition.x, cameraDefaultPosition.y + _size_Y * reverseSize, cameraDefaultPosition.z);
    }

    private void _camera_Shake()
    {
        reverseSize *= -1;
        switch (cameraDirection)
        {

            case ShakeType.Low:
                {
                    _shake_X(shakeSize[(int)ShakeType.Low].x);
                    _shake_Y(shakeSize[(int)ShakeType.Low].y);
                    break;
                }
            case ShakeType.medium:
                {
                    _shake_X(shakeSize[(int)ShakeType.medium].x);
                    _shake_Y(shakeSize[(int)ShakeType.medium].y);
                    break;
                }
            case ShakeType.high:
                {
                    _shake_X(shakeSize[(int)ShakeType.high].x);
                    _shake_Y(shakeSize[(int)ShakeType.high].y);
                    break;
                }
            case ShakeType.microvibration:
                {
                    _shake_X(shakeSize[(int)ShakeType.microvibration].x);
                    _shake_Y(shakeSize[(int)ShakeType.microvibration].y);
                    break;
                }
        }
    }

    private void _Boot_CameraShake(float _setTime)
    {
        shakeFlag = true;
    }
}
