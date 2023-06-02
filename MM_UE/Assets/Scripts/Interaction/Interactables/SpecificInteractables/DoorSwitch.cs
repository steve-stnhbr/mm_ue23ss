using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : Switch
{
    [Header("Door")]
    [SerializeField]
    [Tooltip("Specifies where the door moves locally when opening")]
    Vector3 openLocalPosition = new Vector3(0, -1, 0);
    [Tooltip("Time to open and close the door in seconds")]
    public float timeToOpen = 1f;

    float openPercent = 0;
    Vector3 closedLocalPosition;


    protected override void DoWhileOnFixed()
    {
        UpdateDoor(Time.fixedDeltaTime / timeToOpen);
    }

    protected override void DoWhileOffFixed()    
    {
        UpdateDoor(-Time.fixedDeltaTime / timeToOpen);
    }



    // Start is called before the first frame update
    void Start()
    {
        closedLocalPosition = transform.localPosition;
    }

    protected void UpdateDoor(float changePercent)
    {
        openPercent = Mathf.Clamp01(openPercent + changePercent);
        transform.localPosition = new Vector3(
            closedLocalPosition.x - (closedLocalPosition.x - openLocalPosition.x) * openPercent,
            closedLocalPosition.y - (closedLocalPosition.y - openLocalPosition.y) * openPercent,
            closedLocalPosition.z - (closedLocalPosition.z - openLocalPosition.z) * openPercent);
    }

    protected override void SwitchOn(EnumActor actor)
    {
    }
    protected override void SwitchOff(EnumActor actor)
    {
    }
}
