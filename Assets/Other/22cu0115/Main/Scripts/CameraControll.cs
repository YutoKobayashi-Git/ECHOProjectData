using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControll : MonoBehaviour
{
    [SerializeField,Header("MainCamera")]
    private CinemachineVirtualCameraBase vcam1;
    [SerializeField,Header("SubCamera01")]
    private CinemachineVirtualCameraBase vcam2;
    [SerializeField, Header("SubCamera02")]
    private CinemachineVirtualCameraBase vcam3;
    [SerializeField, Header("SubCamera03")]
    private CinemachineVirtualCameraBase vcam4;
    [SerializeField, Header("SubCamera04")]
    private CinemachineVirtualCameraBase vcam5;
    private float time;

    void LateUpdate()
    {
        if(ScoreManager.Instance.specialFireFrag)
        {
            time += Time.deltaTime;

            if(time >= 3)
            {
                vcam5.Priority = 4;
            }
            else if (time >= 2)
            {
                vcam4.Priority = 3;
            }
            else if (time >= 1)
            {
                vcam3.Priority = 2;
            }
            else if(time >= 0)
            {
                vcam1.Priority = 0;
                vcam2.Priority = 1;
            }
        }
        else
        {
            vcam1.Priority = 1;
            vcam2.Priority = 0;
            vcam3.Priority = 0;
            vcam4.Priority = 0;
            vcam5.Priority = 0;
            time = 0;
        }
    }
}