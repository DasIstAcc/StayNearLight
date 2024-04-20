using Characters.AI.Goal;
using UnityEngine;

public class Cultist : MonsterUnit
{
    [SerializeField]
    private int darkSmashDamage;
    [SerializeField]
    private int darkSmashRadius;
    [SerializeField]
    private float darkSmashCooldown;
    [SerializeField]
    private float smashMoveSpeed;

    private DarkSmash darkSmashAbility;

    private DarkSmashAttackGoal smashAttackGoal;
    private CultistWanderGoal wanderGoal;

    


    public override void Awake()
    {
        base.Awake();

        darkSmashAbility = new DarkSmash(darkSmashDamage, darkSmashRadius, darkSmashCooldown);
        darkSmashAbility.Setup(this);
        abilities[darkSmashAbility] = new AbilityInstance(darkSmashAbility);
        smashAttackGoal = new DarkSmashAttackGoal(this, smashMoveSpeed, false, darkSmashAbility);
        wanderGoal = new CultistWanderGoal(this, smashMoveSpeed, 15, true, 20);

        ApplyGoals();
    }

    private void ApplyGoals()
    {
        goalSelector.AddGoal(1, smashAttackGoal);
        goalSelector.AddGoal(2, wanderGoal);
        targetSelector.AddGoal(3, new NearestAttackableTargetGoal<Cultist>(this, true));
    }

    public override void Update()
    {
        base.Update();
        
    }




    private class CultistWanderGoal : RandomWanderGoal
    {
        public CultistWanderGoal(CharacterUnit unit, float speedModifier, int interval, bool checkNoAction, float wander_radius) : base(unit, speedModifier, interval, checkNoAction, wander_radius)
        {
        }
    }

    private class DarkSmashAttackGoal : MeleeAttackGoal
    {
        public DarkSmashAttackGoal(CharacterUnit character, double speedModifier, bool followingEvenIfNotSeen, Ability ability) : base(character, speedModifier, followingEvenIfNotSeen, ability)
        {
        }
    }
}
