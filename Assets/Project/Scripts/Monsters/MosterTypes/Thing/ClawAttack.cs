using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class ClawAttack : Ability
{
    private int damage;
    private float radius;

    private ParticleSystem clawParticles;

    public ClawAttack(int damage, float radius, float cooldown) : base(cooldown)
    {
        this.damage = damage;
        this.radius = radius;
    }

    public override Ability Setup(CharacterUnit new_owner)
    {
        base.Setup(new_owner);
        clawParticles = GameObject.Instantiate(Resources.Load<ParticleSystem>("ClawAttackPS"), owner.transform);
        return this;
    }

    public override float GetReachDistance()
    {
        return radius;
    }

    public override void Use(AbilityTarget target)
    {
        clawParticles.Play();

        Collider[] colls = Physics.OverlapSphere(owner.transform.position, radius);

        foreach (var col in colls)
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
