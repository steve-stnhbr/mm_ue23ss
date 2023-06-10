using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereActivatorBehaviour : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        ActivationBehaviour activation;
        float distance = Vector3.Distance(collision.gameObject.transform.position, GetComponent<Collider>().bounds.center);
        if (collision.gameObject.TryGetComponent(out activation) && distance < .8)
        {
            animator.SetBool("Active", true);
        } 
        else
        {
            animator.SetBool("Active", false);
        }
    }
}
