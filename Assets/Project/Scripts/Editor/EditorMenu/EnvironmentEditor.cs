using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class EnvironmentEditor
{
    

}

public class CreateBuilding : EditorWindow
{
    string _name;

    Mesh _mesh;
    Material _mat;
    BoxCollider _collider;


    [MenuItem("Light Tools/Environment Creation/Create Building")]
    static void Init()
    {
        CreateBuilding window = (CreateBuilding)GetWindow(typeof(CreateBuilding));
        window.Show();
    }


    private void OnGUI()
    {
        GUILayout.Label("Building Creator", EditorStyles.boldLabel);
        _name = EditorGUILayout.TextField("Hero name ", _name);

        _mesh = (Mesh)EditorGUILayout.ObjectField("Mesh ", _mesh, typeof(Mesh), true);
        _collider = (BoxCollider)EditorGUILayout.ObjectField("Mesh ", _collider, typeof(BoxCollider), true);

        if (_name == "" || _mesh == null || _collider == null) return;

        if (GUILayout.Button("Create"))
        {
            
        }
    }
}