using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageDisplay : Switch
{
    [Tooltip("When set, the object gets activated")]
    [SerializeField] Sprite imageToDisplay;
    Image image;

    private void Start()
    {
        image = GetComponentInChildren<Image>();
    }

    protected override void DoWhileOffFixed()
    {

    }

    protected override void DoWhileOnFixed()
    {
    }

    protected override void SwitchOff(EnumActor actor)
    {
        image.sprite = null;
        
    }

    protected override void SwitchOn(EnumActor actor)
    {
        image.sprite = imageToDisplay;
    }

}
