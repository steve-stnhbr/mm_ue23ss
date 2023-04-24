using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardPlayerMovement: MonoBehaviour
{
    public float maxDistance = 30;
    public LayerMask layerMask;
    public float cameraOffset;
    // This value determines how strongly the wizard player is attached to the mouse movement
    public float maxSpeed;
    public float mouseGravitation;

    Rigidbody rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = Input.mousePosition;
        // offset, to make the tracker visible
        position.z = cameraOffset;
        Vector3 screenWorldPos = Camera.main.ScreenToWorldPoint(position);

        // check for nearest collider in line behind mouse position and jump to it
        RaycastHit raycastHit;
        Ray ray = new Ray(screenWorldPos, screenWorldPos-Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)));
        if(Physics.Raycast(ray, out raycastHit, maxDistance, layerMask))
        {
            moveTo(raycastHit.point);
        }
        /*
        else
        {
            transform.position = screenWorldPos;
        }
        */
    }

    void FixedUpdate()
    {
        if (rigidbody.velocity.magnitude > maxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
        }
    }

    void moveTo(Vector3 position)
    {
        float distance = Vector3.Distance(position, transform.position);
        if (distance < .1)
        {
            return;
        }
        Vector3 force = (position - transform.position) * (mouseGravitation * (rigidbody.mass / distance * distance));
        rigidbody.AddForce(force, ForceMode.Acceleration);
    }
}
