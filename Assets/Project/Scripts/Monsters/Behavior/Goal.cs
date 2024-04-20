using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.AI.Goal
{
    public abstract class Goal
    {
        private HashSet<Flag> flags = new HashSet<Flag>();

        public abstract bool CanUse();

        public virtual bool CanContinueToUse() { return CanUse(); }

        public virtual bool IsInterruptable() { return true; }

        public virtual void Start()
        {

        }

        public virtual void Stop()
        {

        }

        public virtual bool RequiresUpdateEveryTick() { return false; }

        public virtual void Tick()
        {
        }

        public void SetFlags(HashSet<Flag> new_flags)
        {
            flags = new_flags;
        }

        public HashSet<Flag> GetFlags() { return flags; }

        public int AdjustedTickDelay(int new_delay)
        {
            return RequiresUpdateEveryTick() ? new_delay : 0;
        }

        public static int ReducedTickDelay(int delay)
        {
            return Mathf.CeilToInt((float)delay / 2);
        }

        public enum Flag
        {
            MOVE,
            LOOK,
            JUMP,
            TARGET
        }
    }
}

