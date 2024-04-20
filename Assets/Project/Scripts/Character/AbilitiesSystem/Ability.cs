using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Ability
{
    protected CharacterUnit owner;
    protected string name;
    protected float default_cooldown;
    protected float reach_distance;
    protected string ablAnimationName;
    
    //??
    protected AbilityTarget targetContainer;

    public Ability(float default_cd)
    {
        default_cooldown = default_cd;
    }

    public abstract void Use(AbilityTarget target);


    public delegate void OnUse(CharacterUnit target);
    public delegate void OnUseEnd(CharacterUnit target);

    public virtual float GetCooldown()
    {
        return default_cooldown;
    }

    public virtual float GetReachDistance()
    {
        return reach_distance;
    }

    public virtual Ability Setup(CharacterUnit new_owner)
    {
        SetOwner(new_owner);
        return this;
    }

    public string GetAnimation()
    {
        return ablAnimationName;
    }

    public void SetOwner(CharacterUnit new_owner)
    {
        owner = new_owner;
    }
}
