using Characters.AI.Goal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterUnit : MonoBehaviour
{
    public CharacterData m_data;
    //private HashSet<Ability> abilities;
    public Dictionary<Ability, AbilityInstance> abilities;

    protected int ID;
    protected System.Random random;
    
    bool isInvulnerable = false;
    bool isAlive = true;
    
    #region AiDetectionSystemRelated

    public GoalSelector goalSelector;
    public GoalSelector targetSelector;

    protected CharacterUnit target;
    protected NavMeshAgent navAgent;
    protected int noActionTime = 10;
    protected Sensing sensing;
    protected float follow_distance;

    public List<Transform> visiblePoints = new List<Transform>();
    protected float visionAngle = 60;
    protected float visionDistance = 40;

    [SerializeField]
    protected Transform unitEyes;
    [SerializeField]
    protected LayerMask visibleMask;


    #endregion


    #region EventsAndDelegates

    public OnHealthChanged e_onHealthChanged;


    #endregion

    public double GetAttributeValue(Attribute attribute)
    {
        return m_data.attributes.GetAttribute(attribute).GetValue();
    }

    public void ModifyAttribute(Attribute attribute, AttributeModifier modifier)
    {
        m_data.attributes.ModifyAttribute(attribute, modifier);
    }

    public void ModifyAttributePermanently(Attribute attribute, AttributeModifier modifier)
    {
        m_data.attributes.ModifyAttributePermanently(attribute, modifier);
    }

    public virtual CharacterUnit GetTarget() { return target; }
    
    public virtual List<T> GetVisibleUnits<T>() where T : CharacterUnit
    {
        List<T> result = new List<T>();

        foreach(T unit in GameManager._unitManager.GetAllUnits())
        {
            if (unit != null && unit != this && unit.enabled && IsVisibleUnit(unit))
            {
                result.Add(unit);
            }
        }

        return result;
    }

    public virtual bool IsVisibleUnit(CharacterUnit target_unit)
    {
        bool result = DetectionSystem.IsVisibleUnit(target_unit, unitEyes, visionAngle, visionDistance, visibleMask) && !target_unit.IsInvulnerable();

        return result;
    }

    public bool HasLineOfSight(CharacterUnit target_unit)
    {
        return IsVisibleUnit(target_unit);
    }

    public virtual void Awake()
    {
        m_data = new CharacterData("Default");
        random = new System.Random();
        abilities = new Dictionary<Ability, AbilityInstance>();

    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        ID = GameManager._manager.GetNewID();
        foreach (var unit in GameManager._unitManager.GetAllUnits())
        {
            visiblePoints.Add(unit.transform);
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (invulFrames > 0) invulFrames--;
    }

    public Sensing GetSensing()
    {
        return sensing;
    }

    public int GetId()
    {
        return ID;
    }

    public System.Random GetRandom() { return random; }

    public double GetVisibilityPercent(CharacterUnit target)
    {
        double d0 = 1.0D;
        //if (this.isDiscrete())
        //{
        //    d0 *= 0.8D;
        //}

        //if (this.isInvisible())
        //{
        //    float f = this.getArmorCoverPercentage();
        //    if (f < 0.1F)
        //    {
        //        f = 0.1F;
        //    }

        //    d0 *= 0.7D * (double)f;
        //}

        //if (target != null)
        //{
        //    ItemStack itemstack = this.getItemBySlot(EquipmentSlot.HEAD);
        //    EntityType <?> entitytype = target.getType();
        //    if (entitytype == EntityType.SKELETON && itemstack.is (Items.SKELETON_SKULL) || entitytype == EntityType.ZOMBIE && itemstack.is (Items.ZOMBIE_HEAD) || entitytype == EntityType.CREEPER && itemstack.is (Items.CREEPER_HEAD))
        //    {
        //        d0 *= 0.5D;
        //    }
        //}

        return d0;
    }

    public bool CanBeSeenAsEnemy()
    {
        return !isInvulnerable && CanBeSeenByAnyone();
    }

    public bool CanBeSeenByAnyone()
    {
        return isAlive;
    }

    public bool CanAttack(CharacterUnit target)
    {
        return !target.IsInvulnerable();
    }

    public bool CanAttackType(Type target)
    {
        return true;
    }

    public bool IsAlliedTo(CharacterUnit character)
    {
        return false;
    }

    public void SetTarget(CharacterUnit target)
    {
        this.target = target;
    }

    public bool IsAlive()
    {
        return isAlive;
    }
    public bool IsInvulnerable()
    {
        return isInvulnerable;
    }

    public NavMeshAgent GetNavMesh()
    {
        return navAgent;
    }

    public void PerformAbility(AbilityTarget target, Ability ability)
    {
        if (abilities.ContainsKey(ability))
        {
            abilities[ability].GetAbility().Use(target);
        }
    }

    private float invulFrames = 0;
    private float iFramesCount = 30;

    public void TakeHit(CharacterUnit sender, Weapon.AttackData attackData)
    {
        TryTakeDamage(attackData.GetFullDamage());
    }

    public void TakeHit(CharacterUnit sender, DamageSource damageSource, int amount)
    {
        int finalDamage = amount;

        if (!damageSource.IsBypassArmor())
        {
            finalDamage -= m_data.CalculateArmor();
            if (finalDamage < 0) finalDamage = 0;
        }
        if (!damageSource.IsBypassMagic())
        {
            finalDamage -= m_data.CalculateMagicArmor();
            if (finalDamage < 0) finalDamage = 0;
        }

        TryTakeDamage(finalDamage);
    }

    private bool TryTakeDamage(int amount)
    {
        if (IsAlive() && !IsInvulnerable())
        {
            isInvulnerable = true;
            invulFrames += iFramesCount;
            if (!m_data.HurtDirectly(amount)) Death();
            if (e_onHealthChanged != null) e_onHealthChanged.Invoke(amount, this);
            StartCoroutine(InvulReset());
            return true;
        }
        else
        {
            return false;
        }
    }

    public delegate void OnHealthChanged(float amount, CharacterUnit unit);

    private IEnumerator InvulReset()
    {
        yield return new WaitForSeconds(0.3f);
        isInvulnerable = false;
    }

    public virtual void Death()
    {
        if (GetComponent<Animator>() != null) GetComponent<Animator>().SetBool("Dead", true);

        m_data.current_health = 0;
        if (e_onHealthChanged != null) e_onHealthChanged.Invoke(0, this);
        isAlive = false;
        //navAgent.isStopped = true;
    }

    //public bool TryApplyEffect(CharacterUnit target, Effect effect)
    //{
    //    return false;
    //}

    public int GetNoActionTime()
    {
        return noActionTime;
    }

    public void ResetNoActionTime()
    {
        noActionTime = 0;
    }

    //Called by an Animation event
    protected virtual void StartUsingAttack()
    {
        
    }

    //Called by an Animation event
    protected virtual void StopUsingAttack()
    {

    }

}

//RaycastHit hit;
//Physics.Raycast(transform.position + transform.forward * 2 + Vector3.up * 1.5f, transform.forward, out hit, (float)m_data.attributes.GetAttribute(Attributes.VisionRange).GetDefaultValue());


//if (hit.collider == null) return false;
//CharacterUnit unit = hit.collider.gameObject.GetComponentInParent<CharacterUnit>();


//if (unit != null && unit == checked_char)
//{
//    return true;
//}
//else
//{
//    return false;
//}

//public bool TryUseItem(int index)
//{
//    var item = m_data.inventory.GetItem(index);

//    if (item == null) return false;

//    if (item is EquippableItem)
//    {
//        var curr_item = m_data.equipment.GetItem(((EquippableItem)item).GetSlotType());
//        if (curr_item != null)
//        {
//            if (m_data.equipment.EquipItem((EquippableItem)item))
//            {
//                m_data.inventory.AddNewItem(curr_item);
//                m_data.inventory.RemoveItem(item);
//                return true;
//            }
//        }

//        if (m_data.equipment.EquipItem((EquippableItem)item))
//        {
//            m_data.inventory.RemoveItem(item);
//            return true;
//        }
//    }

//    return false;
//}

//public bool TryRemoveItem(EquippableItem.SlotType type)
//{
//    var item = m_data.equipment.GetItem(type);
//    if (item == null) return false;

//    if (m_data.inventory.AddNewItem(item))
//    {
//        return m_data.equipment.RemoveItemAt(type);
//    }
//    return false;
//}