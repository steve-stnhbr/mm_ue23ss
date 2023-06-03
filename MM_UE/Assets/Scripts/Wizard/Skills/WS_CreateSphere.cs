using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_CreateSphere : WizardSkill
{
    public GameObject boxPrefab;

    public override string skillName
    {
        get { return "Create Sphere"; }
    }

    public override void OnExecute(GameObject wizard)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        cube.transform.position = wizard.transform.position;
    }
}
