using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class Registry<T>
{
    private static Dictionary<string, T> data_collection = new();


    public T Register(string name, T item)
    {
        data_collection[name] = item;
        return item;
    }

    public T Unregister(string name) 
    {
        data_collection.Remove(name);
        return data_collection[name];
    }

    public HashSet<T> GetAllData()
    {
        return data_collection.Values.ToHashSet();
    }

    public bool Contains(string ID)
    {
        return data_collection.ContainsKey(ID);
    }

    public T this[string s]
    {
        get {
            if (data_collection.ContainsKey(s)) return data_collection[s];
            else return default(T);
        }
    }
    
    
}

public static class Registry
{
    public static Registry<Attribute> ATTRIBUTES = new AttributeRegistry();
    public static Registry<Ability> ABILITIES = new AbilityRegistry();
    public static Registry<Effect> EFFECTS = new EffectRegistry();
    public static Registry<AttributeModifier> ATTRIBUTE_MODIFIERS = new AttributeModifierRegistry();
    // Add new data sets here

    
}

public class AttributeRegistry : Registry<Attribute> { }
public class AbilityRegistry : Registry<Ability> { }
public class EffectRegistry : Registry<Effect> { }
public class AttributeModifierRegistry : Registry<AttributeModifier> { }