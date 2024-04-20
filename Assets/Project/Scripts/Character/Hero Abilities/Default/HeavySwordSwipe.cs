using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavySwordSwipe : Ability
{
    private int damage;

    public HeavySwordSwipe(int damage, float default_cd) : base(default_cd)
    {
        this.damage = damage;
    }

    public override void Use(AbilityTarget target)
    {
        
    }
}
