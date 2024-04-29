using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBase : MonoBehaviour , CharacterSkill
{
    public BossEnemy boss;
    public ActionBase(BossEnemy boss)
    {
        this.boss = boss;
    }

    public virtual void Excute()
    {

    }
}
