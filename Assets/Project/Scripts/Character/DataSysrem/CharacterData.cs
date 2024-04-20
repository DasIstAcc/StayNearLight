using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using Unity.VisualScripting;
using UnityEngine;


[Serializable]
public class CharacterData
{
    public string CharacterName;

    public double current_health;

    public InventorySystem Inventory = new InventorySystem();
    public EquipmentSystem Equipment = new EquipmentSystem();

    public AttributeSet attributes;
    public HashSet<string> abilitiesIDs;

    public int avaliableUPPoints; // Upgrade points - spent for attribute upgrades

    public CharacterData(string name)
    {
        attributes = new AttributeSet();
        CharacterName = name;
        current_health = attributes.GetAttribute(Attributes.MaxHealth).GetValue();
    }

    //public CharacterData(string name, int hitPoints, int str, int agi, int con, int _int, int baseDamage)
    //{
    //    Name = name;
    //    attributes = new AttributeSet();
    //    attributes.SetAttributeBaseValue(Attributes.MaxHealth, hitPoints);
    //    attributes.SetAttributeBaseValue(Attributes.Strength, str);
    //    attributes.SetAttributeBaseValue(Attributes.Agility, agi);
    //    attributes.SetAttributeBaseValue(Attributes.Constitution, con);
    //    attributes.SetAttributeBaseValue(Attributes.Intelligence, _int);
    //    attributes.SetAttributeBaseValue(Attributes.BaseDamage, baseDamage);
    //    attributes.SetAttributeBaseValue(Attributes.FollowRange, 100);
    //    current_health = attributes.GetAttribute(Attributes.MaxHealth).GetValue();
    //}
    public void Init()
    {
        Inventory.Init(this);
        Equipment.Init(this);
    }

    public void Setup(string name, int hitPoints, int str, int agi, int con, int _int, int baseDamage)
    {
        CharacterName = name;
        attributes = new AttributeSet();
        attributes.SetAttributeBaseValue(Attributes.MaxHealth, hitPoints);
        attributes.SetAttributeBaseValue(Attributes.Strength, str);
        attributes.SetAttributeBaseValue(Attributes.Agility, agi);
        attributes.SetAttributeBaseValue(Attributes.Constitution, con);
        attributes.SetAttributeBaseValue(Attributes.Intelligence, _int);
        attributes.SetAttributeBaseValue(Attributes.BaseDamage, baseDamage);
        attributes.SetAttributeBaseValue(Attributes.FollowRange, 100);
        current_health = attributes.GetAttribute(Attributes.MaxHealth).GetValue();

    }

    public void LoadFrom(CharacterData data)
    {
        CharacterName = data.CharacterName;
        
    }

    public bool HurtDirectly(int amount)
    {
        if (current_health - amount > 0)
        {
            current_health -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public int CalculateArmor()
    {
        int result = (int)attributes.GetAttribute(Attributes.Armor).GetValue();

        int bonus_from_agi = (int)attributes.GetAttribute(Attributes.Agility).GetValue() / (int)attributes.GetAttribute(Attributes.AgilityToArmor).GetValue();

        if (bonus_from_agi > 0) result += bonus_from_agi;

        return result;
    }

    public int CalculateMagicArmor()
    {
        int result = (int)attributes.GetAttribute(Attributes.MagicArmor).GetValue();

        int bonus_from_int = (int)attributes.GetAttribute(Attributes.Intelligence).GetValue() / (int)attributes.GetAttribute(Attributes.IntelligenceToArmor).GetValue();

        if (bonus_from_int > 0) result += bonus_from_int;

        return result;
    }


    #region LevellingRelated

    public bool IsUpgradeAvaliable()
    {
        return avaliableUPPoints > 0;
    }

    public void UpgradeAttribute(Attribute attribute)
    {
        if (avaliableUPPoints > 0)
        {
            avaliableUPPoints--;
            attributes.ModifyAttributePermanently(attribute, new AttributeModifier("Upgrade bonus", 1, AttributeModifier.Operation.ADDITION));
        }
    }


    #endregion
}

//[Serializable]
//public class Stat
//{
//    public string Name;
//    public int value;
//    public int maxValue;

//}

//public class HitPoints : Stat
//{
//    public HitPoints(int amount)
//    {
//        maxValue = amount;
//        value = maxValue;
//    }

//    public bool CanTakeDamage(int amount)
//    {
//        if (value - amount <= 0)
//        {
//            return false;
//        }
//        else
//        {
//            return true;
//        }
//    }

//    public void TakeDamage(int amount)
//    {
//        value = value - amount;
//    }
//}
//public class Strenght : Stat
//{

//}

//public class Agility : Stat
//{

//}
//public class Constitution : Stat
//{

//}
//public class Intelligence : Stat
//{

//}
//public class BaseDamage : Stat
//{

//}
//public class Armor : Stat
//{

//}
//public class MagicArmor : Stat
//{

//}

//public enum StatNames
//{
//    Strenght,
//    Agility,
//    Constitution,
//    Intelligence
//}
