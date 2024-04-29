using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    
    [SerializeField] public int MaxHp;  // �ő��HP
    private int CurrentHp;              // ���݂�HP
    
    private void Start()
    {
        CurrentHp = MaxHp;
    }

    /// <summary>
    /// HP�̎擾
    /// </summary>
    /// <returns></returns>
    public int GetHp()
    {
        return CurrentHp;
    }

    /// <summary>
    /// ���݂�HP���i�[����
    /// </summary>
    /// <param name="Hp"></param>
    public void SetCurrentHp(int Hp)
    {
        
        CurrentHp = Hp;
    }

}
