using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterData : CharacterData
{
    private int aggro;
    private float aggroRange;

    public MonsterData(string name) : base(name)
    {
    }
}
