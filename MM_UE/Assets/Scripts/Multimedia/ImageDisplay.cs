using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDisplay : Switch
{
    [SerializeField] GameObject imageObject;
    protected override void DoWhileOffFixed()
    {
        imageObject.SetActive(false);
    }

    protected override void DoWhileOnFixed()
    {
        imageObject.SetActive(true);
    }

    protected override void SwitchOff(EnumActor actor)
    {
    }

    protected override void SwitchOn(EnumActor actor)
    {
    }
}
