using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


public class Effects
{
    public static Effect BONUS_ARMOR = Register("BONUS_ARMOR", new BonusArmor(0));

    public static Effect Register(string name, Effect effect)
    {
        return Registry.EFFECTS.Register(name, effect);
    }
}