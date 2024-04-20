using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Unity.VisualScripting;

namespace Characters.AI.Goal
{
    public class WrappedGoal : Goal
    {
        private Goal goal;
        private int priority;
        private bool isRunning = false;

        public WrappedGoal(int _priority, Goal _goal)
        {
            priority = _priority;
            goal = _goal;
        }

        public bool CanBeReplacedBy(WrappedGoal new_goal)
        {
            return IsInterruptable() && (new_goal.GetPriority() < GetPriority());
        }

        public override bool CanUse() { return goal.CanUse(); }

        public new bool CanContinueToUse() { return goal.CanContinueToUse(); }

        public new bool IsInterruptable() { return goal.IsInterruptable(); }

        public new void Start()
        {
            if (!isRunning)
            {
                isRunning = true;
                goal.Start();
            }
        }

        public new void Stop()
        {
            if (isRunning)
            {
                isRunning = false;
                goal.Stop();
            }
        }

        public new bool RequiresUpdateEveryTick() { return goal.RequiresUpdateEveryTick(); }

        public new int AdjustedTickDelay(int delay) { return goal.AdjustedTickDelay(delay); }

        public new void Tick() { goal.Tick(); }

        public new void SetFlags(HashSet<Flag> flags) { goal.SetFlags(flags); }

        public new HashSet<Flag> GetFlags() { return goal.GetFlags(); }

        public bool IsRunning() { return isRunning; }

        public int GetPriority() { return priority; }

        public Goal GetGoal() { return goal; }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            else
            {
                return (obj != null && GetType() == obj.GetType()) ? goal.Equals(((WrappedGoal)obj).goal) : false;
            }
        }

        public override int GetHashCode()
        {
            return goal.GetHashCode();
        }
    }
}

