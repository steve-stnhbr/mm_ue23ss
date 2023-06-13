using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagePuzzleLevel : Level
{
    public Transform canvasTransform;
    [Tooltip("The GameObjects which has a MeshRenderer and material whose mainTexture is the Image to be checked")]
    public GameObject image;

    public Switch goal;

    public int checkEveryNFrames = 10;

    Texture2D imageTexture;
    Vector3 canvasPosition;

    // Start is called before the first frame update
    void Start()
    {
        canvasPosition = canvasTransform.position;
        //imageTexture = (Texture2D) image.GetComponent<MeshRenderer>().material.mainTexture;
        imageTexture = image.GetComponent<SpriteRenderer>().sprite.texture;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % checkEveryNFrames == 0)
        {
            bool finished = checkCanvas();
            if (!goal.state && finished)
            {
                Debug.Log("Finished Puzzle");
                goal.state = true;
                goal.Interact(EnumActor.Object);
            } else if (goal.state && !finished)
            {
                Debug.Log("Unfinished Puzzle");
                goal.state = false;
                goal.Interact(EnumActor.Object);
            }
        }
    }

    private bool checkCanvas()
    {
        Vector2 dimensions = new Vector2(imageTexture.width, imageTexture.height);
        for (int i = 0; i < dimensions.x; i++)
        {
            for (int j = 0; j < dimensions.y; j++)
            {
                float grayScaleValue = imageTexture.GetPixel(i, j).grayscale;
                bool colored = grayScaleValue < .5f;
                Vector3 boxCheckPosition = canvasPosition + new Vector3(i, 1, j);
                bool boxIsPlaced = checkForBox(boxCheckPosition);
                if (colored != boxIsPlaced)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool checkForBox(Vector3 vector)
    {
        return Physics.CheckSphere(vector, .2f, LayerMask.GetMask(new string[] { "Decimate" }));
    }
    /* Debug Gizmos
    private void OnDrawGizmos()
    {
        Vector2 dimensions = new Vector2(imageTexture.width, imageTexture.height);
        for (int i = 0; i < dimensions.x; i++)
        {
            for (int j = 0; j < dimensions.y; j++)
            {
                float grayScaleValue = imageTexture.GetPixel(i, j).grayscale;
                bool colored = grayScaleValue < .5f;
                Vector3 boxCheckPosition = canvasPosition + new Vector3(i, 1, j);
                bool boxIsPlaced = checkForBox(boxCheckPosition);
                Debug.Log("(" + i + ":" + j + "):" + grayScaleValue + "->" + colored + ":" + boxIsPlaced);
                //UnityEditor.Handles.Label(boxCheckPosition, "(" + i + ":" + j + ")");
                //UnityEditor.Handles.DrawWireCube(boxCheckPosition, Vector3.one * .1f);
                if (colored)
                {
                UnityEditor.Handles.DrawSolidDisc(boxCheckPosition, Vector3.up, .2f);
                }
            }
        }
    }
    */
}

