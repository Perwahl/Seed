using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PortraitFace))]
public class FaceEditor : Editor
{

    public PortraitFace faceGen;

    public override void OnInspectorGUI()
    {

        if (GUILayout.Button("Generate Face"))
        {
            faceGen.GenerateFace();            
        }
        if (GUILayout.Button("Sad Face"))
        {           
            faceGen.Sad();
        }
        if (GUILayout.Button("Mad Face"))
        {
            faceGen.Mad();
        }
        if (GUILayout.Button("Sleep Face"))
        {
            faceGen.Sleep();
        }
        if (GUILayout.Button("Calm Face"))
        {
            faceGen.Calm();
        }

        DrawDefaultInspector();
        //DrawSettingsEditor(planet.shapeSettings, planet.OnShapeSettingsUpdated, ref planet.shapeSettingsFoldout, ref shapeEditor);
        //DrawSettingsEditor(planet.colourSettings, planet.OnColourSettingsUpdated, ref planet.colourSettingsFoldout, ref colourEditor);
    }

    //void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
    //{
    //    if (settings != null)
    //    {
    //        foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
    //        using (var check = new EditorGUI.ChangeCheckScope())
    //        {
    //            if (foldout)
    //            {
    //                CreateCachedEditor(settings, null, ref editor);
    //                editor.OnInspectorGUI();

    //                if (check.changed)
    //                {
    //                    if (onSettingsUpdated != null)
    //                    {
    //                        onSettingsUpdated();
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    private void OnEnable()
    {
        faceGen = (PortraitFace)target;
    }
}
