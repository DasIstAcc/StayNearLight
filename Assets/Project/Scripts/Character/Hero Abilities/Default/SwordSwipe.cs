using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SwordSwipe : Ability
{
    private int damage;

    public SwordSwipe(int damage, float default_cd) : base(default_cd)
    {
        this.damage = damage;
    }

    public override Ability Setup(CharacterUnit new_owner)
    {
        base.Setup(new_owner);

        PlayerUnit unit = (PlayerUnit)owner;

        //unit.ActiveWepon.GetComponent<WeaponObject>().Setup(owner);
        unit.ActiveWepon.SetActive(false);

        return this;
    }

    public override void Use(AbilityTarget target)
    {
        PlayerUnit unit = (PlayerUnit)owner;

        

        unit.PerformCoroutine(unit.Swing());
    }

    
}
