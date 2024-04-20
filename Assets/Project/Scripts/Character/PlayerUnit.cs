using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class PlayerUnit : CharacterUnit
{
    public Ability SelectedMainAbl { get; private set; }
    public Ability SelectedSecondaryAbl { get; private set; }

    [Tooltip("This is player's weapon")]
    public GameObject ActiveWepon;

    [Header("Player's default weapon")]
    public Weapon default_weapon;

    public override void Awake()
    {
        base.Awake();
        m_data = new CharacterData("Player");
        m_data.Setup("Player", 40, 8, 8, 8, 8, 2);
        m_data.Init();
        m_data.avaliableUPPoints = 6;
        
        default_weapon.UsedBy(m_data);
        m_data.Equipment.InitWeapon(default_weapon, m_data);
    }

    public override void Start()
    {
        base.Start();
        abilities.Add(Abilities.SwordSwipe, new AbilityInstance(new SwordSwipe(5, 0.5f).Setup(this)));
        abilities.Add(Abilities.HeavySwordSwipe, new AbilityInstance(new HeavySwordSwipe(15, 1f).Setup(this)));

        SelectMainAbility(Abilities.SwordSwipe);
        SelectSecondaryAbility(Abilities.HeavySwordSwipe);

        //ActiveWepon = Instantiate(ActiveWepon, this.transform);
        //ActiveWepon.transform.position = new Vector3(0, 1, 1);
        ActiveWepon.SetActive(false);

    }

    public void PerformCoroutine(IEnumerator cor)
    {
        StartCoroutine(cor);
    }

    private bool swinging = false;
    private int stackedAttacks = 0;

    public IEnumerator Swing()
    {
        ActiveWepon.SetActive(true);

        stackedAttacks++;

        swinging = true;

        GetComponent<Animator>().SetTrigger("SwordSwing");

        yield return new WaitForSeconds(1f);

        stackedAttacks--;

        if (stackedAttacks == 0)
        {
            ActiveWepon.SetActive(false);
        }
    }

    public void SelectMainAbility(Ability selectable)
    {
        SelectedMainAbl = selectable;
    }

    public void SelectSecondaryAbility(Ability selectable)
    {
        SelectedSecondaryAbl = selectable;
    }

    public override void Death()
    {
        base.Death();
        GetComponent<ThirdPersonController>().DisableMovement();
    }

    protected override void StartUsingAttack()
    {
        base.StartUsingAttack();
        ActiveWepon.GetComponent<WeaponObject>().OnAttackBegin();
    }

    protected override void StopUsingAttack()
    {
        base.StopUsingAttack();
        ActiveWepon.GetComponent<WeaponObject>().OnAttackEnd();
    }
}
