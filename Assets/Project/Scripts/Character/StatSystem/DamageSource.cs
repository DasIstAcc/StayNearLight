using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DamageSource
{
    public static DamageSource PHYSICAL = (new DamageSource("physical")).IgnoreMagic();
    public static DamageSource MAGICAL = (new DamageSource("magical")).IgnoreArmor();
    public static DamageSource EFFECT = (new DamageSource("effect")).IgnoreMagic().IgnoreArmor();

    private bool bypassArmor;
    private bool bypassMagic;

    internal string strID;
    
    public DamageSource(string ID)
    {
        strID = ID;
    }

    public bool IsBypassArmor()
    {
        return bypassArmor;
    }

    public bool IsBypassMagic()
    {
        return bypassMagic;
    }

    public DamageSource IgnoreArmor()
    {
        bypassArmor = true;
        return this;
    }

    public DamageSource IgnoreMagic()
    {
        bypassMagic = true;
        return this;
    }

}