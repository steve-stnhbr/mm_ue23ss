using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    public float maxDistance = 30;
    public string layerMask = "Default";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 position = Input.mousePosition;
        position.z = 3;
        Vector3 screenWorldPos = Camera.main.ScreenToWorldPoint(position);


        RaycastHit raycastHit;
        Ray ray = new Ray(screenWorldPos, screenWorldPos-Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)));
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 0.1f);
        if(Physics.Raycast(ray, out raycastHit, maxDistance, LayerMask.GetMask(layerMask)))
        {
            transform.position = raycastHit.point;
        }
        else
        {
            transform.position = screenWorldPos;
        }

        
    }
}
