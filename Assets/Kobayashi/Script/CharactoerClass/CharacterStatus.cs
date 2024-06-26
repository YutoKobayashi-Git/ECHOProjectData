using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    
    [SerializeField] public int MaxHp;  // 最大のHP
    private int CurrentHp;              // 現在のHP
    
    private void Start()
    {
        CurrentHp = MaxHp;
    }

    /// <summary>
    /// HPの取得
    /// </summary>
    /// <returns></returns>
    public int GetHp()
    {
        return CurrentHp;
    }

    /// <summary>
    /// 現在のHPを格納する
    /// </summary>
    /// <param name="Hp"></param>
    public void SetCurrentHp(int Hp)
    {
        
        CurrentHp = Hp;
    }

}
