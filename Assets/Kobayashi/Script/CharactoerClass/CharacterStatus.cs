using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    
    [SerializeField] public int MaxHp;  // Å‘å‚ÌHP
    private int CurrentHp;              // Œ»İ‚ÌHP
    
    private void Start()
    {
        CurrentHp = MaxHp;
    }

    /// <summary>
    /// HP‚Ìæ“¾
    /// </summary>
    /// <returns></returns>
    public int GetHp()
    {
        return CurrentHp;
    }

    /// <summary>
    /// Œ»İ‚ÌHP‚ğŠi”[‚·‚é
    /// </summary>
    /// <param name="Hp"></param>
    public void SetCurrentHp(int Hp)
    {
        
        CurrentHp = Hp;
    }

}
