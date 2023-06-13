using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_CreateCylinder : WizardSkill
{
    public GameObject prefab;
    public float cooldown;
    [SerializeField] float relativeSpawnHeight = -1;

    public LayerMask checkLayerMask;
    public float checkRadius = 1;

    float currentCooldown;

    public override string skillName
    {
        get { return "Create Box"; }
    }
    public override void OnExecute(GameObject wizard)
    {
        Vector3 spawnPosition = LevelManager.getCurrentLevel().worldPositionToLevelPosition(wizard.transform.position) + new Vector3(0, relativeSpawnHeight, 0);
        if (currentCooldown <= 0 && !Physics.CheckSphere(spawnPosition, checkRadius, checkLayerMask)) { 
            GameObject instance = GameObject.Instantiate(prefab);
            instance.transform.position = spawnPosition;
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
