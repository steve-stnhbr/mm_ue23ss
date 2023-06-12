using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPrompt : MonoBehaviour
{
    Camera camera;
    Vector3 initialScale;

    private void OnEnable()
    {
        camera = Camera.main;
        initialScale = transform.localScale;
    }

    private void Update()
    {
        // Revert scale from parent
        Vector3 parentScale = transform.parent.localScale;
        transform.localScale = new Vector3(initialScale.x / parentScale.x, initialScale.y / parentScale.y, initialScale.z / parentScale.z);

        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
    }

    public void destroyPrompt()
    {
        Destroy(this.gameObject);
    }
}
