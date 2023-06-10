using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_MagneticField : WizardSkill
{
    public float effectDistance = 7;
    public override string skillName => "MagneticField";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnExecute(GameObject wizard)
    {
        MagneticBehaviour[] magneticBehaviours = Object.FindObjectsByType<MagneticBehaviour>(FindObjectsSortMode.None);
        
        foreach (MagneticBehaviour magnetic in magneticBehaviours)
        {
            Vector3 difference = magnetic.gameObject.transform.position - transform.position;
            Debug.Log("magnet: " + difference.magnitude);
            if (difference.magnitude > effectDistance)
            {
                continue;
            }

            // interpolation function
            float magnitude = difference.sqrMagnitude;
            Vector3 force = difference.normalized * magnetic.magneticStrength * magnitude;

            magnetic.gameObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
        }
    }
}
