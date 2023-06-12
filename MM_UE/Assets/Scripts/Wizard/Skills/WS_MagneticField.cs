using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_MagneticField : WizardSkill
{
    public float cooldown;
    public float effectDistance = 7;
    public override string skillName => "MagneticField";

    bool magnetizes;
    float currentCooldown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCooldown >= 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    public override void OnExecute(GameObject wizard)
    {
        if (currentCooldown < 0)
        {
            GetComponent<Animator>().SetBool("Magnetizes", true);
            currentCooldown = cooldown;
        }
        /* 
        legacy code for instant push aways
        MagneticBehaviour[] magneticBehaviours = Object.FindObjectsByType<MagneticBehaviour>(FindObjectsSortMode.None);
        
        foreach (MagneticBehaviour magnetic in magneticBehaviours)
        {
            PropellMagnetically(magnetic);
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        MagneticBehaviour magnetic;
        if (other.TryGetComponent(out magnetic))
        {
            PropellMagnetically(magnetic);
        }
    }

    private void PropellMagnetically(MagneticBehaviour magnetic)
    {

        Vector3 difference = magnetic.gameObject.transform.position - transform.position;
        if (difference.magnitude > effectDistance)
        {
            return;
        }

        // interpolation function
        float magnitude = difference.sqrMagnitude;
        Vector3 force = difference.normalized * magnetic.magneticStrength;

        magnetic.gameObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
    }
}
