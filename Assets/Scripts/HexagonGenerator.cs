using UnityEngine;

public class HexagonGenerator : MonoBehaviour
{
    /// <summary>
    /// Creates a hexagon mesh
    /// </summary>
    /// <returns>Mesh</returns>
    public static Mesh CreateHexagon()
    {
        Mesh mesh = new Mesh();

        mesh.vertices = DefineVertices();
        mesh.triangles = DefineTriangles();

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }

    /// <summary>
    /// Defines the vertices for the hexagon
    /// </summary>
    /// <returns>Vector3[]</returns>
    private static Vector3[] DefineVertices()
    {
        // vertices - the points in space for the centre and corners of the hexagon
        Vector3[] vertices = new Vector3[7];

        vertices[0] = new Vector3(0.5f, 0f, 0.5f);      // center
        vertices[1] = new Vector3(0.25f, 0f, 0f);       // bottom left
        vertices[2] = new Vector3(0f, 0f, 0.5f);        // middle left
        vertices[3] = new Vector3(0.25f, 0f, 1f);       // top left
        vertices[4] = new Vector3(0.75f, 0f, 1f);       // top right
        vertices[5] = new Vector3(1f, 0f, 0.5f);        // middle right
        vertices[6] = new Vector3(0.75f, 0f, 0f);       // bottom right

        return vertices;
    }

    /// <summary>
    /// Defines the triangles for the hexagon mesh
    /// </summary>
    /// <returns>int[]</returns>
    private static int[] DefineTriangles()
    {
        // triangles are defined by their index of vertices in the vertices array
        int[] triangles = new int[18];

        triangles[0] = 1;
        triangles[1] = 2;
        triangles[2] = 0;
        triangles[3] = 2;
        triangles[4] = 3;
        triangles[5] = 0;
        triangles[6] = 3;
        triangles[7] = 4;
        triangles[8] = 0;
        triangles[9] = 4;
        triangles[10] = 5;
        triangles[11] = 0;
        triangles[12] = 5;
        triangles[13] = 6;
        triangles[14] = 0;
        triangles[15] = 6;
        triangles[16] = 1;
        triangles[17] = 0;

        return triangles;
    }
}
