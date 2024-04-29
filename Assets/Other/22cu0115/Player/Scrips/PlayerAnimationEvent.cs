using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : Player
{
    [SerializeField,Header("剣のコライダー")]
    private Collider boxCollider;

    [SerializeField, Header("右向きスラッシュ")]
    private GameObject[] rightSlashEffect;
    [SerializeField, Header("左向きスラッシュ")]
    private GameObject[] leftSlashEffect;

    private Animator animator;
    private float time;
    private float Idle2TransitionTime = 4;      // Idle2 にいくまでの時間
    private bool isOnce;

    private float playerRotationJude = 90.0f;
    private float effectPositionShift = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Idle02Animation();
    }

    // Idle2
    private void Idle02Animation()
    {
        // 現在のアニメーションが Idle01 だったら
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle01"))
        {
            time += Time.deltaTime;
            if (time >= Idle2TransitionTime && !isOnce)
            {
                isOnce = true;
                Animate(Player.Idle02);
            }
        }
        else
        {
            isOnce = false;
            time = 0;
        }
    }

    // 足音
    private void FootStepsEvent(string name)
    {
        if(name == "Player")
        SoundManager.Instance.Play("FootSteps");
    }
    private void PowerUpAnimEvent()
    {
        ScoreManager.Instance.specialFireFrag = true;
    }
    // Attack04Enter
    private void Attack04Enter()
    {
        SoundManager.Instance.Play("Attack04Enter");
    }
    // Attack04End
    private void Attack04End()
    {
        SoundManager.Instance.Play("Destruction");
    }
    /// <summary>
    /// AnimationEvent : コライダーオン
    /// </summary>
    private void OnAttackCollider(int num)
    {
        for (int i = 0; i < rightSlashEffect.Length; ++i)
        {
            if(num == 3)
            {

                // 右向き
                if(transform.localEulerAngles.y == playerRotationJude)
                {
                    // オブジェクトコピー
                    GameObject obj = rightSlashEffect[num];
                    // 位置調整
                    Vector3 pos = obj.transform.position;
                    pos.x = transform.position.x + effectPositionShift;
                    pos.y = transform.position.y + 3;
                    obj.transform.position = pos;
                    // 生成
                    obj = Instantiate(obj, obj.transform.position,obj.transform.rotation) as GameObject;
                    // 一秒後削除
                    Destroy(obj, 1);
                }
                // 左向き
                else if (transform.localEulerAngles.y == playerRotationJude * 3)
                {
                    // オブジェクトコピー
                    GameObject obj = leftSlashEffect[num];
                    // 位置調整
                    Vector3 pos = obj.transform.position;
                    pos.x = transform.position.x - effectPositionShift;
                    pos.y = transform.position.y + 3;
                    obj.transform.position = pos;
                    // 生成
                    obj = Instantiate(obj, obj.transform.position, obj.transform.rotation) as GameObject;
                    // 一秒後削除
                    Destroy(obj, 1);
                }
            }
            else if(i == num)
            {
                
                //Debug.Log(transform.localEulerAngles.y);

                // 右向き
                if(transform.localEulerAngles.y == playerRotationJude)
                {
                    // オブジェクトコピー
                    GameObject obj = rightSlashEffect[num];
                    // 位置調整
                    Vector3 pos = obj.transform.position;
                    pos.x = transform.position.x + effectPositionShift;
                    pos.y = transform.position.y + 2;
                    obj.transform.position = pos;
                    // 生成
                    obj = Instantiate(obj, obj.transform.position,obj.transform.rotation) as GameObject;
                    // 一秒後削除
                    Destroy(obj, 1);
                }
                // 左向き
                else if (transform.localEulerAngles.y == playerRotationJude * 3)
                {
                    // オブジェクトコピー
                    GameObject obj = leftSlashEffect[num];
                    // 位置調整
                    Vector3 pos = obj.transform.position;
                    pos.x = transform.position.x - effectPositionShift;
                    pos.y = transform.position.y + 2;
                    obj.transform.position = pos;
                    // 生成
                    obj = Instantiate(obj, obj.transform.position, obj.transform.rotation) as GameObject;
                    // 一秒後削除
                    Destroy(obj, 1);
                }
            }
        }
        boxCollider.enabled = true;
    }

    /// <summary>
    /// AnimetionEvent : コライダーオフ
    /// </summary>
    private void OffAttackCollider()
    {
        boxCollider.enabled = false;
    }
}

