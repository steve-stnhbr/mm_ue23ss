using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampForce : MonoBehaviour
{
    [SerializeField] Vector3 force;
    private void OnTriggerStay(Collider other)
    {
        MagneticSphereBehaviour bahaviour = other.GetComponent<MagneticSphereBehaviour>();
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if(bahaviour != null && rb != null)
        {
            rb.AddForce(force, ForceMode.Force);
        }
    }
}
