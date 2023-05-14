using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Level : MonoBehaviour
{
    const int MAX_LEVEL_SIZE = 500;
    const int MAX_VOXEL_SIZE = 25;

    public bool showErrors;
    public bool showBounds;

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

    [Range(0,MAX_VOXEL_SIZE)]
    [Tooltip("The size of a voxel")]
    public int voxelSize;

    [Tooltip("The layers that are checked on their voxel position")]
    public LayerMask checkedLayers;

    public string sceneName;

    Vector3 levelSize;

    // Start is called before the first frame update
    void Start()
    {
        levelSize = new Vector3(levelWidth, levelHeight, levelDepth);
    }

    public abstract void Stop();

    public Vector3 getLevelSize()
    {
        return levelSize;
    }

    private void OnDrawGizmos()
    {
        if (showBounds) 
            UnityEditor.Handles.DrawWireCube(transform.position + (new Vector3(levelWidth, levelHeight, levelDepth) * (voxelSize * .5f)) - Vector3.one * voxelSize / 2f, new Vector3(levelWidth, levelHeight, levelDepth) * voxelSize);

        if (!showErrors) return;
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (Transform tf in GetComponentInChildren<Transform>())
        {
            GameObject go = tf.gameObject;
            if (checkedLayers != (checkedLayers | (1 << go.layer)))
            {
                continue;
            }

            if (!gameObjectIsValid(go))
            {
                UnityEditor.Handles.color = Color.red;
                UnityEditor.Handles.DrawWireCube(go.transform.position, go.transform.localScale);
            }
        }
    }

    private bool gameObjectIsValid(GameObject go)
    {
        return go.transform.position.x % voxelSize == 0
                && go.transform.position.y % voxelSize == 0
                && go.transform.position.z % voxelSize == 0
                && go.transform.localScale.x == voxelSize
                && go.transform.localScale.y == voxelSize
                && go.transform.localScale.z == voxelSize;
        /*
                && go.transform.position.x >= 0
                && go.transform.position.x < levelSize.x * voxelSize
                && go.transform.position.y >= 0
                && go.transform.position.y < levelSize.y * voxelSize
                && go.transform.position.z >= 0
                && go.transform.position.z < levelSize.z * voxelSize;
        */
    }

    public Vector3 worldPositionToLevelPosition(Vector3 worldPos)
    {
        Vector3 levelPos = new Vector3();
        levelPos.x = (float) Math.Round(worldPos.x * voxelSize, MidpointRounding.AwayFromZero) / voxelSize;
        levelPos.y = (float)Math.Round(worldPos.y * voxelSize, MidpointRounding.AwayFromZero) / voxelSize;
        levelPos.z = (float)Math.Round(worldPos.z * voxelSize, MidpointRounding.AwayFromZero) / voxelSize;
        return levelPos;
    }

    public static Level getCurrentLevel()
    {
        foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            Level l = go.GetComponent<Level>();
            if (l != null)
                return l;
        }
        return null;
    }
}
