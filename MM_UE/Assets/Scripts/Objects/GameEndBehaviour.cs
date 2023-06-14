using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class GameEndBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        HumanBasicBehaviour human;
        if (other.gameObject.TryGetComponent(out human) && GetComponentInParent<GoalActivatorSwitch>().state)
        {
            LevelManager.getCurrentLevel().Stop();
        }
    }
}
