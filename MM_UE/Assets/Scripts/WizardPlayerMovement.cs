using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardPlayerMovement: MonoBehaviour
{
    public float maxDistance = 30;
    public LayerMask layerMask;
    public float cameraOffset;
    // This value describes the maximum speed the wizard player can have while moving to the mouse
    public float maxSpeed;
    // This value determines how strongly the wizard player is attached to the mouse movement
    public float mouseGravitation;
    // This value describes how far away from the mouse no more force is exerted on the wizard player 
    public float maxDistanceToMouse;

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
        Vector3 levelPos = Camera.main.ScreenToWorldPoint(position);

        // check for nearest collider in line behind mouse position and jump to it
        RaycastHit raycastHit;
        Ray ray = new Ray(levelPos, levelPos - Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)));
        if(Physics.Raycast(ray, out raycastHit, maxDistance, layerMask))
        {
            moveTo(Level.getCurrentLevel().worldPositionToLevelPosition(raycastHit.point));
        }
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
        if (distance < maxDistanceToMouse)
        {
            return;
        }
        Vector3 force = (position - transform.position) * (mouseGravitation * (rigidbody.mass / distance * distance));
        rigidbody.AddForce(force, ForceMode.Acceleration);
    }
}
