using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeModifiers
{
    //public static AttributeModifier STR_FLAT_BONUS = Register("STRENGTH FLAT BONUS", new AttributeModifier("Str flat bonus", 1, AttributeModifier.Operation.ADDITION));


    public static AttributeModifier Register(string name, AttributeModifier attributeMod)
    {
        return Registry.ATTRIBUTE_MODIFIERS.Register(name, attributeMod);
    }
}
