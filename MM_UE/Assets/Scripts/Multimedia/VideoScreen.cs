using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoScreen : Switch
{
    [SerializeField] VideoPlayer videoPlayer;

    protected override void DoWhileOffFixed()
    {

    }

    protected override void DoWhileOnFixed()
    {

    }

    protected override void SwitchOff(EnumActor actor)
    {
        videoPlayer.Play();
    }

    protected override void SwitchOn(EnumActor actor)
    {
        videoPlayer.Pause();
    }
}
