using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Level))]
[ExecuteInEditMode]
public class LevelEditor : Editor
{
    [Tooltip("If set, a groundObject can be added by clicking")]
    public bool editMode;
    [Tooltip("This prefab is instantiated on click")]
    public GameObject prefab;

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        //serializedObject.Update();

        if (editMode)
        {
            if (GUILayout.Button("Disable Editing"))
            {
                editMode = false;
            }
        }
        else
        {
            if (GUILayout.Button("Enable Editing"))
            {
                editMode = true;
            }
        }

        
    }

    public void OnSceneGUI()
    {
        Level level = (Level)target;

        if (editMode)
        {
            if (Event.current.type == EventType.MouseUp)
            {
                Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit hitInfo;


                if (Physics.Raycast(worldRay, out hitInfo))
                {
                    GameObject instance = Instantiate(prefab);
                    instance.transform.position = hitInfo.point;
                    instance.transform.parent = level.transform;

                    EditorUtility.SetDirty(instance);
                }

            }

            Event.current.Use();

        }
    }


}
