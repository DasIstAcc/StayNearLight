using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;

public class Weapon : EquipmentItem
{
    public class AttackData
    {
        public CharacterUnit Target => m_Target;
        public CharacterUnit Source => m_Source;

        CharacterUnit m_Target;
        CharacterUnit m_Source;

        Dictionary<DamageSource, int> m_Damages = new Dictionary<DamageSource, int>();

        /// <summary>
        /// Build a new AttackData. All AttackData need a target, but source is optional. If source is null, the
        /// damage is assume to be from a non CharacterData source (elemental effect, environment) and no boost will
        /// be applied to damage (target defense is still taken in account).
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        public AttackData(CharacterUnit target, CharacterUnit source = null)
        {
            m_Target = target;
            m_Source = source;

            m_Damages.Add(DamageSource.PHYSICAL, 0);
            m_Damages.Add(DamageSource.MAGICAL, 0); //?
        }

        /// <summary>
        /// Add an amount of damage given in the given type. The source (if non null, see class documentation for
        /// info) boost will be applied and the target defense will be removed from that amount.
        /// </summary>
        /// <param name="damageType">The type of damage</param>
        /// <param name="amount">The amount of damage</param>
        /// <returns></returns>
        public int AddDamage(DamageSource damageType, int amount)
        {
            int addedAmount = amount;

            //Physical damage are increase by 1% for each point of strength
            if (damageType == DamageSource.PHYSICAL)
            {
                //source can be null when it's elemental or effect damage
                if (m_Source != null)
                    addedAmount += Mathf.FloorToInt(addedAmount * (float)m_Source.m_data.attributes.GetAttribute(Attributes.Strength).GetValue() * 0.01f);

                //each point of armor remove 1 damage, with a minimum of 1 damage
                addedAmount = Mathf.Max(addedAmount - (int)m_Target.m_data.attributes.GetAttribute(Attributes.Armor).GetValue(), 1);
            }

            //we then add boost per damage type. Not this is called elementalBoost, but physical can also be boosted (IN DEVELOPEMENT)
            //if (m_Source != null)
            //    addedAmount += addedAmount * Mathf.FloorToInt(m_Source.Stats.stats.elementalBoosts[(int)damageType] / 100.0f);

            //Then the elemental protection that is a percentage
            //addedAmount -= addedAmount * Mathf.FloorToInt(m_Target.Stats.stats.elementalProtection[(int)damageType] / 100.0f);

            m_Damages[damageType] += addedAmount;

            return addedAmount;
        }

        /// <summary>
        /// Return the current amount of damage of the given type stored in that AttackData. This is the *effective*
        /// amount of damage, boost and defense have already been applied.
        /// </summary>
        /// <param name="damageType">The type of damage</param>
        /// <returns>How much damage of that type is stored in that AttackData</returns>
        public int GetDamage(DamageSource damageType)
        {
            return m_Damages[damageType];
        }

        /// <summary>
        /// Return the total amount of damage across all type stored in that AttackData. This is *effective* damage,
        /// that mean all boost/defense was already applied.
        /// </summary>
        /// <returns>The total amount of damage across all type in that Attack Data</returns>
        public int GetFullDamage()
        {
            int totalDamage = 0;
            foreach (var val in m_Damages)
            {
                totalDamage += val.Value;
            }

            return totalDamage;
        }
    }

    public abstract class WeaponAttackEffect : ScriptableObject
    {
        public string Description;

        //return the amount of physical damage. If no change, just return physicalDamage passed as parameter
        public virtual void OnAttack(CharacterUnit target, CharacterUnit user, ref AttackData data) { }

        //called after all weapon effect where applied, allow to react to the total amount of damage applied
        public virtual void OnPostAttack(CharacterUnit target, CharacterUnit user, AttackData data) { }

        public virtual string GetDescription()
        {
            return Description;
        }
    }


    [System.Serializable]
    public struct Stat
    {
        public float Speed;
        public int MinimumDamage;
        public int MaximumDamage;
        public float MaxRange;
    }

    [Header("Stats")]
    public Stat Stats = new Stat() { Speed = 1.0f, MaximumDamage = 1, MinimumDamage = 1, MaxRange = 1 };

    public List<WeaponAttackEffect> AttackEffects;

    public int ApplyAttack(CharacterUnit attacker, CharacterUnit target)
    {
        AttackData attackData = new AttackData(target, attacker);

        int damage = Random.Range(Stats.MinimumDamage, Stats.MaximumDamage + 1);//Probably need some thought

        attackData.AddDamage(DamageSource.PHYSICAL, damage);

        foreach (var wae in AttackEffects)
            wae.OnAttack(target, attacker, ref attackData);

        target.TakeHit(attacker, attackData);

        foreach (var wae in AttackEffects)
            wae.OnPostAttack(target, attacker, attackData);

        return attackData.GetFullDamage();
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor
{
    Weapon m_Target;

    ItemEditor m_ItemEditor;

    List<string> m_AvailableEquipEffectType;
    SerializedProperty m_EquippedEffectListProperty;

    List<string> m_AvailableWeaponAttackEffectType;
    SerializedProperty m_WeaponAttackEffectListProperty;

    //SerializedProperty m_MinimumStrengthProperty;
    //SerializedProperty m_MinimumAgilityProperty;
    //SerializedProperty m_MinimumIntelligenceProperty;
    //SerializedProperty m_MinimumDefenseProperty;

    SerializedProperty m_WeaponStatProperty;

    [MenuItem("Assets/Create/Item/Weapon", priority = -999)]
    static public void CreateWeapon()
    {
        var newWeapon = CreateInstance<Weapon>();
        newWeapon.Slot = (EquipmentItem.EquipmentSlot)666;

        ProjectWindowUtil.CreateAsset(newWeapon, "weapon.asset");
    }

    void OnEnable()
    {
        m_Target = target as Weapon;
        m_EquippedEffectListProperty = serializedObject.FindProperty(nameof(Weapon.EquippedEffects));
        m_WeaponAttackEffectListProperty = serializedObject.FindProperty(nameof(Weapon.AttackEffects));

        //m_MinimumStrengthProperty = serializedObject.FindProperty(nameof(EquipmentItem.MinimumStrength));
        //m_MinimumAgilityProperty = serializedObject.FindProperty(nameof(EquipmentItem.MinimumAgility));
        //m_MinimumIntelligenceProperty = serializedObject.FindProperty(nameof(EquipmentItem.MinimumIntelligence));
        //m_MinimumDefenseProperty = serializedObject.FindProperty(nameof(EquipmentItem.MinimumArmor));
        m_WeaponStatProperty = serializedObject.FindProperty(nameof(Weapon.Stats));

        m_ItemEditor = new ItemEditor();
        m_ItemEditor.Init(serializedObject);

        var lookup = typeof(EquipmentItem.EquippedEffect);
        m_AvailableEquipEffectType = System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(lookup))
            .Select(type => type.Name)
            .ToList();

        lookup = typeof(Weapon.WeaponAttackEffect);
        m_AvailableWeaponAttackEffectType = System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(lookup))
            .Select(type => type.Name)
            .ToList();
    }

    public override void OnInspectorGUI()
    {
        m_ItemEditor.GUI();

        //EditorGUILayout.PropertyField(m_MinimumStrengthProperty);
        //EditorGUILayout.PropertyField(m_MinimumAgilityProperty);
        //EditorGUILayout.PropertyField(m_MinimumIntelligenceProperty);
        //EditorGUILayout.PropertyField(m_MinimumDefenseProperty);

        //EditorGUILayout.PropertyField(m_WeaponStatProperty, true);
        var child = m_WeaponStatProperty.Copy();
        var depth = child.depth;
        child.NextVisible(true);

        EditorGUILayout.LabelField("Weapon Stats", EditorStyles.boldLabel);
        while (child.depth > depth)
        {
            EditorGUILayout.PropertyField(child, true);
            child.NextVisible(false);
        }

        int choice = EditorGUILayout.Popup("Add new Equipment Effect", -1, m_AvailableEquipEffectType.ToArray());

        if (choice != -1)
        {
            var newInstance = ScriptableObject.CreateInstance(m_AvailableEquipEffectType[choice]);

            AssetDatabase.AddObjectToAsset(newInstance, target);

            m_EquippedEffectListProperty.InsertArrayElementAtIndex(m_EquippedEffectListProperty.arraySize);
            m_EquippedEffectListProperty.GetArrayElementAtIndex(m_EquippedEffectListProperty.arraySize - 1).objectReferenceValue = newInstance;
        }


        Editor ed = null;
        int toDelete = -1;
        for (int i = 0; i < m_EquippedEffectListProperty.arraySize; ++i)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            var item = m_EquippedEffectListProperty.GetArrayElementAtIndex(i);
            SerializedObject obj = new SerializedObject(item.objectReferenceValue);

            Editor.CreateCachedEditor(item.objectReferenceValue, null, ref ed);

            ed.OnInspectorGUI();
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("-", GUILayout.Width(32)))
            {
                toDelete = i;
            }
            EditorGUILayout.EndHorizontal();
        }

        if (toDelete != -1)
        {
            var item = m_EquippedEffectListProperty.GetArrayElementAtIndex(toDelete).objectReferenceValue;
            DestroyImmediate(item, true);

            m_EquippedEffectListProperty.DeleteArrayElementAtIndex(toDelete);
        }

        //attack
        choice = EditorGUILayout.Popup("Add new Weapon Attack Effect", -1, m_AvailableWeaponAttackEffectType.ToArray());

        if (choice != -1)
        {
            var newInstance = ScriptableObject.CreateInstance(m_AvailableWeaponAttackEffectType[choice]);

            AssetDatabase.AddObjectToAsset(newInstance, target);

            m_WeaponAttackEffectListProperty.InsertArrayElementAtIndex(m_WeaponAttackEffectListProperty.arraySize);
            m_WeaponAttackEffectListProperty.GetArrayElementAtIndex(m_WeaponAttackEffectListProperty.arraySize - 1).objectReferenceValue = newInstance;
        }

        toDelete = -1;
        for (int i = 0; i < m_WeaponAttackEffectListProperty.arraySize; ++i)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            var item = m_WeaponAttackEffectListProperty.GetArrayElementAtIndex(i);
            SerializedObject obj = new SerializedObject(item.objectReferenceValue);

            Editor.CreateCachedEditor(item.objectReferenceValue, null, ref ed);

            ed.OnInspectorGUI();
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("-", GUILayout.Width(32)))
            {
                toDelete = i;
            }
            EditorGUILayout.EndHorizontal();
        }

        if (toDelete != -1)
        {
            var item = m_WeaponAttackEffectListProperty.GetArrayElementAtIndex(toDelete).objectReferenceValue;
            DestroyImmediate(item, true);

            //need to do it twice, first time just nullify the entry, second actually remove it.
            m_WeaponAttackEffectListProperty.DeleteArrayElementAtIndex(toDelete);
            m_WeaponAttackEffectListProperty.DeleteArrayElementAtIndex(toDelete);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif