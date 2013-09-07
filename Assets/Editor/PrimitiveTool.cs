using UnityEngine;
using UnityEditor;

public static class PrimitiveTool
{
    [MenuItem("Extra/Create Cube Mesh Asset")]
    static void CreateCubeMeshAsset ()
    {
        var mesh = new Mesh ();
        mesh.subMeshCount = 2;

        var vertices = new Vector3 [4 * 6];

        vertices [0] = new Vector3 (-1, +1, -1);
        vertices [1] = new Vector3 (+1, +1, -1);
        vertices [2] = new Vector3 (+1, -1, -1);
        vertices [3] = new Vector3 (-1, -1, -1);

        vertices [4] = new Vector3 (+1, +1, -1);
        vertices [5] = new Vector3 (+1, +1, +1);
        vertices [6] = new Vector3 (+1, -1, +1);
        vertices [7] = new Vector3 (+1, -1, -1);

        vertices [8] = new Vector3 (+1, +1, +1);
        vertices [9] = new Vector3 (-1, +1, +1);
        vertices [10] = new Vector3 (-1, -1, +1);
        vertices [11] = new Vector3 (+1, -1, +1);

        vertices [12] = new Vector3 (-1, +1, +1);
        vertices [13] = new Vector3 (-1, +1, -1);
        vertices [14] = new Vector3 (-1, -1, -1);
        vertices [15] = new Vector3 (-1, -1, +1);

        vertices [16] = new Vector3 (-1, +1, +1);
        vertices [17] = new Vector3 (+1, +1, +1);
        vertices [18] = new Vector3 (+1, +1, -1);
        vertices [19] = new Vector3 (-1, +1, -1);

        vertices [20] = new Vector3 (-1, -1, -1);
        vertices [21] = new Vector3 (+1, -1, -1);
        vertices [22] = new Vector3 (+1, -1, +1);
        vertices [23] = new Vector3 (-1, -1, +1);

        mesh.vertices = vertices;

        var indices = new int[4 * 6];
        for (var i = 0; i < indices.Length; i++) {
            indices [i] = i;
        }
        mesh.SetIndices (indices, MeshTopology.Quads, 0);

        indices = new int[4 * 3 * 2];
        var offs = 0;
        for (var i1 = 0; i1 < 16; i1 += 4) {
            for (var i2 = i1; i2 < i1 + 3; i2++) {
                indices [offs++] = i2;
                indices [offs++] = i2 + 1;
            }
        }
        mesh.SetIndices (indices, MeshTopology.Lines, 1);

        mesh.Optimize ();
        mesh.RecalculateNormals ();

        AssetDatabase.CreateAsset (mesh, "Assets/Cube.asset");
        AssetDatabase.ImportAsset ("Assets/Cube.asset");
    }
}