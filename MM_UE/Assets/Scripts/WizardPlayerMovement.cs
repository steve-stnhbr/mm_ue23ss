using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardPlayerMovement: MonoBehaviour
{
    public float maxDistance = 30;
    public LayerMask layerMask;
    public float cameraOffset;
    // This value determines how strongly the wizard player is attached to the mouse movement
    public float followStrength;


    // Start is called before the first frame update
    void Start()
    {
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
        else
        {
            transform.position = screenWorldPos;
        }
    }

    void moveTo(Vector3 position)
    {
        Vector3 diff = (position - transform.position);
        Vector3 newPos = transform.position + diff.normalized * diff.magnitude * followStrength;
        transform.position = position;
    }
}
