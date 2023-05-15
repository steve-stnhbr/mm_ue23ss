using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_CreateCylinder : WizardSkill
{
    public GameObject prefab;

    public override void OnExecute(GameObject wizard)
    {
        GameObject instance = GameObject.Instantiate(prefab);
        instance.transform.position = LevelManager.getCurrentLevel().worldPositionToLevelPosition(wizard.transform.position);
    }
}
