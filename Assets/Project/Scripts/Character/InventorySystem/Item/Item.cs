using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public int id;
    public Sprite ItemSprite;
    public string Name;
    public string Description;
    public bool stackable = true;

    public Attribute[] stats;
    public GameObject WorldObjectPrefab;


    public Item(int id, Sprite icon, string name, string description, Attribute[] stats, bool stackable)
    {
        this.id = id;
        this.ItemSprite = icon;
        Name = name;
        Description = description;
        this.stats = stats;
        this.stackable = stackable;
    }

    public Item(int id, Sprite icon, bool stackable)
    {
        this.id = id;
        this.ItemSprite = icon;
        this.stackable = stackable;
    }

    public Item()
    {
        id = -1;
        stackable = false;
    }

    public virtual bool UsedBy(CharacterData data)
    {
        return false;
    }

    public string GetDescription()
    {
        return Description;
    }
}


#if UNITY_EDITOR
public class ItemEditor
{
    SerializedObject m_Target;

    SerializedProperty m_NameProperty;
    SerializedProperty m_IconProperty;
    SerializedProperty m_DescriptionProperty;
    SerializedProperty m_StackableProperty;
    SerializedProperty m_WorldObjectPrefabProperty;

    public void Init(SerializedObject target)
    {
        m_Target = target;

        m_NameProperty = m_Target.FindProperty(nameof(Item.Name));
        m_IconProperty = m_Target.FindProperty(nameof(Item.ItemSprite));
        m_DescriptionProperty = m_Target.FindProperty(nameof(Item.Description));
        m_StackableProperty = m_Target.FindProperty(nameof(Item.stackable));
        m_WorldObjectPrefabProperty = m_Target.FindProperty(nameof(Item.WorldObjectPrefab));
    }

    public void GUI()
    {
        EditorGUILayout.PropertyField(m_IconProperty);
        EditorGUILayout.PropertyField(m_NameProperty);
        EditorGUILayout.PropertyField(m_DescriptionProperty, GUILayout.MinHeight(128));
        EditorGUILayout.PropertyField(m_WorldObjectPrefabProperty);
    }
}
#endif