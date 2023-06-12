using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_CreateCylinder : WizardSkill
{
    public GameObject prefab;
    public float cooldown;
    [SerializeField] float relativeSpawnHeight = -1;

    float currentCooldown;

    public override string skillName
    {
        get { return "Create Box"; }
    }
    public override void OnExecute(GameObject wizard)
    {
        if (currentCooldown <= 0) { 
            GameObject instance = GameObject.Instantiate(prefab);
            Vector3 spawnPosition = LevelManager.getCurrentLevel().worldPositionToLevelPosition(wizard.transform.position);
            instance.transform.position = new Vector3(spawnPosition.x, spawnPosition.y + relativeSpawnHeight, spawnPosition.z);
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
