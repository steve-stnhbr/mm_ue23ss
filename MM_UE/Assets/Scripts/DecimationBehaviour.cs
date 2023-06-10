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
    [System.NonSerialized]
    public short framesToDestroy;

    bool toDestroy;


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
                toDestroy = true;
            }
        }
        if (toDestroy)
        {
            transform.position = new Vector3(-100, -100, -100);
            GetComponent<BoxCollider>().size = Vector3.zero;
            Debug.Log("Planning to destroy " + Time.time);
            framesToDestroy--;
            if (framesToDestroy < 0)
            {
                Debug.Log("Destroying " + Time.time);
                Destroy(gameObject);
            }
        }
    }
}
