using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSystem
{
    //TOREMOVE
    //public void testCallback(CharacterData data)
    //{
    //    EffectHandler handler = new EffectHandler(OnEffectApplication);
    //    ApplyStatus(handler, data, data, new Status() { Name = "HOHO", effectTime = 10});
    //}
    //

    //public int GetStatValueByName(CharacterData data, string name)
    //{
    //    int result = -1;
    //    foreach(Stat s in data.Stats)
    //    {
    //        if (s.Name == name) result = s.value;
    //    }
    //    return result;
    //}
    
    /// <summary>
    /// Basic, with default constants
    /// </summary>
    /// <returns></returns>
    public CharacterData CreateCharacter()
    {
        CharacterData data = new CharacterData("Dummy");

        //string[] statNames = Enum.GetNames(typeof(StatNames));
        //Stat[] stats = new Stat[statNames.Length];

        //for (int i = 0; i < statNames.Length; i++)
        //{
        //    stats[i] = new Stat();
        //    stats[i].Name = statNames[i];
        //    stats[i].value = 8;
        //}

        //data.Stats = stats;

        return data;
    }

    //public static void ApplyStatus(EffectHandler handler, CharacterData sender, CharacterData target, Status effet)
    //{
    //    if (handler == null) return;

    //    handler.Invoke(sender, target, effet);
    //}

    //public static void OnEffectApplication(CharacterData sender, CharacterData target, Status effect)
    //{
    //    Debug.Log("Effect " + effect.Name + " applied on " + target.Name + " by " + sender.Name);
    //}
}

//public delegate void EffectHandler(CharacterData sender, CharacterData target, Status effect);