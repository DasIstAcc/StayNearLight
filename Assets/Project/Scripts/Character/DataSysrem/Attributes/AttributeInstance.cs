using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEditor;

public class AttributeInstance
{
    private Attribute attribute;
    private ConcurrentDictionary<AttributeModifier.Operation, HashSet<AttributeModifier>> modifiersByOperation = new() { };
    private Dictionary<int, AttributeModifier> modifierById = new Dictionary<int, AttributeModifier> { };
    private HashSet<AttributeModifier> permanentModifiers = new HashSet<AttributeModifier>();
    private double baseValue;
    private bool dirty = true;
    private double cachedValue;
    private Action<AttributeInstance> onDirty;

    public AttributeInstance(Attribute attribute, Action<AttributeInstance> onDirty)
    {
        this.attribute = attribute;
        this.onDirty = onDirty;
        baseValue = attribute.GetDefaultValue();
    }

    public Attribute GetAttribute()
    {
        return attribute;
    }

    public double GetBaseValue()
    {
        return baseValue;
    }

    public void SetBaseValue(double val)
    {
        if (val != baseValue)
        {
            baseValue = val;
            SetDirty();
        }
    }

    public HashSet<AttributeModifier> GetModifiers(AttributeModifier.Operation operation)
    {
        return modifiersByOperation.GetOrAdd(operation, (value) => {
            return new HashSet<AttributeModifier>();
        });
    }

    public HashSet<AttributeModifier> GetModifiers()
    {
        return modifierById.Values.ToHashSet();
    }

    public AttributeModifier GetModifier(int ID)
    {
        if (modifierById.ContainsKey(ID))
            return modifierById[ID];
        else return null;
    }

    public bool HasModifier(AttributeModifier attribute)
    {
        return modifierById.ContainsKey(attribute.GetId());
    }

    private void AddModifier(AttributeModifier new_modifier)
    {
        AttributeModifier attributemodifier = null; 
        modifierById.TryGetValue(new_modifier.GetId(), out attributemodifier);
        if (attributemodifier != null)
        {
            throw new ArgumentException("Modifier is already applied on this attribute!");
        }
        else
        {
            GetModifiers(new_modifier.getOperation()).Add(new_modifier);
            SetDirty();
        }
    }

    public void AddTransientModifier(AttributeModifier mod)
    {
        AddModifier(mod);
    }

    public void AddPermanentModifier(AttributeModifier mod)
    {
        AddModifier(mod);
        permanentModifiers.Add(mod);
    }

    protected void SetDirty()
    {
        dirty = true;
        onDirty(this);
    }

    public void RemoveModifier(AttributeModifier modifier)
    {
        GetModifiers(modifier.getOperation()).Remove(modifier);
        modifierById.Remove(modifier.GetId());
        permanentModifiers.Remove(modifier);
        SetDirty();
    }

    public void RemoveModifier(int ID)
    {
        AttributeModifier attributemodifier = GetModifier(ID);
        if (attributemodifier != null)
        {
            RemoveModifier(attributemodifier);
        }

    }

    public bool RemovePermanentModifier(int ID)
    {
        AttributeModifier attributemodifier = GetModifier(ID);
        if (attributemodifier != null && permanentModifiers.Contains(attributemodifier))
        {
            RemoveModifier(attributemodifier);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveModifiers()
    {
        foreach (AttributeModifier attributemodifier in GetModifiers())
        {
            RemoveModifier(attributemodifier);
        }

    }

    public double GetValue()
    {
        if (dirty)
        {
            cachedValue = CalculateValue();
            dirty = false;
        }

        return this.cachedValue;
    }

    private double CalculateValue()
    {
        double d0 = GetBaseValue();

        foreach (var attributemodifier in GetModifiersOrEmpty(AttributeModifier.Operation.ADDITION))
        {
            d0 += attributemodifier.getAmount();
        }

        double d1 = d0;

        foreach (var attributemodifier1 in GetModifiersOrEmpty(AttributeModifier.Operation.MULTIPLY_BASE))
        {
            d1 += d0 * attributemodifier1.getAmount();
        }

        foreach (var attributemodifier2 in GetModifiersOrEmpty(AttributeModifier.Operation.MULTIPLY_TOTAL))
        {
            d1 *= 1.0D + attributemodifier2.getAmount();
        }

        return attribute.SanitizeValue(d1);
    }

    private HashSet<AttributeModifier> GetModifiersOrEmpty(AttributeModifier.Operation operation)
    {
        return modifiersByOperation.GetValueOrDefault(operation, new HashSet<AttributeModifier>());
    }

    public void ReplaceFrom(AttributeInstance instance)
    {
        baseValue = instance.baseValue;
        modifierById.Clear();
        modifierById.AddRange(instance.modifierById);
        permanentModifiers.Clear();
        permanentModifiers.AddRange(instance.permanentModifiers);
        modifiersByOperation.Clear();
        foreach(var mod in instance.modifiersByOperation)
        {
            modifiersByOperation[mod.Key].AddRange(mod.Value);
        }
        SetDirty();
    }

    //public CompoundTag save()
    //{
    //    CompoundTag compoundtag = new CompoundTag();
    //    compoundtag.putString("Name", Registry.ATTRIBUTE.getKey(this.attribute).toString());
    //    compoundtag.putDouble("Base", this.baseValue);
    //    if (!this.permanentModifiers.isEmpty())
    //    {
    //        ListTag listtag = new ListTag();

    //        for (AttributeModifier attributemodifier : this.permanentModifiers)
    //        {
    //            listtag.add(attributemodifier.save());
    //        }

    //        compoundtag.put("Modifiers", listtag);
    //    }

    //    return compoundtag;
    //}

    //public void load(CompoundTag p_22114_)
    //{
    //    this.baseValue = p_22114_.getDouble("Base");
    //    if (p_22114_.contains("Modifiers", 9))
    //    {
    //        ListTag listtag = p_22114_.getList("Modifiers", 10);

    //        for (int i = 0; i < listtag.size(); ++i)
    //        {
    //            AttributeModifier attributemodifier = AttributeModifier.load(listtag.getCompound(i));
    //            if (attributemodifier != null)
    //            {
    //                this.modifierById.put(attributemodifier.getId(), attributemodifier);
    //                this.getModifiers(attributemodifier.getOperation()).add(attributemodifier);
    //                this.permanentModifiers.add(attributemodifier);
    //            }
    //        }
    //    }

    //    this.setDirty();
    //}
}
