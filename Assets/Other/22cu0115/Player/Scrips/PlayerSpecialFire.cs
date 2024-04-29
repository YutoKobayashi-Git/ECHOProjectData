using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerSpecialFire : MonoBehaviour
{
    [SerializeField, Header("�K�E�Q�[�W�̃X���C�_�[")]
    private Slider slider;

    [SerializeField, Header("Buff")]
    private GameObject effect;

    [SerializeField, Header("�ǂ��X�s�[�h")]
    private float driveSpeed = 8.0f;

    private GameObject[] enemyTarget;      // �G�̃g�����X
    private GameObject nearestEnemy;        // ��ԋ߂��G�̎擾       
    private List<float> disList = new List<float>();
    private float min = 100;        // �����l�̐ݒ�i�|�C���g�j

    private float time;     // ���Ԍv��
    private Animator animator;      // �A�j���[�^�[
    private Vector3 _forward = Vector3.forward;     // �O���̊�ƂȂ郍�[�J����ԃx�N�g��
    private bool isOnce;
    private bool isOnce2;

    Player player = null;
    private void Start()
    {
        animator = GetComponent<Animator>();

        // �p�[�e�B�N������
        // �̂��ɕύX
        Vector3 EffectPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        effect = Instantiate(effect, EffectPosition, Quaternion.identity, transform) as GameObject;
        effect.SetActive(false);
        player = GetComponent<Player>();
    }

    private void OnSpecialFire(InputAction.CallbackContext context)
    {
        // �`���[�g���A�������Ȃ��ƃ{�^����������悤��
        if (GameManager.Instance.SpecialAttack != true) return;
        // �����ꂽ�u�Ԃ���
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
        // UI�K��
        slider.value = ScoreManager.Instance.specialGauge * 0.1f;

        // �Z
        if (ScoreManager.Instance.specialFireFrag)
        {
            time += Time.deltaTime;
            SpecialAttack();
        }
        else
        {
            time = 0;
        }

        // �Z�����܂�����
        if (ScoreManager.Instance.specialGauge >= 10)
        {
            effect.SetActive(true);
        }

        // �f�o�b�O�p
        if (Input.GetKey(KeyCode.C))
        {
            ScoreManager.Instance.specialGauge = 10;
        }
    }

    private void SpecialAttack()
    {
        // �Z�̃t���O���オ������
        if (time >= 8)
        {
            // ������
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
            //Debug.Log("2.5�b��������");
            isOnce = true;
            animator.SetBool("Run", false);
            // �g���K�[��Judge�X�N���v�g
            //anim.SetTrigger("SP_trg");
        }
        else if (time >= 3 && !isOnce)
        {
            //�ʒu����
            if (enemyTarget != null)
            {
                // ��ԋ߂��G��T��
                if (GetNearestEnemy() == null) return;
                nearestEnemy = GetNearestEnemy();

                var dis = Vector3.Distance(gameObject.transform.position, nearestEnemy.transform.position);
                var dir = nearestEnemy.transform.position - gameObject.transform.position;

                // �^�[�Q�b�g�̕����ւ̉�]
                var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
                lookAtRotation.x = 0;
                lookAtRotation.z = 0;
                // ��]�␳
                var offsetRotation = Quaternion.FromToRotation(_forward, Vector3.forward);

                // ��]�␳���^�[�Q�b�g�����ւ̉�]�̏��ɁA���g�̌����𑀍삷��
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
            // �G���S�ł�����I���B
            if (enemyTarget.Length == 0)
            {
                return null;
            }
            // ��ʏ�ň�ԋ߂��G��T���d�g��
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

            // �����l�ɖ߂��i�d�v�|�C���g�j
            min = 100;

            // ���X�g������������i�|�C���g�j
            disList = new List<float>();

            return nearestEnemy;
        }
        return null;
    }

       
}
