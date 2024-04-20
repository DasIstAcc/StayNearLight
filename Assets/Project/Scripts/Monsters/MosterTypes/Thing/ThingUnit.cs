using Characters.AI.Goal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingUnit : MonsterUnit
{
    [SerializeField]
    private int attackDamage;
    [SerializeField]
    private int attackRadius;
    [SerializeField]
    private float attackCooldown;
    [SerializeField]
    private float attackMoveSpeed;

    private ClawAttack clawAttackAbility;

    private ThingClawAttackGoal clawAttackGoal;
    private ThingRandomWanderScrool wanderGoal;



    public override void Awake()
    {
        base.Awake();

        clawAttackAbility = new ClawAttack(attackDamage, attackRadius, attackCooldown);
        clawAttackAbility.Setup(this);
        abilities[clawAttackAbility] = new AbilityInstance(clawAttackAbility);
        clawAttackGoal = new ThingClawAttackGoal(this, attackMoveSpeed, false, clawAttackAbility);
        wanderGoal = new ThingRandomWanderScrool(this, attackMoveSpeed, 15, true, 20);

        ApplyGoals();

        m_data.CharacterName = "Thing";
        ModifyAttributePermanently(Attributes.MaxHealth, new AttributeModifier("Max Hp", 50, AttributeModifier.Operation.ADDITION));
        m_data.current_health = m_data.attributes.GetAttribute(Attributes.MaxHealth).GetValue();
    }

    private void ApplyGoals()
    {
        goalSelector.AddGoal(1, clawAttackGoal);
        goalSelector.AddGoal(2, wanderGoal);
        targetSelector.AddGoal(3, new NearestAttackableTargetGoal<PlayerUnit>(this, true));
    }



    private class ThingRandomWanderScrool : RandomWanderGoal
    {
        public ThingRandomWanderScrool(CharacterUnit unit, float speedModifier, int interval, bool checkNoActionTime, float wanderingRadius) : base(unit, speedModifier, interval, checkNoActionTime, wanderingRadius)
        {
        }
    }


    private class ThingClawAttackGoal : MeleeAttackGoal
    {
        public ThingClawAttackGoal(CharacterUnit character, double speedModifier, bool followingEvenIfNotSeen, Ability bound) : base(character, speedModifier, followingEvenIfNotSeen, bound)
        {
        }
    }
}
