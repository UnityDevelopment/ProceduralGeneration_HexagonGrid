using UnityEngine;

// TODO: Create new project for hex classes
// TODO: Add to repo
// TODO: Start on raycasting
// TODO: Evaluate whether hexes will still work as desired with only 4 triangles instead of 6, I will lose the center vertex in this process

    // it would make sense to keep a separate array of the 0 baseVertexIndexes so that 
// these can be used for detection on ray casting?


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class HexagonGridGenerator : MonoBehaviour
{
    [Tooltip("The width of the hex grid, in hexagons.")]
    public int _width = 1;

    [Tooltip("The length of the hex grid, in hexagons.")]
    public int _length = 1;

    private const int HORIZONTALVERTEXSTEP = 3;     // 3 represents the number of vertex steps until the same vertex of the next hex
    private const int VERTICALVERTEXSTEP = 2;       // 2 represents the number of vertex steps until the same vertex of the next hex
    private const int HEXAGONVERTICES = 7;          // 7 represents the number of vertices on a hexagon, including one in the center

    private int _gridVertexBoundaryHorizontal;
    private int _gridVertexBoundaryVertical;

    private Vector2[] _hexagonBaseTextureCoordinates;

    /// <summary>
    /// Initialisation
    /// </summary>
    void Start()
    {
        Initialise();
    }

    /// <summary>
    /// Initialises the HexagonGridGenerator
    /// </summary>
    private void Initialise()
    {
        _gridVertexBoundaryHorizontal = (_width * HORIZONTALVERTEXSTEP) + 1;
        _gridVertexBoundaryVertical = (_length * VERTICALVERTEXSTEP) + 1;

        _hexagonBaseTextureCoordinates = GenerateHexagonBaseTextureCoordinates();

        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();

        meshFilter.sharedMesh = CreateHexagonGridMesh();
    }

    /// <summary>
    /// Creates a hexgaon grid mesh
    /// </summary>
    /// <returns>Mesh</returns>
    private Mesh CreateHexagonGridMesh()
    {
        Mesh hexagonGridMesh = new Mesh();
        Mesh hexagonMesh;

        float widthOffset = 0f;
        float lengthOffset = 0f;

        bool hexagonVerticalStep = false;

        for (int iLength = 0; iLength < _length; iLength++)
        {
            hexagonVerticalStep = false;

            for (int iWidth = 0; iWidth < _width; iWidth++)
            {
                widthOffset = 0.75f;
                lengthOffset = iLength;

                if (hexagonVerticalStep == true)
                {
                    lengthOffset += 0.5f;
                }

                hexagonVerticalStep = !hexagonVerticalStep;

                hexagonMesh = MeshBuilder.Offset(HexagonGenerator.CreateHexagon(), new Vector3((iWidth * widthOffset), 0, lengthOffset));

                hexagonGridMesh = MeshBuilder.Combine(hexagonGridMesh, hexagonMesh);
            }
        }

        hexagonGridMesh.uv = CalculateTextureCoordinates();

        hexagonGridMesh.RecalculateNormals();
        hexagonGridMesh.RecalculateBounds();

        return hexagonGridMesh;
    }

    /// <summary>
    /// Calculates the texture coordinates for the hexagon grid mesh
    /// </summary>
    /// <returns>Vector[]</returns>
    private Vector2[] CalculateTextureCoordinates()
    {
        int vertexCount = (_width * _length) * HEXAGONVERTICES;
        Vector2[] textureCoordinates = new Vector2[vertexCount];

        int uvIndex = 0;

        bool hexagonVerticalStep = false;

        float horizontalVertexOffset = 0f;
        float verticalVertexOffset = 0f;

        float u = 0f;
        float v = 0f;

        for (int iLength = 0; iLength < _length; iLength++)
        {
            hexagonVerticalStep = false;

            for (int iWidth = 0; iWidth < _width; iWidth++)
            {
                for (int baseVertexIndex = 0; baseVertexIndex < _hexagonBaseTextureCoordinates.Length; baseVertexIndex++)
                {
                    horizontalVertexOffset = (iWidth * HORIZONTALVERTEXSTEP) + _hexagonBaseTextureCoordinates[baseVertexIndex].x;
                    verticalVertexOffset = (iLength * VERTICALVERTEXSTEP) + _hexagonBaseTextureCoordinates[baseVertexIndex].y;

                    if (hexagonVerticalStep == true)
                    {
                        verticalVertexOffset += 1;
                    }

                    u = horizontalVertexOffset / _gridVertexBoundaryHorizontal;
                    v = verticalVertexOffset / _gridVertexBoundaryVertical;

                    textureCoordinates[uvIndex] = new Vector2(u, v);
                    uvIndex += 1;
                }

                hexagonVerticalStep = !hexagonVerticalStep;
            }
        }

        return textureCoordinates;
    }

    /// <summary>
    /// Returns a Vector2 array containing the base vertices for a texture mapping
    /// </summary>
    /// <returns>Vector2[]</returns>
    private Vector2[] GenerateHexagonBaseTextureCoordinates()
    {
        // vertices which create a base hexagon
        Vector2[] hexagonBaseVertices = new Vector2[7];

        hexagonBaseVertices[0] = new Vector2(2f, 1f);
        hexagonBaseVertices[1] = new Vector2(1f, 0f);
        hexagonBaseVertices[2] = new Vector2(0f, 1f);
        hexagonBaseVertices[3] = new Vector2(1f, 2f);
        hexagonBaseVertices[4] = new Vector2(3f, 2f);
        hexagonBaseVertices[5] = new Vector2(4f, 1f);
        hexagonBaseVertices[6] = new Vector2(3f, 0f);

        return hexagonBaseVertices;
    }
}