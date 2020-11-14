//Poylgon Planet - Contact. https://polygonplanet.com/contact/
//Copyright © 2016-2018 Polygon Planet. All rights reserved. https://polygonplanet.com/privacy-policy/
//This source file is subject to Unity Technologies Asset Store Terms of Service. https://unity3d.com/legal/as_terms

#pragma warning disable 0168 //Variable declared, but not used.
#pragma warning disable 0219 //Variable assigned, but not used.
#pragma warning disable 0414 //Private field assigned, but not used.
#pragma warning disable 0649 //Variable asisgned to, and will always have default value.

#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SaveLoadManager))]
public class SaveLoadManagerEditor : Editor
{
    //Object Data
    private SaveLoadManager script;

    private void OnEnable()
    {
        script = (SaveLoadManager)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        Utilities();

        serializedObject.ApplyModifiedProperties();
        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(script);
    }

    private void Utilities()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Utilities", EditorStyles.boldLabel);

        if (GUILayout.Button("Open Save Path"))
            OpenSavePath();

        if (GUILayout.Button("Delete Save File"))
            DeleteSave();
    }

    private void OpenSavePath()
    {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }

    private void DeleteSave()
    {
        try
        {
            if (EditorUtility.DisplayDialog("Delete save file?", "Are you sure you want to delete the save file?", "Yes", "Cancel"))
            {
                DirectoryInfo dataDir = new DirectoryInfo(Application.persistentDataPath);
                dataDir.Delete(true);
            }
        }
        catch (Exception exception)
        {
            Debug.Log(exception);
        }
    }
}
#endif