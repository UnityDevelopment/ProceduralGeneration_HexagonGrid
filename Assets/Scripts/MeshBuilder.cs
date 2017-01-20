using UnityEngine;

public class MeshBuilder : MonoBehaviour
{
    /// <summary>
    /// Returns the combined mesh of the two specified meshes
    /// </summary>
    /// <param name="firstMesh">First Mesh to combine</param>
    /// <param name="secondMesh">Second Mesh to combine</param>
    /// <returns>Mesh</returns>
    public static Mesh Combine(Mesh firstMesh, Mesh secondMesh)
    {
        Mesh combined = new Mesh();

        combined.vertices = CombineVertices(firstMesh, secondMesh);
        combined.triangles = CombineTriangles(firstMesh, secondMesh);

        combined.RecalculateBounds();
        combined.RecalculateNormals();

        return combined;
    }

    /// <summary>
    /// Returns the combined triangles of the two specified meshes
    /// </summary>
    /// <param name="firstMesh">First Mesh to combine</param>
    /// <param name="secondMesh">Second Mesh to combine</param>
    /// <returns>int[]</returns>
    private static int[] CombineTriangles(Mesh firstMesh, Mesh secondMesh)
    {
        int trianglesOffset = firstMesh.triangles.Length;
        int verticesOffset = firstMesh.vertices.Length;
        int combinedTrianglesLength = firstMesh.triangles.Length + secondMesh.triangles.Length;

        int[] triangles = new int[combinedTrianglesLength];

        firstMesh.triangles.CopyTo(triangles, 0);

        for (int triangleIndex = 0; triangleIndex < secondMesh.triangles.Length; triangleIndex++)
        {
            triangles[triangleIndex + trianglesOffset] = secondMesh.triangles[triangleIndex] + verticesOffset;
        }

        return triangles;
    }

    /// <summary>
    /// Returns the combined vertices of the two specified meshes
    /// </summary>
    /// <param name="firstMesh">First Mesh to combine</param>
    /// <param name="secondMesh">Second Mesh to combine</param>
    /// <returns></returns>
    private static Vector3[] CombineVertices(Mesh firstMesh, Mesh secondMesh)
    {
        int combinedVerticesLength = firstMesh.vertices.Length + secondMesh.vertices.Length;

        Vector3[] combinedVertices = new Vector3[combinedVerticesLength];

        firstMesh.vertices.CopyTo(combinedVertices, 0);
        secondMesh.vertices.CopyTo(combinedVertices, firstMesh.vertices.Length);

        return combinedVertices;
    }

    /// <summary>
    /// Returns a new mesh repositioned by the specified offset
    /// </summary>
    /// <param name="originalMesh">The mesh to offset</param>
    /// <param name="offset">The offset</param>
    /// <returns>Mesh</returns>
    public static Mesh Offset(Mesh originalMesh, Vector3 offset)
    {
        Mesh offsetMesh = new Mesh();

        int offsetVerticesLength = originalMesh.vertices.Length;

        Vector3[] offsetVertices = new Vector3[offsetVerticesLength];

        for (int i = 0; i < originalMesh.vertices.Length; i++)
        {
            offsetVertices[i] = originalMesh.vertices[i] + offset;
        }

        offsetMesh.vertices = offsetVertices;
        offsetMesh.triangles = originalMesh.triangles;
        offsetMesh.uv = originalMesh.uv;

        offsetMesh.RecalculateBounds();
        offsetMesh.normals = originalMesh.normals;

        return offsetMesh;
    }
}