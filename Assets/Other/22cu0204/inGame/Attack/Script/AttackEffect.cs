using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackEffect : MonoBehaviour
{
    [SerializeField, Header("エフェクトが消滅するまでの時間")] float destroyTime;
    [SerializeField, Header("エフェクトのトランスフォーム")] RectTransform effectRectTrans;
    [SerializeField, Header("エフェクトのimageコンポーネント")] Image effectImage;
    [SerializeField, Header("エフェクトのスケール")] Vector3 effectScale;
    [SerializeField, Header("拡大する値")] float upScaleValue;
    [SerializeField, Header("透明度用")] float Value;
    private float countTime;
    // Start is called before the first frame update
    void Start()
    {
        effectRectTrans = this.gameObject.GetComponent<RectTransform>();
        effectImage = this.gameObject.GetComponent<Image>();
        effectScale = effectRectTrans.localScale;
        effectScale.z = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime;
        if(countTime > 0.01)
        {
            effectScale.x += upScaleValue;
            effectScale.y += upScaleValue;
            effectImage.color = new Color(effectImage.color.r, effectImage.color.g, effectImage.color.b, effectImage.color.a - Value);
            effectRectTrans.localScale = effectScale;
        }
        if (countTime > destroyTime)
        {
            Destroy(this.gameObject);
        }
    }
}
