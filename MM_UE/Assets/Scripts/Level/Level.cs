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

    [Tooltip("A prefab for the walls created around the Levels bounds")]
    public GameObject wallPrefab;

    public string sceneName;

    [Tooltip("The material that is applied to the walls surrounding the level")]
    public Material wallMaterial;

    Vector3 levelSize;

    // Start is called before the first frame update
    void Start()
    {
        levelSize = new Vector3(levelWidth, levelHeight, levelDepth);
        createWallGameObjects();
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

    private void createWallGameObjects()
    {

        // front wall
        createPlaneForCorners(new Vector3(0, 0, 0), new Vector3(levelWidth, 0, 0), new Vector3(levelWidth, levelHeight, 0), new Vector3(0, levelHeight, 0));
        // hind wall
        createPlaneForCorners(new Vector3(0, 0, levelDepth), new Vector3(0, levelHeight, levelDepth), new Vector3(levelWidth, levelHeight, levelDepth), new Vector3(levelWidth, 0, levelDepth));
        //left wall
        createPlaneForCorners(new Vector3(levelWidth, 0, 0), new Vector3(levelWidth, 0, levelDepth), new Vector3(levelWidth, levelHeight, levelDepth), new Vector3(levelWidth, levelHeight, 0));
        // right wall
        createPlaneForCorners(new Vector3(0, levelHeight, 0), new Vector3(0, levelHeight, levelDepth), new Vector3(0, 0, levelDepth), new Vector3(0, 0, 0));
        // Ceiling
        createPlaneForCorners(new Vector3(0, levelHeight, 0), new Vector3(levelWidth, levelHeight, 0), new Vector3(levelWidth, levelHeight, levelDepth), new Vector3(0, levelHeight, levelDepth));

        // bottom wall
        createPlaneForCorners(new Vector3(0, 0, 0), new Vector3(levelWidth, 0, 0), new Vector3(levelWidth, -100, 0), new Vector3(0, -100, 0));
        // left Wall
        createPlaneForCorners(new Vector3(-100, -100, 0), new Vector3(-100, 100, 0), new Vector3(0, 100, 0), new Vector3(0, -100, 0));
        // right wall
        createPlaneForCorners(new Vector3(levelWidth, -100, 0), new Vector3(levelWidth, 100, 0), new Vector3(100, 100, 0), new Vector3(100, -100, 0));
        // top wall
        createPlaneForCorners(new Vector3(0, 100, 0), new Vector3(levelWidth, 100, 0), new Vector3(levelWidth, levelHeight, 0), new Vector3(0, levelHeight, 0));
        
        // TODO: add bound walls for other perspectives
    }

    private void createPlaneForCorners(Vector3 corner1, Vector3 corner2, Vector3 corner3, Vector3 corner4)
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Mesh mesh = new Mesh();
        Vector3 half = Vector3.one / 2;
        mesh.vertices = new Vector3[]
        {
            corner1 - half,
            corner2 - half,
            corner3 - half,
            corner4 - half
        };

        mesh.triangles = new int[]
        {
            0,1,2,2,3,0
        };

        Plane geoPlane = new Plane();
        geoPlane.Set3Points(corner1, corner2, corner3);
        Vector3 normal = geoPlane.normal;

        mesh.normals = new Vector3[] {
            normal,
            normal,
            normal,
            normal
        };

        plane.GetComponent<MeshFilter>().mesh = mesh;
        plane.GetComponent<MeshCollider>().sharedMesh = mesh;

        plane.GetComponent<MeshRenderer>().material = wallMaterial;

        /*
        plane.transform.position = (corner1 + corner2 + corner3 + corner4) / 4;
        Bounds bounds = plane.GetComponent<MeshFilter>().mesh.bounds;
        bounds.extents = new Vector3(20, 0, 50);
        */
    }
}
