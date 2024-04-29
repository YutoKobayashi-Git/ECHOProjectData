using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_Shaking : MonoBehaviour
{
    // Low/medium/high/microvibrationの4種類から選んで使用する
    public enum ShakeType
    {
        Low = 0,
        medium,
        high,
        microvibration
    };
    
    // 変数宣言
    [SerializeField, Header("適用するカメラオブジェクト")]
    GameObject cameraObj;

    [SerializeField, Header("揺れのモード")]
    ShakeType cameraDirection;

    [SerializeField, Header("揺らす大きさ：x軸.y軸")]
    float[] shakeSize = new float[4];


    [SerializeField, Header("揺らす秒数")]
    float shakeCount;

    [SerializeField, Header("一周に必要な秒数")]
    float time_X;

    [SerializeField, Header("一周に必要な秒数")]
    float time_Y;



    [SerializeField, Header("元の場所")]
    private Vector3 cameraDefaultPosition;

    [SerializeField, Header("テスト用フラグ")]
    bool shakeFlag;

    // sin関数の計算結果
    private float sin_X;
    private float sin_Y;

    // 周波数
    private float frequency_X;
    private float frequency_Y;

    // 時間カウント用
    private float shaketimeCount;
    // Start is called before the first frame update
    void Start()
    {
        if (cameraObj == null)
        {

            cameraObj = this.gameObject;
            cameraDefaultPosition = cameraObj.transform.position;
        }
        frequency_X = 1f / time_X;
        frequency_Y = 1f / time_Y;
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeFlag)
        {;
            shaketimeCount += Time.deltaTime;
            // 指定されたモードの揺れ方を実行
            if (shakeCount >= shaketimeCount)
            {
                _camera_Shake();
            }
            else
            {
                shaketimeCount = 0f;
                cameraObj.transform.position = cameraDefaultPosition;
                shakeFlag = false;
            }
        }
    }

    private void _shake_X(float _size_X)
    {
        sin_X = Mathf.Sin(2* Mathf.PI * frequency_X * Time.time);
        cameraObj.transform.position = new Vector3(cameraDefaultPosition.x + _size_X * sin_X, cameraObj.transform.position.y, cameraObj.transform.position.z);
    }

    private void _shake_Y(float _size_Y)
    {
        sin_Y = Mathf.Sin(2 * Mathf.PI * frequency_Y * Time.time);
        cameraObj.transform.position = new Vector3(cameraObj.transform.position.x, cameraDefaultPosition.y + _size_Y * sin_Y, cameraObj.transform.position.z);
    }

    private void _camera_Shake()
    {
        switch (cameraDirection)
        {

            case ShakeType.Low:
                {
                    _shake_X(shakeSize[(int)ShakeType.Low]);
                    _shake_Y(shakeSize[(int)ShakeType.Low]);
                    break;
                }
            case ShakeType.medium:
                {
                    _shake_X(shakeSize[(int)ShakeType.medium]);
                    _shake_Y(shakeSize[(int)ShakeType.medium]);
                    break;
                }
            case ShakeType.high:
                {
                    _shake_X(shakeSize[(int)ShakeType.high]);
                    _shake_Y(shakeSize[(int)ShakeType.high]);
                    break;
                }
            case ShakeType.microvibration:
                {
                    _shake_X(shakeSize[(int)ShakeType.microvibration]);
                    _shake_Y(shakeSize[(int)ShakeType.microvibration]);
                    break;
                }
        }
    }

    // 値等のセット
    public void _Boot_CameraShake(float _setTime, ShakeType _type, float _time_X, float _time_Y)
    {
        shakeFlag = true;
        shakeCount = _setTime;
        cameraDirection = _type;
        cameraDefaultPosition = cameraObj.transform.position;
        frequency_X = 1f / _time_X;
        frequency_Y = 1f / _time_Y;
    }
}
