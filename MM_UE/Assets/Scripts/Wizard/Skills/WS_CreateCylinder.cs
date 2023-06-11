using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_CreateCylinder : WizardSkill
{
    public GameObject prefab;
    public float cooldown;

    float currentCooldown;

    public override string skillName
    {
        get { return "Create Box"; }
    }
    public override void OnExecute(GameObject wizard)
    {
        if (currentCooldown <= 0) { 
            GameObject instance = GameObject.Instantiate(prefab);
            instance.transform.position = LevelManager.getCurrentLevel().worldPositionToLevelPosition(wizard.transform.position);
            currentCooldown = cooldown;
        }
    }

    private void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }
}
