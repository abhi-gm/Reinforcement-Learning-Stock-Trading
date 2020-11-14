//Poylgon Planet - Contact. https://polygonplanet.com/contact/
//Copyright © 2016-2018 Polygon Planet. All rights reserved. https://polygonplanet.com/privacy-policy/
//This source file is subject to Unity Technologies Asset Store Terms of Service. https://unity3d.com/legal/as_terms

#pragma warning disable 0168 //Variable declared, but not used.
#pragma warning disable 0219 //Variable assigned, but not used.
#pragma warning disable 0414 //Private field assigned, but not used.
#pragma warning disable 0649 //Variable asisgned to, and will always have default value.

#if UNITY_EDITOR
using PolygonPlanet.Editor;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StockManager))]
public class StockManagerEditor : Editor
{
    //Variables
    private StockManager myScript;
    private ReorderableList defaultStocks;

    private void OnEnable()
    {
        myScript = (StockManager)target;
        defaultStocks = new ReorderableList(serializedObject.FindProperty("defaultStocks"));
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        defaultStocks.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(myScript);
    }
}
#endif