using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Level : MonoBehaviour
{
    const int MAX_LEVEL_SIZE = 500;
    const int VOXEL_SIZE = 25;

    [Header("Level Size")]
    [Range(0, MAX_LEVEL_SIZE)]
    [Tooltip("The amount of voxels in X direction")]
    public int levelWidth;
    [Range(0, MAX_LEVEL_SIZE)]
    [Tooltip("The amount of voxels in Y direction")]
    public int levelHeight;
    [Range(0, MAX_LEVEL_SIZE)]
    [Tooltip("The amount of voxels in Z direction")]
    public int levelDepth;

    public string sceneName;

    Vector3 levelSize;

    // Start is called before the first frame update
    void Start()
    {
        levelSize = new Vector3(levelWidth, levelHeight, levelDepth);
        SceneManager.LoadScene(sceneName);
    }

    public abstract void Stop();

    public Vector3 getLevelSize()
    {
        return levelSize;
    }

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.DrawWireCube(Vector3.zero, new Vector3(levelWidth, levelHeight, levelDepth) * VOXEL_SIZE);
    }
}
