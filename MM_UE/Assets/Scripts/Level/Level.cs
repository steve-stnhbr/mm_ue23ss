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

    public WizardSkillBehaviour GetWizardSkillBehaviour()
    {
        return UnityEngine.Object.FindObjectsOfType<WizardSkillBehaviour>()[0];
    }

    private void createWallGameObjects()
    {
        int wallExtent = (int) (Math.Max(levelWidth, Math.Max(levelHeight, levelDepth)) * 1.7);
        // front wall
        createPlaneForCorners(
            new Vector3(0, 0, 0),
            new Vector3(levelWidth, 0, 0),
            new Vector3(levelWidth, levelHeight, 0),
            new Vector3(0, levelHeight, 0)
        );
        // hind wall
        createPlaneForCorners(
            new Vector3(0, 0, levelDepth),
            new Vector3(0, levelHeight, levelDepth),
            new Vector3(levelWidth, levelHeight, levelDepth),
            new Vector3(levelWidth, 0, levelDepth)
        );
        //left wall
        createPlaneForCorners(
            new Vector3(levelWidth, 0, 0),
            new Vector3(levelWidth, 0, levelDepth),
            new Vector3(levelWidth, levelHeight, levelDepth),
            new Vector3(levelWidth, levelHeight, 0)
        );
        // right wall
        createPlaneForCorners(
            new Vector3(0, levelHeight, 0),
            new Vector3(0, levelHeight, levelDepth),
            new Vector3(0, 0, levelDepth),
            new Vector3(0, 0, 0)
        );
        // Ceiling
        createPlaneForCorners(
            new Vector3(0, levelHeight, 0),
            new Vector3(levelWidth, levelHeight, 0),
            new Vector3(levelWidth, levelHeight, levelDepth),
            new Vector3(0, levelHeight, levelDepth)
        );

        // bound walls for front side
        createPlaneForCorners(
            new Vector3(0, 0, 0), 
            new Vector3(levelWidth, 0, 0), 
            new Vector3(levelWidth, -wallExtent, 0),
            new Vector3(0, -wallExtent, 0)
        );
        createPlaneForCorners(
            new Vector3(-wallExtent, -wallExtent, 0),
            new Vector3(-wallExtent, wallExtent, 0),
            new Vector3(0, wallExtent, 0),
            new Vector3(0, -wallExtent, 0)
        );
        createPlaneForCorners(
            new Vector3(levelWidth, -wallExtent, 0),
            new Vector3(levelWidth, wallExtent, 0),
            new Vector3(wallExtent, wallExtent, 0),
            new Vector3(wallExtent, -wallExtent, 0)
        );
        createPlaneForCorners(
            new Vector3(0, wallExtent, 0),
            new Vector3(levelWidth, wallExtent, 0),
            new Vector3(levelWidth, levelHeight, 0),
            new Vector3(0, levelHeight, 0)
        );

        // bound walls for left side
        createPlaneForCorners(
            new Vector3(0, levelHeight, 0),
            new Vector3(0, levelHeight, levelDepth),
            new Vector3(0, wallExtent, levelDepth),
            new Vector3(0, wallExtent, 0)
        );
        createPlaneForCorners(
            new Vector3(0, -wallExtent, 0), 
            new Vector3(0, -wallExtent, levelDepth), 
            new Vector3(0, 0, levelDepth), 
            new Vector3(0, 0, 0)
        );
        createPlaneForCorners(
            new Vector3(0, -wallExtent, levelDepth), 
            new Vector3(0, -wallExtent, wallExtent), 
            new Vector3(0, wallExtent, wallExtent), 
            new Vector3(0, wallExtent, levelDepth)
        );
        createPlaneForCorners(
            new Vector3(0, wallExtent, 0),
            new Vector3(0, wallExtent, -wallExtent),
            new Vector3(0, -wallExtent, -wallExtent),
            new Vector3(0, -wallExtent, 0)
        );

        //bound walls for back side
        createPlaneForCorners(
            new Vector3(0, -wallExtent, levelDepth),
            new Vector3(levelWidth, -wallExtent, levelDepth),
            new Vector3(levelWidth, 0, levelDepth),
            new Vector3(0, 0, levelDepth)
        );
        createPlaneForCorners(
            new Vector3(0, -wallExtent, levelDepth),
            new Vector3(0, wallExtent, levelDepth),
            new Vector3(-wallExtent, wallExtent, levelDepth),
            new Vector3(-wallExtent, -wallExtent, levelDepth)
        );
        createPlaneForCorners(
            new Vector3(wallExtent, -wallExtent, levelDepth),
            new Vector3(wallExtent, wallExtent, levelDepth),
            new Vector3(levelWidth, wallExtent, levelDepth),
            new Vector3(levelWidth, -wallExtent, levelDepth)
        );
        createPlaneForCorners(
            new Vector3(0, levelHeight, levelDepth),
            new Vector3(levelWidth, levelHeight, levelDepth),
            new Vector3(levelWidth, wallExtent, levelDepth),
            new Vector3(0, wallExtent, levelDepth)
        );


        // bound walls for right side
        createPlaneForCorners(
            new Vector3(levelWidth, wallExtent, 0),
            new Vector3(levelWidth, wallExtent, levelDepth),
            new Vector3(levelWidth, levelHeight, levelDepth),
            new Vector3(levelWidth, levelHeight, 0)
        );
        createPlaneForCorners(
            new Vector3(levelWidth, 0, 0),
            new Vector3(levelWidth, 0, levelDepth),
            new Vector3(levelWidth, -wallExtent, levelDepth),
            new Vector3(levelWidth, -wallExtent, 0)
        );
        createPlaneForCorners(
            new Vector3(levelWidth, wallExtent, levelDepth),
            new Vector3(levelWidth, wallExtent, wallExtent),
            new Vector3(levelWidth, -wallExtent, wallExtent),
            new Vector3(levelWidth, -wallExtent, levelDepth)
        );
        createPlaneForCorners(
            new Vector3(levelWidth, -wallExtent, 0),
            new Vector3(levelWidth, -wallExtent, -wallExtent),
            new Vector3(levelWidth, wallExtent, -wallExtent),
            new Vector3(levelWidth, wallExtent, 0)
        );
    }

    private void createPlaneForCorners(Vector3 corner1, Vector3 corner2, Vector3 corner3, Vector3 corner4)
    {
        Vector3 half = Vector3.one / 2;
        corner1 = corner1 - half;
        corner2 = corner2 - half;
        corner3 = corner3 - half;
        corner4 = corner4 - half;

        float unitLength = 1.0f; // Längeneinheit

        // Berechne die Anzahl der Längeneinheiten in beiden Dimensionen
        int widthSegments = Mathf.FloorToInt(Vector3.Distance(corner1, corner2) / unitLength);
        int lengthSegments = Mathf.FloorToInt(Vector3.Distance(corner1, corner4) / unitLength);

        // Erzeuge das Gesamt-Mesh
        Mesh mesh = new Mesh();

        // Berechne die Anzahl der Vertices, Dreiecke und UVs
        int numVertices = (widthSegments + 1) * (lengthSegments + 1);
        int numTriangles = widthSegments * lengthSegments * 2;
        int numUVs = numVertices;

        // Arrays für Vertices, Normals, Triangles und UVs erstellen
        Vector3[] vertices = new Vector3[numVertices];
        Vector3[] normals = new Vector3[numVertices];
        int[] triangles = new int[numTriangles * 3];
        Vector2[] uvs = new Vector2[numUVs];

        // Berechne den Halb-Offset
        Vector3 halfOffset = new Vector3(unitLength, 0.0f, unitLength) / 2;

        // Erzeuge die Vertices, Normals und UVs
        for (int i = 0; i <= lengthSegments; i++)
        {
            for (int j = 0; j <= widthSegments; j++)
            {
                int index = i * (widthSegments + 1) + j;

                // Berechne die Position des Vertices
                Vector3 position = corner1 + (j * unitLength / widthSegments) * (corner2 - corner1) + (i * unitLength / lengthSegments) * (corner4 - corner1);

                // Setze den Vertex, Normal und UV
                vertices[index] = position;
                normals[index] = Vector3.Cross(corner4 - corner1, corner2 - corner1).normalized;
                uvs[index] = new Vector2((float)j / widthSegments, (float)i / lengthSegments);
            }
        }

        // Erzeuge die Dreiecke
        int triangleIndex = 0;
        for (int i = 0; i < lengthSegments; i++)
        {
            for (int j = 0; j < widthSegments; j++)
            {
                int vertexIndex = i * (widthSegments + 1) + j;

                // Erzeuge das erste Dreieck
                triangles[triangleIndex] = vertexIndex;
                triangles[triangleIndex + 1] = vertexIndex + 1;
                triangles[triangleIndex + 2] = vertexIndex + widthSegments + 1;

                // Erzeuge das zweite Dreieck
                triangles[triangleIndex + 3] = vertexIndex + widthSegments + 1;
                triangles[triangleIndex + 4] = vertexIndex + 1;
                triangles[triangleIndex + 5] = vertexIndex + widthSegments + 2;

                triangleIndex += 6;
            }
        }

        // Setze das Mesh
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        // Erzeuge das GameObject
        GameObject plane = new GameObject("Plane");
        plane.AddComponent<MeshFilter>().mesh = mesh;
        plane.AddComponent<MeshRenderer>().material = wallMaterial;
        plane.AddComponent<MeshCollider>().sharedMesh = mesh;
    }
}
