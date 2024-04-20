using static UnityEngine.EventSystems.EventTrigger;
using System;
using System.Collections.Generic;

public class Sensing
{
    private CharacterUnit character;
    private HashSet<int> seen = new HashSet<int>();
    private HashSet<int> unseen = new HashSet<int>();

    public Sensing(CharacterUnit character)
    {
        this.character = character;
    }

    public void Tick()
    {
        seen.Clear();
        unseen.Clear();
    }

    public bool HasLineOfSight(CharacterUnit checked_char)
    {
        int i = checked_char.GetId();
        if (seen.Contains(i))
        {
            return true;
        }
        else if (unseen.Contains(i))
        {
            return false;
        }
        else
        {
            bool flag = character.HasLineOfSight(checked_char);
            if (flag)
            {
                seen.Add(i);
            }
            else
            {
                unseen.Add(i);
            }

            return flag;
        }
    }
}