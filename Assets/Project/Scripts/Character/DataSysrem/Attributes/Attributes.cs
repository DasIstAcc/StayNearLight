using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Attributes
{
    public static Attribute FollowRange = Register("FOLLOW_RANGE", new RangedAttribute("follow_range", 32, 0, 3000));
    public static Attribute MaxHealth = Register("MAX_HEALTH", new RangedAttribute("max_health", 10, 1, 100000));
    public static Attribute Armor = Register("ARMOR", new RangedAttribute("armor", 0, 0, 1000));
    public static Attribute MagicArmor = Register("MAGIC_ARMOR", new RangedAttribute("magic_armor", 0, 0, 1000));
    public static Attribute Strength = Register("STRENGTH", new RangedAttribute("strength", 8, 1, 4000));
    public static Attribute Agility = Register("AGILITY", new RangedAttribute("agility", 8, 1, 4000));
    public static Attribute Constitution = Register("CONSTITUTION", new RangedAttribute("constitution", 8, 1, 4000));
    public static Attribute Intelligence = Register("INTELLIGENCE", new RangedAttribute("intelligence", 8, 1, 4000));
    public static Attribute BaseDamage = Register("BASE_DAMAGE", new RangedAttribute("base_damage", 1, 1, 10000));
    public static Attribute AgilityToArmor = Register("AGILITY_TOUGHNESS", new RangedAttribute("agi_toughness", 12, 1, 40));
    public static Attribute IntelligenceToArmor = Register("INTELLIGENCE_TOUGHNESS", new RangedAttribute("int_toughness", 12, 1, 40));
    public static Attribute VisionRange = Register("VISION_RANGE", new RangedAttribute("vision_range", 45, 10, 300));

    public static Attribute Register(string name, Attribute attribute)
    {
        return Registry.ATTRIBUTES.Register(name, attribute);
    }
}
