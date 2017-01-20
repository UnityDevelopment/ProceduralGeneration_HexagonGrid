using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class DisplayMeshVertices : MonoBehaviour
{
    /// <summary>
    /// Draws our mesh vertices gizmo
    /// </summary>
    private void OnDrawGizmos()
    {
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();

        Mesh mesh = meshFilter.sharedMesh;

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

        foreach (Vector3 vertex in mesh.vertices)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(vertex, 0.035f);
        }
    }
}
