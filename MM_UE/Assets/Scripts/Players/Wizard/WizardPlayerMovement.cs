using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardPlayerMovement: MonoBehaviour, IDisableInputForMenu, IDisableInputForInteraction
{
    public float maxDistance = 30;
    public LayerMask layerMask;
    public float cameraOffset;
    [Tooltip("This value describes the maximum speed the wizard player can have while moving to the mouse")]
    public float maxSpeed;
    [Tooltip("This value determines how strongly the wizard player is attached to the mouse movement")]
    public float mouseGravitation;
    [Tooltip("This value describes how far away from the mouse no more force is exerted on the wizard player")]
    public float maxDistanceToMouse;

    [Tooltip("This value determines at what player speed the turbine shuts off")]
    public float turbineInactiveSpeed;

    public GameObject locationMarker;
    public LayerMask layerMaskLocation;

    Rigidbody rigidbody;

    AudioSource audioSource;

    bool inputDisabledForMenu = false;
    bool inputDisabledForInteraction = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }


    void FixedUpdate()
    {
        if (rigidbody.velocity.magnitude > maxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
        }
        
        if (inputDisabledForInteraction || inputDisabledForMenu)
        {
            return;
        }

        Vector3 position = Input.mousePosition;
        // offset, to make the tracker visible
        position.z = cameraOffset;
        Vector3 levelPos = Camera.main.ScreenToWorldPoint(position);

        // check for nearest collider in line behind mouse position and move towards it
        RaycastHit raycastHit;
        Ray ray = new Ray(levelPos, levelPos - Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)));
        if (Physics.Raycast(ray, out raycastHit, maxDistance, layerMask))
        {
            moveTo(LevelManager.getCurrentLevel().worldPositionToLevelPosition(raycastHit.point));
        }
        
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        ParticleSystem.EmissionModule emission = ps.emission;
        if (rigidbody.velocity.magnitude > turbineInactiveSpeed)
        {
            emission.enabled = true;
            emission.rateOverTime = rigidbody.velocity.magnitude * 100;
            audioSource.volume = rigidbody.velocity.magnitude * .04f;
        } else
        {
            emission.enabled = false;
        }


        RaycastHit raycastHitLocation;
        Ray rayLocation = new Ray(transform.position, new Vector3(0, -1, 0));
        if (Physics.Raycast(rayLocation, out raycastHitLocation, maxDistance, layerMaskLocation))
        {
            Vector3 voxelVector = LevelManager.getCurrentLevel().worldPositionToLevelPosition(raycastHitLocation.point);
            locationMarker.transform.position = new Vector3(voxelVector.x, raycastHitLocation.point.y, voxelVector.z);
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
        Vector3 xz = new Vector3(force.x, 0, force.z);
        rigidbody.AddForce(xz, ForceMode.Acceleration);
        if (force.magnitude > .3f)
        {
            rigidbody.transform.rotation = Quaternion.Slerp(rigidbody.transform.rotation, Quaternion.LookRotation(rigidbody.velocity), Time.deltaTime * 40f);
        }
    }

    public void DisableInputForMenu()
    {
        inputDisabledForMenu = true;
    }

    public void EnableInputForMenu()
    {
        inputDisabledForMenu = false;
    }

    public void DisableInputForInteraction()
    {
        inputDisabledForInteraction = true;
    }

    public void EnableInputForInteraction()
    {
        inputDisabledForInteraction = false;
    }
}
