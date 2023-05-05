using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_CreateCylinder : WizardSkill
{
    public GameObject boxPrefab;

    public override void OnExecute(GameObject wizard)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cube.transform.position = wizard.transform.position;
    }
}
