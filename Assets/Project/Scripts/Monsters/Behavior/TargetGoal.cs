using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Characters.AI.Goal
{
    public abstract class TargetGoal : Goal
    {
        protected CharacterUnit goalOwner;

        protected bool mustSee;
        protected bool mustReach;

        private int useenTicks;

        protected CharacterUnit targetCharacter;

        protected int unseenMemoryTicks = 60;

        public TargetGoal(CharacterUnit performer, bool _mustSee) : this(performer, _mustSee, false) {  }

        public TargetGoal(CharacterUnit performer, bool _mustSee, bool _mustReach)
        {
            goalOwner = performer;
            mustSee = _mustSee;
            mustReach = _mustReach;
        }

        public new bool CanContinueToUse()
        {
            CharacterUnit character = goalOwner.GetTarget();
            if (character == null) { character = targetCharacter; }

            if (character == null) { return false; }
            else if (!goalOwner.CanAttack(character)) { return  false; }
            

            return false;
        }

        protected double GetFollowDistance() { return goalOwner.GetAttributeValue(Attributes.FollowRange); }
    }
}
