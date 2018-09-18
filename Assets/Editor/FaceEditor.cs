//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(FaceGenerator))]
//public class FaceEditor : Editor {

//    public FaceGenerator faceGen;

//	public override void OnInspectorGUI()
//	{
       
//        if (GUILayout.Button("Generate Face"))
//        {
//            faceGen.GenerateFace();
//        }

//        DrawSettingsEditor(planet.shapeSettings, planet.OnShapeSettingsUpdated, ref planet.shapeSettingsFoldout, ref shapeEditor);
//        //DrawSettingsEditor(planet.colourSettings, planet.OnColourSettingsUpdated, ref planet.colourSettingsFoldout, ref colourEditor);
//	}

//    //void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
//    //{
//    //    if (settings != null)
//    //    {
//    //        foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
//    //        using (var check = new EditorGUI.ChangeCheckScope())
//    //        {
//    //            if (foldout)
//    //            {
//    //                CreateCachedEditor(settings, null, ref editor);
//    //                editor.OnInspectorGUI();

//    //                if (check.changed)
//    //                {
//    //                    if (onSettingsUpdated != null)
//    //                    {
//    //                        onSettingsUpdated();
//    //                    }
//    //                }
//    //            }
//    //        }
//    //    }
//    //}

//	private void OnEnable()
//	{
//        faceGen = (FaceGenerator)target;
//	}
//}
