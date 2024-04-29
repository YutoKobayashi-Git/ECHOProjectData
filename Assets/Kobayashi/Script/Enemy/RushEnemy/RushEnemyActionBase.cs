using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushEnemyActionBase : CharacterSkill
{
    public RushEnemy rushEnemy;
    public RushEnemyActionBase(RushEnemy rushEnemy)
    {
        this.rushEnemy = rushEnemy;
    }

    public virtual void Excute()
    {

    }
}
