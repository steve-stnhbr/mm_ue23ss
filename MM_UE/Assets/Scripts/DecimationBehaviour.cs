using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecimationBehaviour : MonoBehaviour
{
    [System.NonSerialized]
    public float decimationSpeed;
    [System.NonSerialized]
    public float cutoff;
    [System.NonSerialized]
    public bool decimated;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (decimated)
        {
            Renderer renderer = GetComponent<Renderer>();

            renderer.material.SetFloat("_CutoffHeight", cutoff);
            cutoff -= (Time.deltaTime) * decimationSpeed;
            if (cutoff < -2)
            {
                Debug.Log("Destroying");
                Destroy(gameObject);
            }
        }
    }
}
