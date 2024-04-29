using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackEffect : MonoBehaviour
{
    [SerializeField, Header("�G�t�F�N�g�����ł���܂ł̎���")] float destroyTime;
    [SerializeField, Header("�G�t�F�N�g�̃g�����X�t�H�[��")] RectTransform effectRectTrans;
    [SerializeField, Header("�G�t�F�N�g��image�R���|�[�l���g")] Image effectImage;
    [SerializeField, Header("�G�t�F�N�g�̃X�P�[��")] Vector3 effectScale;
    [SerializeField, Header("�g�傷��l")] float upScaleValue;
    [SerializeField, Header("�����x�p")] float Value;
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
