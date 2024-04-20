using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContactZone : MonoBehaviour
{

    [SerializeField]
    private CharacterUnit owner;

    public void Setup(CharacterUnit owner)
    {
        this.owner = owner;
        gameObject.AddComponent<BoxCollider>();
        gameObject.GetComponent<BoxCollider>().size = GetComponentInChildren<BoxCollider>().size;
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterUnit target = other.GetComponent<CharacterUnit>();

        if (target == null) return;

        //For rework
        owner.m_data.Equipment.Weapon.ApplyAttack(owner, target);
        //target.TakeHit(owner, DamageSource.PHYSICAL, (int)owner.GetAttributeValue(Attributes.BaseDamage) + 2);
        
    }
}
