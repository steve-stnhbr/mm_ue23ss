using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Level : MonoBehaviour
{
    const int MAX_LEVEL_SIZE = 500;
    const int VOXEL_SIZE = 25;

    public bool showErrors;

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
        if (!showErrors) return;
        UnityEditor.Handles.DrawWireCube(transform.position + (new Vector3(levelWidth, levelHeight, levelDepth) * (VOXEL_SIZE / 2)), new Vector3(levelWidth, levelHeight, levelDepth) * VOXEL_SIZE);
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
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
        return go.transform.position.x % VOXEL_SIZE == 0
                && go.transform.position.y % VOXEL_SIZE == 0
                && go.transform.position.z % VOXEL_SIZE == 0
                && go.transform.localScale.x == VOXEL_SIZE
                && go.transform.localScale.y == VOXEL_SIZE
                && go.transform.localScale.z == VOXEL_SIZE;
        /*
                && go.transform.position.x >= 0
                && go.transform.position.x < levelSize.x * VOXEL_SIZE
                && go.transform.position.y >= 0
                && go.transform.position.y < levelSize.y * VOXEL_SIZE
                && go.transform.position.z >= 0
                && go.transform.position.z < levelSize.z * VOXEL_SIZE;
        */
    }

    public Vector3 worldPositionToLevelPosition(Vector3 worldPos)
    {
        Vector3 levelPos = new Vector3();
        levelPos.x = (float) Math.Round(worldPos.x * VOXEL_SIZE, MidpointRounding.AwayFromZero) / VOXEL_SIZE;
        levelPos.y = (float)Math.Round(worldPos.y * VOXEL_SIZE, MidpointRounding.AwayFromZero) / VOXEL_SIZE;
        levelPos.z = (float)Math.Round(worldPos.z * VOXEL_SIZE, MidpointRounding.AwayFromZero) / VOXEL_SIZE;
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
