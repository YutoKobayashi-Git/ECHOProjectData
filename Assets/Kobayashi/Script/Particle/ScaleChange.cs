using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChange : MonoBehaviour
{
    float ChangeScale = 0.0f;

    private readonly static float ChangePulsNum = 0.1f;

    void Update()
    {

        ChangeScale += (Time.deltaTime / ChangePulsNum);

        gameObject.transform.localScale = new Vector3(ChangeScale, ChangeScale, ChangeScale);
    }



}
