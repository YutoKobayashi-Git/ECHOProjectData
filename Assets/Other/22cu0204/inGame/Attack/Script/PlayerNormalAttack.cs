using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerNormalAttack : MonoBehaviour
{
    [SerializeField, Header("����̃I�u�W�F�N�g")] GameObject judgeObject;
    // Start is called before the first frame update
    void Start()
    {
        judgeObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            judgeObject.SetActive(true);
        }
    }

    public void OnNormalAttack(InputAction.CallbackContext context)
    {
        // �����ꂽ�u�Ԃ���
        if (!context.performed) return;

        judgeObject.SetActive(true);


    }
}
