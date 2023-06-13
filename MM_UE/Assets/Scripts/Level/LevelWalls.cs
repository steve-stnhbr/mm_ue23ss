using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelWalls : MonoBehaviour
{
    [Tooltip("The material that is applied to the walls surrounding the level")]
    public Material wallMaterial;

    float levelWidth, levelHeight, levelDepth;
    Vector3 levelSize;

    List<GameObject> walls;

    // Start is called before the first frame update
    void Start()
    {
        walls = new List<GameObject>();

        Level cur = LevelManager.getCurrentLevel();
        levelWidth = cur.levelWidth;
        levelHeight = cur.levelHeight;
        levelDepth = cur.levelDepth;
        levelSize = cur.getLevelSize();

        createWallGameObjects();
    }

    // Update is called once per frame
    void Update()
    {
        Camera cam = Camera.main;
        foreach (GameObject obj in walls)
        {
            if (!(obj.name.Contains("_right") || obj.name.Contains("_left"))) {
                continue;
            }
            Vector3 wallCenter = obj.GetComponent<MeshFilter>().mesh.bounds.center - levelSize / 2;
            Vector3 cameraCenter = cam.transform.position - levelSize / 2;
            if (Math.Sign(wallCenter.x) == Math.Sign(cameraCenter.x) 
                && Math.Sign(wallCenter.z) == Math.Sign(cameraCenter.z))
            {
                obj.GetComponent<Renderer>().forceRenderingOff = true;
                obj.SetActive(false);
            } else
            {
                obj.GetComponent<Renderer>().forceRenderingOff = false;
                obj.SetActive(true);
            }
        }
    }


    private void createWallGameObjects()
    {
        int wallExtent = (int)(Math.Max(levelWidth, Math.Max(levelHeight, levelDepth)) * 4);
        // front wall
        /*
        createPlaneForCorners(
            new Vector3(0, 0, 0),
            new Vector3(levelWidth, 0, 0),
            new Vector3(levelWidth, levelHeight, 0),
            new Vector3(0, levelHeight, 0)
        );
        */

        createPlaneForCorners(
            new Vector3(-wallExtent, -wallExtent, 0),
            new Vector3(wallExtent, -wallExtent, 0),
            new Vector3(wallExtent, wallExtent, 0),
            new Vector3(-wallExtent, wallExtent, 0)
        );
        // back wall
        createPlaneForCorners(
            new Vector3(-wallExtent, -wallExtent, levelDepth),
            new Vector3(-wallExtent, wallExtent, levelDepth),
            new Vector3(wallExtent, wallExtent, levelDepth),
            new Vector3(wallExtent, -wallExtent, levelDepth)
        );
        // left wall
        createPlaneForCorners(
            new Vector3(levelWidth, -wallExtent, -wallExtent),
            new Vector3(levelWidth, -wallExtent, wallExtent),
            new Vector3(levelWidth, wallExtent, wallExtent),
            new Vector3(levelWidth, wallExtent, -wallExtent)
        );
        // right wall
        createPlaneForCorners(
            new Vector3(0, -wallExtent, -wallExtent),
            new Vector3(0, wallExtent, -wallExtent),
            new Vector3(0, wallExtent, wallExtent),
            new Vector3(0, -wallExtent, wallExtent)
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
            new Vector3(0, wallExtent, 0),
            new Vector3(levelWidth, wallExtent, 0),
            new Vector3(levelWidth, levelHeight, 0),
            new Vector3(0, levelHeight, 0),
            "front_top"
        );
        createPlaneForCorners(
            new Vector3(0, 0, 0),
            new Vector3(levelWidth, 0, 0),
            new Vector3(levelWidth, -wallExtent, 0),
            new Vector3(0, -wallExtent, 0),
            "front_bottom"
        );
        createPlaneForCorners(
            new Vector3(-wallExtent, -wallExtent, 0),
            new Vector3(-wallExtent, wallExtent, 0),
            new Vector3(0, wallExtent, 0),
            new Vector3(0, -wallExtent, 0),
            "front_left"
        );
        createPlaneForCorners(
            new Vector3(levelWidth, -wallExtent, 0),
            new Vector3(levelWidth, wallExtent, 0),
            new Vector3(wallExtent, wallExtent, 0),
            new Vector3(wallExtent, -wallExtent, 0),
            "front_right"
        );


        // bound walls for left side
        createPlaneForCorners(
            new Vector3(0, levelHeight, 0),
            new Vector3(0, levelHeight, levelDepth),
            new Vector3(0, wallExtent, levelDepth),
            new Vector3(0, wallExtent, 0),
            "left_top"
        );
        createPlaneForCorners(
            new Vector3(0, -wallExtent, 0),
            new Vector3(0, -wallExtent, levelDepth),
            new Vector3(0, 0, levelDepth),
            new Vector3(0, 0, 0),
            "left_bottom"
        );
        createPlaneForCorners(
            new Vector3(0, -wallExtent, levelDepth),
            new Vector3(0, -wallExtent, wallExtent),
            new Vector3(0, wallExtent, wallExtent),
            new Vector3(0, wallExtent, levelDepth),
            "left_left"
        );
        createPlaneForCorners(
            new Vector3(0, wallExtent, 0),
            new Vector3(0, wallExtent, -wallExtent),
            new Vector3(0, -wallExtent, -wallExtent),
            new Vector3(0, -wallExtent, 0),
            "left_right"
        );

        //bound walls for back side
        createPlaneForCorners(
            new Vector3(0, levelHeight, levelDepth),
            new Vector3(levelWidth, levelHeight, levelDepth),
            new Vector3(levelWidth, wallExtent, levelDepth),
            new Vector3(0, wallExtent, levelDepth),
            "back_top"
        );
        createPlaneForCorners(
            new Vector3(0, -wallExtent, levelDepth),
            new Vector3(levelWidth, -wallExtent, levelDepth),
            new Vector3(levelWidth, 0, levelDepth),
            new Vector3(0, 0, levelDepth),
            "back_bottom"
        );
        createPlaneForCorners(
            new Vector3(wallExtent, -wallExtent, levelDepth),
            new Vector3(wallExtent, wallExtent, levelDepth),
            new Vector3(levelWidth, wallExtent, levelDepth),
            new Vector3(levelWidth, -wallExtent, levelDepth),
            "back_left"
        );
        createPlaneForCorners(
            new Vector3(0, -wallExtent, levelDepth),
            new Vector3(0, wallExtent, levelDepth),
            new Vector3(-wallExtent, wallExtent, levelDepth),
            new Vector3(-wallExtent, -wallExtent, levelDepth),
            "back_right"
        );

        // bound walls for right side
        createPlaneForCorners(
            new Vector3(levelWidth, wallExtent, 0),
            new Vector3(levelWidth, wallExtent, levelDepth),
            new Vector3(levelWidth, levelHeight, levelDepth),
            new Vector3(levelWidth, levelHeight, 0),
            "right_top"
        );
        createPlaneForCorners(
            new Vector3(levelWidth, 0, 0),
            new Vector3(levelWidth, 0, levelDepth),
            new Vector3(levelWidth, -wallExtent, levelDepth),
            new Vector3(levelWidth, -wallExtent, 0),
            "right_bottom"
        );
        createPlaneForCorners(
            new Vector3(levelWidth, -wallExtent, 0),
            new Vector3(levelWidth, -wallExtent, -wallExtent),
            new Vector3(levelWidth, wallExtent, -wallExtent),
            new Vector3(levelWidth, wallExtent, 0),
            "right_left"
        );
        createPlaneForCorners(
            new Vector3(levelWidth, wallExtent, levelDepth),
            new Vector3(levelWidth, wallExtent, wallExtent),
            new Vector3(levelWidth, -wallExtent, wallExtent),
            new Vector3(levelWidth, -wallExtent, levelDepth),
            "right_right"
        );
    }

    private void createPlaneForCorners(Vector3 corner1, Vector3 corner2, Vector3 corner3, Vector3 corner4, String name = "GenereatedWallPlane")
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
                //uvs[index] = new Vector2((float)j / widthSegments, (float)i / lengthSegments);
                uvs[index] = new Vector2((float)j % 2, (float)i % 2);
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
        plane.name = name;
        plane.AddComponent<MeshFilter>().mesh = mesh;
        plane.AddComponent<MeshRenderer>().material = wallMaterial;
        plane.AddComponent<MeshCollider>().sharedMesh = mesh;
        plane.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        plane.isStatic = true;
        walls.Add(plane);
    }
}
