using System;
using System.Collections.Generic;

public class AttributeSet
{
    private Dictionary<Attribute, AttributeInstance> attributes;

    public AttributeSet()
    {
        attributes = new Dictionary<Attribute, AttributeInstance>();

        foreach(var a in Registry.ATTRIBUTES.GetAllData())
        {
            attributes[a] = new AttributeInstance(a, OnAttributeModified);
        }
    }

    public void SetAttributeBaseValue(Attribute to_set, double val)
    {
        attributes[to_set].SetBaseValue(val);
    }

    public void ModifyAttribute(Attribute attribute, AttributeModifier modifier)
    {
        attributes[attribute].AddTransientModifier(modifier);
    }

    public void ModifyAttributePermanently(Attribute attribute, AttributeModifier modifier)
    {
        attributes[attribute].AddPermanentModifier(modifier);
    }

    public AttributeInstance GetAttribute(Attribute to_get)
    {
        return attributes[to_get];
    }

    private void OnAttributeModified(AttributeInstance instance)
    {

    }
}
