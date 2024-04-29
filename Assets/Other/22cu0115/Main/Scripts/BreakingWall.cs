using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingWall : MonoBehaviour
{
    Animator animator;
    [SerializeField, Header("Buff")]
    private GameObject effect;
    private void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        // パーティクル生成
        Vector3 EffectPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        effect = Instantiate(effect, EffectPosition, Quaternion.identity);
        effect.SetActive(false);
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(ScoreManager.Instance.perfectComboCnt);
        
        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack04"))
        {
            effect.SetActive(true);
            SoundManager.Instance.Play("Explosion");
            gameObject.SetActive(false);
        }
        
    }
}
