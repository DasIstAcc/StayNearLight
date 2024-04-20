using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeaponObject : MonoBehaviour
{
    [SerializeField]
    private Weapon m_Weapon;
    [SerializeField]
    private CharacterUnit m_Owner;

    [SerializeField]
    private Transform damage_point_start;
    [SerializeField]
    private Transform damage_point_end;
    [SerializeField]
    private float radius;

    [Header("Layers that could ba attacked")]
    [SerializeField]
    private LayerMask attackableLayer;

    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            Collider[] hits = Physics.OverlapCapsule(damage_point_start.position, damage_point_end.position, radius, attackableLayer);

            foreach (Collider hit in hits)
            {
                if (CheckTarget(hit))
                {
                    //Some interactions
                }
            }
        }
    }

    //Return true if check was successful ???
    public bool CheckTarget(Collider target)
    {
        CharacterUnit targeted = target.GetComponentInParent<CharacterUnit>();
        if (targeted == null) return true; // Skip everything that is not a unit

        if (target.gameObject == m_Owner.gameObject)
        {//Skip weapon owner, but not missing it
            return true;
        }
        if (!targeted.CanBeSeenAsEnemy()) // needed check for alliance
        {//Skip untargetable colliders and missing them
            return false;
        }

        Debug.Log(targeted.name + " gets attacked");
        //migth need some rework
        m_Weapon.ApplyAttack(m_Owner, targeted);

        return true;
    }

    public void OnAttackBegin()
    {
        isAttacking = true;
    }

    public void OnAttackEnd()
    {
        isAttacking = false;
    }
}
