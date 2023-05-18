using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_CreateBox : WizardSkill
{
    public GameObject boxPrefab;

    public override string skillName
    {
        get { return "Create floating box"; }
    }
    public override void OnExecute(GameObject wizard)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = wizard.transform.position;
    }
}
