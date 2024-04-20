
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements.Experimental;

namespace Characters.AI.Goal
{
    public class MeleeAttackGoal : Goal
    {
        internal CharacterUnit m_owner;
        internal Ability boundAbility;
        private double speedModifier;
        private bool followingTargetEvenIfNotSeen;
        private int ticksUntilNextPathRecalculation;
        private int ticksUntilNextAttack = 0;
        private int attackInterval = 20;
        private long lastCanUseCheck = 0;
        private static long COOLDOWN_BETWEEN_CAN_USE_CHECKS = 20L;
        private float canAttackCooldown = 20;
        private float DEFAULT_ATTACK_RANGE = 3;
        

        public MeleeAttackGoal(CharacterUnit character, double speedModifier, bool followingEvenIfNotSeen, Ability bound)
        {
            m_owner = character;
            this.speedModifier = speedModifier;
            followingTargetEvenIfNotSeen = followingEvenIfNotSeen;
            SetFlags(new() { Goal.Flag.MOVE, Goal.Flag.LOOK });
            boundAbility = bound;
            canAttackCooldown = boundAbility.GetCooldown();
        }

        public override bool CanUse()
        {
            lastCanUseCheck++;
            if (lastCanUseCheck < 20L)
            {
                return false;
            }
            else
            {
                lastCanUseCheck = 0;
                CharacterUnit charObj = m_owner.GetTarget();
                if (charObj == null)
                {
                    return false;
                }
                else if (!charObj.IsAlive())
                {
                    return false;
                }
                else
                {
                    m_owner.GetNavMesh().CalculatePath(charObj.transform.position, m_owner.GetNavMesh().path);
                    if (!m_owner.GetNavMesh().hasPath)
                    {
                        m_owner.GetNavMesh().SetDestination(m_owner.GetNavMesh().pathEndPosition);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public override bool CanContinueToUse()
        {
            CharacterUnit targetEntity = m_owner.GetTarget();
            if (targetEntity == null)
            {
                return false;
            }
            else if (!targetEntity.IsAlive())
            {
                return false;
            }
            else if (!followingTargetEvenIfNotSeen)
            {
                if (((m_owner.GetNavMesh().pathStatus == NavMeshPathStatus.PathInvalid) || (m_owner.GetNavMesh().pathStatus == NavMeshPathStatus.PathPartial)))
                {
                    return false;
                }

                if (!(!m_owner.HasLineOfSight(targetEntity) && m_owner.GetNavMesh().pathStatus == NavMeshPathStatus.PathComplete))
                {
                    m_owner.ResetNoActionTime();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                m_owner.ResetNoActionTime();
                return true;
            }
        }

        public override void Start()
        {
            m_owner.GetNavMesh().speed = (float)speedModifier;
            m_owner.GetNavMesh().SetDestination(m_owner.GetNavMesh().pathEndPosition);
            //character.SetAggressive(true);
            ticksUntilNextPathRecalculation = 0;
        }

        public override void Stop()
        {
            m_owner.GetNavMesh().ResetPath();
        }

        public override bool RequiresUpdateEveryTick()
        {
            return true;
        }

        public override void Tick()
        {
            CharacterUnit targetEntity = m_owner.GetTarget();
            if (targetEntity != null)
            {
                m_owner.gameObject.transform.LookAt(targetEntity.transform);
                double d0 = Math.Pow(Vector3.Distance(m_owner.transform.position, targetEntity.transform.position), 2);
                ticksUntilNextPathRecalculation = Math.Max(ticksUntilNextPathRecalculation - 1, 0);
                if ((followingTargetEvenIfNotSeen || m_owner.GetSensing().HasLineOfSight(targetEntity)) && ticksUntilNextPathRecalculation <= 0 && (Vector3.Distance(m_owner.transform.position, targetEntity.transform.position) >= boundAbility.GetReachDistance() - 0.2f || m_owner.GetRandom().NextDouble() < 0.05F))
                {
                    ticksUntilNextPathRecalculation = 4 + m_owner.GetRandom().Next(7);
                    
                    m_owner.GetNavMesh().speed = (float)speedModifier;
                    m_owner.GetNavMesh().SetDestination(targetEntity.transform.position);
                    if (!m_owner.GetNavMesh().hasPath)
                    {
                        ticksUntilNextPathRecalculation += 15;
                    }
                    ticksUntilNextPathRecalculation = AdjustedTickDelay(ticksUntilNextPathRecalculation);
                }

                ticksUntilNextAttack = Math.Max(ticksUntilNextAttack - 1, 0);
                CheckAndPerformAttack(targetEntity, d0);
            }
        }

        internal void CheckAndPerformAttack(CharacterUnit target, double distance)
        {
            if (ticksUntilNextAttack <= 0 && Math.Sqrt(distance) < boundAbility.GetReachDistance())
            {
                ResetAttackCooldown();
                m_owner.PerformAbility(new AbilityTarget(target), boundAbility);
            }
        }

        internal void ResetAttackCooldown()
        {
            ticksUntilNextAttack = (int)boundAbility.GetCooldown() * 60;
        }

        internal bool IsTimeToAttack()
        {
            return ticksUntilNextAttack <= 0;
        }

        internal int GetTicksUntilNextAttack()
        {
            return ticksUntilNextAttack;
        }

        internal float GetAttackInterval()
        {
            return canAttackCooldown;
        }


        //internal double getAttackReachSqr(CharacterObject p_25556_)
        //{
        //    return (double)(this.character.getBbWidth() * 2.0F * this.character.getBbWidth() * 2.0F + p_25556_.getBbWidth());
        //}
    }
}
