using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemyActionBase : CharacterSkill
{
    public GunEnemy gunEnemy;
    public GunEnemyActionBase(GunEnemy gunEnemy)
    {
        this.gunEnemy = gunEnemy;
    }

    public virtual void Excute()
    {

    }
}
