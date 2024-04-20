using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public class EquipmentItem : Item
{
    public enum EquipmentSlot
    {
        Head,
        Torso,
        Legs,
        Feet,
        Accessory
    }

    public EquipmentSlot Slot;

    bool isRemovable = true;

    private void OnEnable()
    {
        stackable = false;
    }

    public abstract class EquippedEffect : ScriptableObject
    {
        public string Description;
        //return true if could be used, false otherwise.
        public abstract void Equipped(CharacterData user);
        public abstract void Removed(CharacterData user);

        public virtual string GetDescription()
        {
            return Description;
        }
    }

    public List<EquippedEffect> EquippedEffects;

    public EquipmentSlot GetSlotType() { return Slot; }

    public override bool UsedBy(CharacterData user)
    {
        //var userStat = user.Stats.stats;

        //if (userStat.agility < MinimumAgility
        //    || userStat.strength < MinimumStrength
        //    || userStat.armor < MinimumArmor
        //    || userStat.intelligence < MinimumIntelligence)
        //{
        //    return false;
        //}

        user.Equipment.Equip(this);

        return true;
    }

    public virtual void EquippedBy(CharacterData data)
    {
        foreach (var eff in EquippedEffects)
        {
            eff.Equipped(data);
        }
    }

    public virtual void UnequippedBy(CharacterData data)
    {
        foreach (var eff in EquippedEffects)
        {
            eff.Removed(data);
        }
    }

    //public enum SlotType
    //{
    //    Bracer,
    //    Necklace,
    //    Ring1,
    //    Ring2
    //}
}

#if UNITY_EDITOR
[CustomEditor(typeof(EquipmentItem))]
public class EquippableItemEditor : Editor
{
    EquipmentItem m_Target;

    ItemEditor m_ItemEditor;

    SerializedProperty m_SlotProperty;

    List<string> m_AvailableEquipEffectType;
    SerializedProperty m_EquippedEffectListProperty;

    void OnEnable()
    {
        m_Target = target as EquipmentItem;
        m_EquippedEffectListProperty = serializedObject.FindProperty(nameof(EquipmentItem.EquippedEffects));

        m_SlotProperty = serializedObject.FindProperty(nameof(EquipmentItem.Slot));

        //m_MinimumStrengthProperty = serializedObject.FindProperty(nameof(EquipmentItem.MinimumStrength));
        //m_MinimumAgilityProperty = serializedObject.FindProperty(nameof(EquipmentItem.MinimumAgility));
        //m_MinimumIntelligenceProperty = serializedObject.FindProperty(nameof(EquipmentItem.MinimumIntelligence));
        //m_MinimumArmorProperty = serializedObject.FindProperty(nameof(EquipmentItem.MinimumArmor));

        m_ItemEditor = new ItemEditor();
        m_ItemEditor.Init(serializedObject);

        var lookup = typeof(EquipmentItem.EquippedEffect);
        m_AvailableEquipEffectType = System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(lookup))
            .Select(type => type.Name)
            .ToList();
    }

    public override void OnInspectorGUI()
    {
        m_ItemEditor.GUI();

        EditorGUILayout.PropertyField(m_SlotProperty);

        //EditorGUILayout.PropertyField(m_MinimumStrengthProperty);
        //EditorGUILayout.PropertyField(m_MinimumAgilityProperty);
        //EditorGUILayout.PropertyField(m_MinimumIntelligenceProperty);
        //EditorGUILayout.PropertyField(m_MinimumArmorProperty);

        int choice = EditorGUILayout.Popup("Add new Effect", -1, m_AvailableEquipEffectType.ToArray());

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

            //need to do it twice, first time just nullify the entry, second actually remove it.
            m_EquippedEffectListProperty.DeleteArrayElementAtIndex(toDelete);
            m_EquippedEffectListProperty.DeleteArrayElementAtIndex(toDelete);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif