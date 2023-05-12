using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardPlayerMovement: MonoBehaviour
{
    public float maxDistanceFromScreen = 30;
    public LayerMask layerMask;
    public float cameraOffset;
    // This value describes the maximum speed the wizard player can have while moving to the mouse
    public float maxSpeed;
    // This value determines how strongly the wizard player is attached to the mouse movement
    public float mouseGravitation;
    // This value describes how far away from the mouse no more force is exerted on the wizard player 
    public float maxDistanceToMouse;

    // This value describes the Length of a Voxel Cube
    public float voxelLength = 1f;
    // This value describes the offset of the Voxel Grid
    public Vector3 voxelOffset = new Vector3(0, 0, 0);

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
        if(Physics.Raycast(ray, out raycastHit, maxDistanceFromScreen, layerMask))
        {
            moveTo(voxelizeVector(raycastHit.point, voxelLength, voxelOffset));
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
        if (distance < maxDistanceToMouse)
        {
            return;
        }
        Vector3 force = (position - transform.position) * (mouseGravitation * (rigidbody.mass / distance * distance));
        rigidbody.AddForce(force, ForceMode.Acceleration);
    }

    Vector3 voxelizeVector(Vector3 vector, float voxelLength, Vector3 offset)
    {
        return new Vector3(voxelizeFloat(vector.x, voxelLength, offset.x), vector.y, voxelizeFloat(vector.z, voxelLength, offset.z));
    }

    float voxelizeFloat(float number, float voxelSteps, float offset)
    {
        return (Mathf.Round((number+offset)/voxelSteps) * voxelSteps)-offset;
    }
}
