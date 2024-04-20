using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Abilities
{
    //Add new stuff here
    public static Ability DarkSmash = Register("Dark Smash", new DarkSmash(0, 0, 10));
    public static Ability SwordSwipe = Register("Sword Swipe", new SwordSwipe(5, 0.5f));
    public static Ability HeavySwordSwipe = Register("Heavy Sword Swipe", new HeavySwordSwipe(15, 1f));
    public static Ability ClawAttack = Register("Claw Attack", new ClawAttack(5, 1, 2));


    public static Ability Register(string name, Ability ability)
    {
        return Registry.ABILITIES.Register(name, ability);
    }
}
