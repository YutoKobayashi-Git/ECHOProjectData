using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerSpecialFire : MonoBehaviour
{
    [SerializeField, Header("必殺ゲージのスライダー")]
    private Slider slider;

    [SerializeField, Header("Buff")]
    private GameObject effect;

    [SerializeField, Header("追うスピード")]
    private float driveSpeed = 8.0f;

    private GameObject[] enemyTarget;      // 敵のトランス
    private GameObject nearestEnemy;        // 一番近い敵の取得       
    private List<float> disList = new List<float>();
    private float min = 100;        // 初期値の設定（ポイント）

    private float time;     // 時間計測
    private Animator animator;      // アニメーター
    private Vector3 _forward = Vector3.forward;     // 前方の基準となるローカル空間ベクトル
    private bool isOnce;
    private bool isOnce2;

    Player player = null;
    private void Start()
    {
        animator = GetComponent<Animator>();

        // パーティクル生成
        // のちに変更
        Vector3 EffectPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        effect = Instantiate(effect, EffectPosition, Quaternion.identity, transform) as GameObject;
        effect.SetActive(false);
        player = GetComponent<Player>();
    }

    private void OnSpecialFire(InputAction.CallbackContext context)
    {
        // チュートリアルをこなすとボタンを押せるように
        if (GameManager.Instance.SpecialAttack != true) return;
        // 押された瞬間だけ
        if (!context.performed) return;
        if (GetNearestEnemy() == null) return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Jump")) return;
        if (ScoreManager.Instance.specialGauge >= 10 && !isOnce2)
        {
            if (player.GetPlayerHp() <= 0) return;
            {
                ScoreManager.Instance.specialFireFrag = true;
                this.gameObject.layer = 6;
                isOnce2 = true;
                ScoreManager.Instance.specialGauge = 0;
                SoundManager.Instance.Play("StockPower");
                animator.SetBool("Run", false);
                animator.SetTrigger("PowerUp");
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // UI適応
        slider.value = ScoreManager.Instance.specialGauge * 0.1f;

        // 技
        if (ScoreManager.Instance.specialFireFrag)
        {
            time += Time.deltaTime;
            SpecialAttack();
        }
        else
        {
            time = 0;
        }

        // 技が溜まったら
        if (ScoreManager.Instance.specialGauge >= 10)
        {
            effect.SetActive(true);
        }

        // デバッグ用
        if (Input.GetKey(KeyCode.C))
        {
            ScoreManager.Instance.specialGauge = 10;
        }
    }

    private void SpecialAttack()
    {
        // 技のフラグが上がったら
        if (time >= 8)
        {
            // 初期化
            effect.SetActive(false);
            this.gameObject.layer = 0;
            ScoreManager.Instance.specialFireFrag = false;
            ScoreManager.Instance.specialGauge = 0;
            ScoreManager.Instance.notesCnt = 0;
            ScoreManager.Instance.perfectComboCnt = 0;
            animator.SetBool("Run", false);
            animator.SetTrigger("null");

            isOnce = false;
            isOnce2 = false;
        }
        else if (time >= 5.5f && !isOnce)
        {
            //Debug.Log("2.5秒立ったぞ");
            isOnce = true;
            animator.SetBool("Run", false);
            // トリガーはJudgeスクリプト
            //anim.SetTrigger("SP_trg");
        }
        else if (time >= 3 && !isOnce)
        {
            //位置調整
            if (enemyTarget != null)
            {
                // 一番近い敵を探す
                if (GetNearestEnemy() == null) return;
                nearestEnemy = GetNearestEnemy();

                var dis = Vector3.Distance(gameObject.transform.position, nearestEnemy.transform.position);
                var dir = nearestEnemy.transform.position - gameObject.transform.position;

                // ターゲットの方向への回転
                var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
                lookAtRotation.x = 0;
                lookAtRotation.z = 0;
                // 回転補正
                var offsetRotation = Quaternion.FromToRotation(_forward, Vector3.forward);

                // 回転補正→ターゲット方向への回転の順に、自身の向きを操作する
                gameObject.transform.rotation = lookAtRotation * offsetRotation;

                //Debug.Log(dis);

                if (dis > 4.4f)
                {
                    transform.position = Vector3.MoveTowards(transform.position,
                        new Vector3(nearestEnemy.transform.position.x,gameObject.transform.position.y,
                        gameObject.transform.position.z), driveSpeed * Time.deltaTime);
                    animator.SetBool("Run", true);
                }
                else
                {
                    animator.SetBool("Run", false);
                }
            }
        }
    }

    private GameObject GetNearestEnemy()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            enemyTarget = GameObject.FindGameObjectsWithTag("Enemy");
            // 敵が全滅したら終了。
            if (enemyTarget.Length == 0)
            {
                return null;
            }
            // 画面上で一番近い敵を探す仕組み
            foreach (GameObject t in enemyTarget)
            {
                float distance = Vector3.Distance(transform.position, t.transform.position);

                disList.Add(distance);

                foreach (float d in disList)
                {
                    if (d < min)
                    {
                        min = d;

                        nearestEnemy = t;
                    }
                }
            }

            // 初期値に戻す（重要ポイント）
            min = 100;

            // リストも初期化する（ポイント）
            disList = new List<float>();

            return nearestEnemy;
        }
        return null;
    }

       
}
