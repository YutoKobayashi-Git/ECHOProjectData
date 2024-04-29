using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : Player
{
    [SerializeField,Header("���̃R���C�_�[")]
    private Collider boxCollider;

    [SerializeField, Header("�E�����X���b�V��")]
    private GameObject[] rightSlashEffect;
    [SerializeField, Header("�������X���b�V��")]
    private GameObject[] leftSlashEffect;

    private Animator animator;
    private float time;
    private float Idle2TransitionTime = 4;      // Idle2 �ɂ����܂ł̎���
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
        // ���݂̃A�j���[�V������ Idle01 ��������
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

    // ����
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
    /// AnimationEvent : �R���C�_�[�I��
    /// </summary>
    private void OnAttackCollider(int num)
    {
        for (int i = 0; i < rightSlashEffect.Length; ++i)
        {
            if(num == 3)
            {

                // �E����
                if(transform.localEulerAngles.y == playerRotationJude)
                {
                    // �I�u�W�F�N�g�R�s�[
                    GameObject obj = rightSlashEffect[num];
                    // �ʒu����
                    Vector3 pos = obj.transform.position;
                    pos.x = transform.position.x + effectPositionShift;
                    pos.y = transform.position.y + 3;
                    obj.transform.position = pos;
                    // ����
                    obj = Instantiate(obj, obj.transform.position,obj.transform.rotation) as GameObject;
                    // ��b��폜
                    Destroy(obj, 1);
                }
                // ������
                else if (transform.localEulerAngles.y == playerRotationJude * 3)
                {
                    // �I�u�W�F�N�g�R�s�[
                    GameObject obj = leftSlashEffect[num];
                    // �ʒu����
                    Vector3 pos = obj.transform.position;
                    pos.x = transform.position.x - effectPositionShift;
                    pos.y = transform.position.y + 3;
                    obj.transform.position = pos;
                    // ����
                    obj = Instantiate(obj, obj.transform.position, obj.transform.rotation) as GameObject;
                    // ��b��폜
                    Destroy(obj, 1);
                }
            }
            else if(i == num)
            {
                
                //Debug.Log(transform.localEulerAngles.y);

                // �E����
                if(transform.localEulerAngles.y == playerRotationJude)
                {
                    // �I�u�W�F�N�g�R�s�[
                    GameObject obj = rightSlashEffect[num];
                    // �ʒu����
                    Vector3 pos = obj.transform.position;
                    pos.x = transform.position.x + effectPositionShift;
                    pos.y = transform.position.y + 2;
                    obj.transform.position = pos;
                    // ����
                    obj = Instantiate(obj, obj.transform.position,obj.transform.rotation) as GameObject;
                    // ��b��폜
                    Destroy(obj, 1);
                }
                // ������
                else if (transform.localEulerAngles.y == playerRotationJude * 3)
                {
                    // �I�u�W�F�N�g�R�s�[
                    GameObject obj = leftSlashEffect[num];
                    // �ʒu����
                    Vector3 pos = obj.transform.position;
                    pos.x = transform.position.x - effectPositionShift;
                    pos.y = transform.position.y + 2;
                    obj.transform.position = pos;
                    // ����
                    obj = Instantiate(obj, obj.transform.position, obj.transform.rotation) as GameObject;
                    // ��b��폜
                    Destroy(obj, 1);
                }
            }
        }
        boxCollider.enabled = true;
    }

    /// <summary>
    /// AnimetionEvent : �R���C�_�[�I�t
    /// </summary>
    private void OffAttackCollider()
    {
        boxCollider.enabled = false;
    }
}

