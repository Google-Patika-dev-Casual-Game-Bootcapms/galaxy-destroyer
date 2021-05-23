using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class LevelEditorUnityWindowComponent : EditorWindow
{
    private static LevelEditorUnityWindowComponent window;
    private List<string> _savedLevelNames = new List<string>();
    
    private string NewLevelName = String.Empty;

    [MenuItem("Tools/LevelEditor")]
    private static void CreateWindow()
    {
        window = (LevelEditorUnityWindowComponent)EditorWindow.GetWindow(typeof(LevelEditorUnityWindowComponent)); //create a window
        window.title = "Level Editor";
    }

    private  void OnGUI()
    {
        if (window == null)
        {
            CreateWindow();
            _savedLevelNames = new List<string>();
        }

        NewLevelName = GUI.TextField(new Rect(10, 10, position.width, 20), NewLevelName, 25);

        if (GUI.Button(new Rect(10, 40, position.width, 20), "Save Level"))
        {
            EditorComponent.instance.SaveLevelDataAsJson(NewLevelName);
        }

        if (GUI.Button(new Rect(10, 70, position.width, 20), "Show Saved Levels"))
        {
            _savedLevelNames = new List<string>();
            _savedLevelNames = EditorComponent.instance.GetLevelNames();
        }

        GUILayout.BeginArea(new Rect(10, 110, position.width, position.height));

        if (_savedLevelNames != null)
        {
            for (int i = 0; i < _savedLevelNames.Count; i++)
            {
                if (GUILayout.Button(_savedLevelNames[i]))
                {
                    EditorComponent.instance.LoadLevelDataFromJson(_savedLevelNames[i]);
                }
            }
        }
        
        GUILayout.EndArea();
    }
}
#endif

