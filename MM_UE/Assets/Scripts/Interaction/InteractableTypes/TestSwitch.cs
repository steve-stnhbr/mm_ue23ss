using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSwitch : Switch
{
    public Material onMaterial;
    public Material offMaterial;
    public MeshRenderer meshRenderer;


    protected override void DoWhileOffFixed()
    {
    }

    protected override void DoWhileOnFixed()
    {
    }

    protected override void SwitchOff(EnumActor actor)
    {
        meshRenderer.material = offMaterial;
    }

    protected override void SwitchOn(EnumActor actor)
    {
        meshRenderer.material = onMaterial;
    }

}
