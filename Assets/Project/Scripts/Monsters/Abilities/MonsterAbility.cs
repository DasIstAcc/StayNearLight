using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

public class MonsterAbility : MonoBehaviour
{
    public CharacterData owner;

    public float radius;


    public void Awake()
    {
        owner = gameObject.GetComponent<CharacterUnit>().m_data;
    }

}

public delegate void MonsterAbilityUse(CharacterData data, MonsterAbility ability, Transform position);
public delegate void MonsterAbilityHit(CharacterData data, MonsterAbility ability, CharacterData character);
public delegate void MonsterAbilityEnd(CharacterData data, MonsterAbility ability, Transform position);
