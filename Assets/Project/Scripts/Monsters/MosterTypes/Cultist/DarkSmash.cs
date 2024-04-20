using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DarkSmash : Ability
{
    private int damage;
    private float radius;

    private ParticleSystem smashParticles;

    public DarkSmash(int damage, float radius, float cooldown) : base(cooldown)
    {
        this.damage = damage;
        this.radius = radius;
    }

    public override Ability Setup(CharacterUnit new_owner)
    {
        base.Setup(new_owner);
        smashParticles = GameObject.Instantiate(Resources.Load<ParticleSystem>("DarkSmashPS"), owner.transform);
        return this;
    }

    public override float GetReachDistance()
    {
        return radius;
    }

    public override void Use(AbilityTarget target)
    {
        smashParticles.Play();
        
        Collider[] colls = Physics.OverlapSphere(owner.transform.position, radius);

        foreach(var col in colls)
        {
            CharacterUnit obj;
            if (obj = col.GetComponentInParent<CharacterUnit>())
            {
                if (!obj.IsAlliedTo(owner) && obj != owner)
                {
                    obj.TakeHit(owner, DamageSource.PHYSICAL, damage);
                }
            }
        }
    }
}