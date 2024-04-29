using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJController_Telop : MonoBehaviour
{
    enum Condition
    {
        standby,
        display,
        nondisplay
    };

    [SerializeField, Header("縮小させる子のオブジェクト")] GameObject[] targetObj;

    [SerializeField, Header("scaleの拡大値")] float scaleUpValue;

    [SerializeField, Header("scaleの縮小値")] float scaleDownValue;

    // 子のオブジェクトのトランスフォーム
    [SerializeField, Header("確認用")]
    Transform[] objTrans = new Transform[2];

    // 子のオブジェクトの元のサイズ
    [SerializeField, Header("確認用")]
    Vector3[] objOriginalScale = new Vector3[2];

    Condition telopCondition = Condition.standby;

    // 代入用変数
    Vector3 scale;

    public int colliderNum;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i< transform.childCount; ++i)
        {
            targetObj[i] = transform.GetChild(i).gameObject;
            objTrans[i] = targetObj[i].transform;
            objOriginalScale[i] = objTrans[i].localScale;
            objTrans[i].localScale = new Vector3(0f, 0f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // objBoxCollider.size = new Vector3(5f, 25f, 10f);
        switch (telopCondition)
        {
            case Condition.display:
                _Terop_On();
                break;

            case Condition.nondisplay:
                _Terop_Off();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            telopCondition = Condition.display;
        }

        if(colliderNum == 0)
        {
            GameManager.Instance.NormalAttack = true;
        }
        if (colliderNum == 1)
        {
            GameManager.Instance.Jump = true;
        }
        if (colliderNum == 2)
        {
            GameManager.Instance.Avoid = true;
        }
        if (colliderNum == 3)
        {
            GameManager.Instance.SpecialAttack = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            telopCondition = Condition.nondisplay;
        }
    }

    private void _Terop_On()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            scale = objTrans[i].localScale;
            if (scale.x < objOriginalScale[i].x)
            {
                scale.x += scaleUpValue;
            }
            else
            {
                scale.x = objOriginalScale[i].x;
            }

            if (scale.y < objOriginalScale[i].y)
            {
                scale.y += scaleUpValue;
            }
            else
            {
                scale.y = objOriginalScale[i].y;
            }

            if (scale.z < objOriginalScale[i].z)
            {
                scale.z += scaleUpValue;
            }
            else
            {
                scale.z = objOriginalScale[i].z;
            }
            objTrans[i].localScale = scale;
        }
        if((objTrans[0].localScale == objOriginalScale[0]) && (objTrans[1].localScale == objOriginalScale[1]))
        {
            telopCondition = Condition.standby;
        }
    }

    private void _Terop_Off()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            scale = objTrans[i].localScale;
            if (scale.x > 0f)
            {
                scale.x -= scaleDownValue;
            }
            else
            {
                scale.x = 0f;
            }

            if (scale.y > 0f)
            {
                scale.y -= scaleDownValue;
            }
            else
            {
                scale.y = 0f;
            }

            if (scale.z > 0f)
            {
                scale.z -= scaleDownValue;
            }
            else
            {
                scale.z = 0f;
            }

            objTrans[i].localScale = scale;

        }

        if ((objTrans[0].localScale == new Vector3(0f, 0f, 0f)) && (objTrans[1].localScale == new Vector3(0f, 0f, 0f)))
        {
            telopCondition = Condition.standby;
        }
    }
}
