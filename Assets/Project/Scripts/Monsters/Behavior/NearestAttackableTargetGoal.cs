using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Characters.AI.Goal
{
    public class NearestAttackableTargetGoal<T> : TargetGoal
    {
        private static int DEFAULT_RANDOM_INTERVAL = 10;

        internal Type targetType = typeof(T);

        internal int randomInterval;

        internal CharacterUnit target;

        internal TargetingConditions targetingConditions;

        public NearestAttackableTargetGoal(CharacterUnit target, bool mustSee): 
            this(target, 10, mustSee, false, null) { }

        public NearestAttackableTargetGoal(CharacterUnit target, bool mustSee, Predicate<CharacterUnit> selector) : 
            this(target, 10, mustSee, false, selector) { }

        public NearestAttackableTargetGoal(CharacterUnit target, bool mustSee, bool mustReach) :
            this(target, 10, mustSee, mustReach, null) { }

        public NearestAttackableTargetGoal(CharacterUnit target, int interval, bool mustSee, bool mustReach, Predicate<CharacterUnit> selector) : base(target, mustSee, mustReach)
        {
            targetType = typeof(T);
            randomInterval = ReducedTickDelay(interval);
            SetFlags(new HashSet<Flag>() { Flag.TARGET });
            targetingConditions = TargetingConditions.ForCombat().SetRange(GetFollowDistance()).SetSelector(selector);
        }

        public override bool CanUse()
        {
            if (randomInterval > 0 && goalOwner.GetRandom().Next(randomInterval) != 0)
            {
                return false;
            }
            else
            {
                FindTarget();
                return target != null;
            }
        }

        internal void FindTarget()
        {
            Collider[] colls = Physics.OverlapSphere(goalOwner.transform.position, (float)GetFollowDistance());

            foreach(var c in colls)
            {
                CharacterUnit unit = c.gameObject.GetComponentInParent<CharacterUnit>();
                if (unit != null && unit != goalOwner && unit.IsAlive() && goalOwner.HasLineOfSight(unit) && (unit is T))
                {
                    var prevTarget = target;
                    target = unit;
                    if (target != prevTarget)
                    {
                        Debug.Log($"{goalOwner.name} found new target ({target.name})");
                        //Debug.Log(goalOwner.GetSensing().HasLineOfSight(target));
                    }
                    return;
                }
            }
        }

        public override void Start()
        {
            goalOwner.SetTarget(target);
            base.Start();
        }

        public void SetTarget(CharacterUnit target)  { this.target = target; }
    }
}
