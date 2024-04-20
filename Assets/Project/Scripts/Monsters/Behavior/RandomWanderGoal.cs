using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

namespace Characters.AI.Goal
{
    public class RandomWanderGoal : Goal
    {
        public static int DEFAULT_INTERVAL = 120;
        internal CharacterUnit owner;
        internal Vector3 goalDestination;
        internal float speedModifier;
        internal int interval;
        internal bool forceTrigger;
        private bool checkNoActionTime;
        private float wanderingRadius = 5;
        private int timeSinceLastAction = 0;

        public RandomWanderGoal(CharacterUnit unit, float speedModifier) : this(unit, speedModifier, 120) { }

        public RandomWanderGoal(CharacterUnit unit, float speedModifier, int interval) : this(unit, speedModifier, interval, true, 5) { }

        public RandomWanderGoal(CharacterUnit unit, float speedModifier, int interval, bool checkNoActionTime, float wanderingRadius)
        {
            owner = unit;
            this.speedModifier = speedModifier;
            this.interval = interval;
            this.checkNoActionTime = checkNoActionTime;
            SetFlags(new() { Goal.Flag.MOVE });
            goalDestination = owner.transform.position;
            this.wanderingRadius = wanderingRadius;
        }

        public override bool CanUse()
        {
            if (!forceTrigger)
            {
                if (checkNoActionTime && owner.GetNoActionTime() >= 600)
                {
                    return false;
                }

                if (owner.GetNavMesh().pathStatus == NavMeshPathStatus.PathInvalid || owner.GetNavMesh().pathStatus == NavMeshPathStatus.PathPartial)
                {
                    return true;
                }

                if (owner.GetNavMesh().pathStatus != NavMeshPathStatus.PathComplete)
                {
                    return false;
                }

                if (owner.GetRandom().Next(ReducedTickDelay(interval)) != 0)
                {
                    return false;
                }
            }

            Vector3 vec3 = GetPosition();
            if (vec3 == null)
            {
                return false;
            }
            else
            {
                goalDestination = vec3;
                forceTrigger = false;
                return true;
            }
        }

        internal Vector3 GetPosition()
        {
            Vector2 rand = UnityEngine.Random.insideUnitCircle;
            return owner.transform.position + new Vector3(rand.x, rand.y) * wanderingRadius;
        }

        public override bool CanContinueToUse()
        {
            return !((owner.GetNavMesh().pathStatus == NavMeshPathStatus.PathComplete) || (owner.GetNavMesh().pathStatus == NavMeshPathStatus.PathInvalid) || (owner.GetNavMesh().pathStatus == NavMeshPathStatus.PathPartial));
        }

        public override void Start()
        {
            owner.ResetNoActionTime();
            timeSinceLastAction = 0;
            owner.GetNavMesh().speed = speedModifier;
            owner.GetNavMesh().SetDestination(goalDestination);
        }

        public override void Stop()
        {
            base.Stop();
        }

        public void Trigger()
        {
            forceTrigger = true;
        }

        public void SetInterval(int interval)
        {
            this.interval = interval;
        }
    }
}
