using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// ƒ{ƒX‚ÌØ‚è‚Â‚¯‚é
/// </summary>
public class SlashWeapon : ActionBase
{
    private readonly string SoundName = "BossSlash";

    public SlashWeapon(BossEnemy boss, WeaponBaseClass weapon) : base(boss)
    {
    }

    public override void Excute()
    {
        SoundManager.Instance.Play(SoundName);
    }
}
