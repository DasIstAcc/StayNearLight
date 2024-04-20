using System;
using System.Numerics;

public class TargetingConditions
{
    public static TargetingConditions DEFAULT = ForCombat();
    private static double MIN_VISIBILITY_DISTANCE_FOR_INVISIBLE_TARGET = 2.0D;
    private bool isCombat;
    private double range = -1.0D;
    private bool checkLineOfSight = true;
    private bool testInvisible = true;
    private Predicate<CharacterUnit> selector;

    private TargetingConditions(bool is_combat)
    {
        this.isCombat = is_combat;
    }

    public static TargetingConditions ForCombat()
    {
        return new TargetingConditions(true);
    }

    public static TargetingConditions ForNonCombat()
    {
        return new TargetingConditions(false);
    }

    public TargetingConditions Copy()
    {
        TargetingConditions targetingconditions = isCombat ? ForCombat() : ForNonCombat();
        targetingconditions.range = range;
        targetingconditions.checkLineOfSight = checkLineOfSight;
        targetingconditions.testInvisible = testInvisible;
        targetingconditions.selector = selector;
        return targetingconditions;
    }

    public TargetingConditions SetRange(double value)
    {
        this.range = value;
        return this;
    }

    public TargetingConditions IgnoreLineOfSight()
    {
        checkLineOfSight = false;
        return this;
    }

    public TargetingConditions IgnoreInvisibilityTesting()
    {
        testInvisible = false;
        return this;
    }

    public TargetingConditions SetSelector(Predicate<CharacterUnit> predicate)
    {
        selector = predicate;
        return this;
    }

    public bool Test(CharacterUnit targeter, CharacterUnit targeted)
    {
        if (targeter == targeted)
        {
            return false;
        }
        else if (!targeted.CanBeSeenByAnyone())
        {
            return false;
        }
        else if (selector != null && !selector(targeted))
        {
            return false;
        }
        else
        {
            if (targeter == null)
            {
                if (isCombat && (!targeted.CanBeSeenAsEnemy()))
                {
                    return false;
                }
            }
            else
            {
                if (isCombat && (!targeter.CanAttack(targeted) || !targeter.CanAttackType(targeted.GetType()) || targeter.IsAlliedTo(targeted)))
                {
                    return false;
                }

                if (range > 0.0D)
                {
                    double d0 = testInvisible ? targeted.GetVisibilityPercent(targeter) : 1.0D;
                    double d1 = Math.Max(range * d0, 2.0D);
                    double d2 = Math.Pow(UnityEngine.Vector3.Distance(targeter.transform.position, targeted.transform.position), 2);
                    if (d2 > d1 * d1)
                    {
                        return false;
                    }
                }

                if (checkLineOfSight && targeter.GetType() == typeof(CharacterUnit)) {
                    CharacterUnit character = (CharacterUnit)targeter;
                    if (!character.GetSensing().HasLineOfSight(targeted))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}