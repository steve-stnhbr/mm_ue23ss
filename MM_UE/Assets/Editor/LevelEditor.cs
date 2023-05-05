using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
    }

    public void OnSceneGUI()
    {
        Level level = (Level)target;

        UnityEditor.Handles.ScaleHandle(level.getLevelSize(), Vector3.zero, Quaternion.identity);

    }


}
