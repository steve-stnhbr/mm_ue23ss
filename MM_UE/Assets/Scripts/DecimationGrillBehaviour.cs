using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecimationGrillBehaviour : MonoBehaviour
{
    public float decimationSpeed;
    public float initialCutoff;
    public short framesToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        decimate(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    private void decimate(GameObject gameObject)
    {
        if (gameObject.layer == LayerMask.NameToLayer("Decimate"))
        {
            DecimationBehaviour decimation = gameObject.GetComponentInChildren<DecimationBehaviour>();
            if (decimation && !decimation.decimated)
            {
                decimation.framesToDestroy = framesToDestroy;
                decimation.decimationSpeed = decimationSpeed;
                decimation.cutoff = initialCutoff;
                decimation.decimated = true;
            }
        }
    }
}
