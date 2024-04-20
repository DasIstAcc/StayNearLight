using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


namespace Characters.AI.Goal
{
    public class GoalSelector
    {
        private class EmptyWrappedGoal : WrappedGoal
        {
            public EmptyWrappedGoal(int _priority, Goal _goal) : base(_priority, _goal)
            {
            }

            public new bool IsRunning()
            {
                return false;
            }
        }

        private class EmptyGoal : Goal
        {
            public override bool CanUse()
            {
                return false;
            }
        }

        private static WrappedGoal NO_GOAL = new EmptyWrappedGoal(int.MaxValue, new EmptyGoal());

        private Dictionary<Goal.Flag, WrappedGoal> lockedFlags = new Dictionary<Goal.Flag, WrappedGoal>();

        private HashSet<WrappedGoal> availableGoals = new HashSet<WrappedGoal>();

        private HashSet<Goal.Flag> disabledFlags = new HashSet<Goal.Flag>();

        private int tickCount;

        private int newGoalRate = 3;

        public GoalSelector() { 
        }

        public void AddGoal(int priority, Goal goal)
        {
            availableGoals.Add(new WrappedGoal(priority, goal));
        }

        public void RemoveAllGoals() { availableGoals.Clear(); }

        public void RemoveGoal(Goal to_remove)
        {
            foreach (var g in availableGoals)
            {
                if (g.GetGoal() == to_remove && g.IsRunning())
                {
                    g.Stop();
                    availableGoals.Remove(g);
                    break;
                }
            }
        }

        private static bool GoalContainsAnyFlags(WrappedGoal wrapped, HashSet<Goal.Flag> to_search)
        {
            foreach(var f in wrapped.GetFlags())
            {
                if (to_search.Contains(f)) return true;
            }

            return false;
        }

        private static bool GoalCanBeReplacedForAllFlags(WrappedGoal wrapped, Dictionary<Goal.Flag, WrappedGoal> goalMap)
        {
            foreach(var g in wrapped.GetFlags())
            {
                if (!goalMap.GetValueOrDefault(g, NO_GOAL).CanBeReplacedBy(wrapped))
                {
                    return false;
                }
            }

            return true;
        }

        public void Tick()
        {
            // Message Goals Cleaning

            foreach (var wrappedGoal in availableGoals)
            {
                if (wrappedGoal.IsRunning() && GoalContainsAnyFlags(wrappedGoal, disabledFlags) || !wrappedGoal.CanContinueToUse())
                {
                    //if (wrappedGoal.GetPriority() == 2) Debug.Log(wrappedGoal);
                    wrappedGoal.Stop();
                }
            }

            for(int i = 0; i < lockedFlags.Count; i++)
            {
                if (!lockedFlags.ElementAt(i).Value.IsRunning())
                {
                    lockedFlags.Remove(lockedFlags.ElementAt(i).Key);
                }
            }


            foreach(var wrappedGoal in availableGoals)
            {
                bool canUse = wrappedGoal.CanUse();
                //if (wrappedGoal.GetPriority() == 2) Debug.Log(wrappedGoal + " " + !wrappedGoal.IsRunning() + " " + GoalCanBeReplacedForAllFlags(wrappedGoal, lockedFlags)+ " "+ canUse + " Locked flags count = " + lockedFlags.Count + " can continue to use" + wrappedGoal.CanContinueToUse());
                if (!wrappedGoal.IsRunning() && !GoalContainsAnyFlags(wrappedGoal, disabledFlags) && GoalCanBeReplacedForAllFlags(wrappedGoal, lockedFlags) && canUse)
                {
                    foreach (var flag in wrappedGoal.GetFlags())
                    {
                        WrappedGoal wrappedGoalL1 = lockedFlags.GetValueOrDefault(flag, NO_GOAL);
                        wrappedGoalL1?.Stop();
                        lockedFlags[flag] =  wrappedGoal;
                    }

                    wrappedGoal.Start();
                }
            }

            TickRunningGoals(true);
        }

        public void TickRunningGoals(bool by_force)
        {
            foreach (WrappedGoal wrappedgoal in availableGoals)
            {
                if (wrappedgoal.IsRunning() && (by_force || wrappedgoal.RequiresUpdateEveryTick()))
                {
                    //Debug.Log(wrappedgoal.GetGoal());
                    wrappedgoal.Tick();
                }
            }
        }

        public HashSet<WrappedGoal> GetAvailableGoals() { return availableGoals; }
        
        public HashSet<WrappedGoal> GetRunningGoals() { return (HashSet<WrappedGoal>)availableGoals.Where(x => x.IsRunning()); }

        public void SetNewGoalRate(int new_rate) { newGoalRate = new_rate; }

        public void DisableControlFlag(Goal.Flag flag) { disabledFlags.Add(flag); }

        public void EnableControlFlag(Goal.Flag flag) { disabledFlags.Remove(flag); }

        public void SetControlFlag(Goal.Flag flag, bool enable)
        {
            if (enable)
            {
                EnableControlFlag(flag);
            }
            else
            {
                DisableControlFlag(flag);
            }

        }
    }
}

