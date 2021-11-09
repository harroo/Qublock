
using UnityEngine;

using Qublock.Core;
using Qublock.Meshing;

public static partial class BaseMeshing {

    public static void Block (Chunk chunk, int x, int y, int z, MeshUVSet uvSet) {
        if (!chunk.IsSolid(x + 1, y, z)) {
            chunk.visuVertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            chunk.visuVertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            chunk.visuVertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            chunk.visuVertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            chunk.CalculateTriangles();
            chunk.collVertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            chunk.collVertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            chunk.collVertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            chunk.collVertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            chunk.CalculateCollTriangles();
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.PX_X + tileSize, tileSize * uvSet.PX_Y));
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.PX_X + tileSize, tileSize * uvSet.PX_Y + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.PX_X, tileSize * uvSet.PX_Y + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.PX_X, tileSize * uvSet.PX_Y));
        }
        if (!chunk.IsSolid(x - 1, y, z)) {
            chunk.visuVertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
            chunk.visuVertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            chunk.visuVertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            chunk.visuVertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            chunk.CalculateTriangles();
            chunk.collVertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
            chunk.collVertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            chunk.collVertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            chunk.collVertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            chunk.CalculateCollTriangles();
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.NX_X + tileSize, tileSize * uvSet.NX_Y));
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.NX_X + tileSize, tileSize * uvSet.NX_Y + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.NX_X, tileSize * uvSet.NX_Y + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.NX_X, tileSize * uvSet.NX_Y));
        }
        if (!chunk.IsSolid(x, y + 1, z)) {
            chunk.visuVertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            chunk.visuVertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            chunk.visuVertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            chunk.visuVertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            chunk.CalculateTriangles();
            chunk.collVertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            chunk.collVertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            chunk.collVertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            chunk.collVertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            chunk.CalculateCollTriangles();
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.PY_X + tileSize, tileSize * uvSet.PY_Y));
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.PY_X + tileSize, tileSize * uvSet.PY_Y + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.PY_X, tileSize * uvSet.PY_Y + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.PY_X, tileSize * uvSet.PY_Y));
        }
        if (!chunk.IsSolid(x, y - 1, z)) {
            chunk.visuVertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            chunk.visuVertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            chunk.visuVertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            chunk.visuVertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
            chunk.CalculateTriangles();
            chunk.collVertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            chunk.collVertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            chunk.collVertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            chunk.collVertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
            chunk.CalculateCollTriangles();
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.NY_X + tileSize, tileSize * uvSet.NY_Y));
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.NY_X + tileSize, tileSize * uvSet.NY_Y + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.NY_X, tileSize * uvSet.NY_Y + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.NY_X, tileSize * uvSet.NY_Y));
        }
        if (!chunk.IsSolid(x, y, z + 1)) {
            chunk.visuVertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            chunk.visuVertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            chunk.visuVertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            chunk.visuVertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
            chunk.CalculateTriangles();
            chunk.collVertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            chunk.collVertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            chunk.collVertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            chunk.collVertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
            chunk.CalculateCollTriangles();
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.PZ_X + tileSize, tileSize * uvSet.PZ_Y));
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.PZ_X + tileSize, tileSize * uvSet.PZ_Y + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.PZ_X, tileSize * uvSet.PZ_Y + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.PZ_X, tileSize * uvSet.PZ_Y));
        }
        if (!chunk.IsSolid(x, y, z - 1)) {
            chunk.visuVertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            chunk.visuVertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            chunk.visuVertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            chunk.visuVertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            chunk.CalculateTriangles();
            chunk.collVertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            chunk.collVertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            chunk.collVertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            chunk.collVertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            chunk.CalculateCollTriangles();
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.NZ_X + tileSize, tileSize * uvSet.NZ_Y));
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.NZ_X + tileSize, tileSize * uvSet.NZ_Y + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.NZ_X, tileSize * uvSet.NZ_Y + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * uvSet.NZ_X, tileSize * uvSet.NZ_Y));
        }
    }
}

public static partial class SVBaseMeshing {

    public static void Block (Mesh mesh, MeshUVSet uvSet) {

        mesh.vertices = new Vector3[] {

            // PX Face.
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(0.5f,  0.5f, -0.5f),
            new Vector3(0.5f,  0.5f,  0.5f),
            new Vector3(0.5f, -0.5f,  0.5f),

            // NX Face.
            new Vector3(-0.5f, -0.5f,  0.5f),
            new Vector3(-0.5f,  0.5f,  0.5f),
            new Vector3(-0.5f,  0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),

            // PY Face.
            new Vector3(-0.5f, 0.5f,  0.5f),
            new Vector3( 0.5f, 0.5f,  0.5f),
            new Vector3( 0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),

            // NY Face.
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3( 0.5f, -0.5f, -0.5f),
            new Vector3( 0.5f, -0.5f,  0.5f),
            new Vector3(-0.5f, -0.5f,  0.5f),

            // PZ Face.
            new Vector3( 0.5f, -0.5f, 0.5f),
            new Vector3( 0.5f,  0.5f, 0.5f),
            new Vector3(-0.5f,  0.5f, 0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),

            // NZ Face.
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f,  0.5f, -0.5f),
            new Vector3( 0.5f,  0.5f, -0.5f),
            new Vector3( 0.5f, -0.5f, -0.5f),
        };

        mesh.triangles = new int[] {

            // PX Face.
            0, 1, 2,  0, 2, 3,

            // NX Face.
            4, 5, 6,  4, 6, 7,

            // PY Face.
            8, 9, 10,  8, 10, 11,

            // NY Face.
            12, 13, 14,  12, 14, 15,

            // PZ Face.
            16, 17, 18,  16, 18, 19,

            // NZ Face.
            20, 21, 22,  20, 22, 23,
        };

        mesh.uv = new Vector2[] {

            // PX Face.
            new Vector2(tileSize * uvSet.PX_X + tileSize, tileSize * uvSet.PX_Y),
            new Vector2(tileSize * uvSet.PX_X + tileSize, tileSize * uvSet.PX_Y + tileSize),
            new Vector2(tileSize * uvSet.PX_X, tileSize * uvSet.PX_Y + tileSize),
            new Vector2(tileSize * uvSet.PX_X, tileSize * uvSet.PX_Y),

            // NX Face.
            new Vector2(tileSize * uvSet.NX_X + tileSize, tileSize * uvSet.NX_Y),
            new Vector2(tileSize * uvSet.NX_X + tileSize, tileSize * uvSet.NX_Y + tileSize),
            new Vector2(tileSize * uvSet.NX_X, tileSize * uvSet.NX_Y + tileSize),
            new Vector2(tileSize * uvSet.NX_X, tileSize * uvSet.NX_Y),

            // PY Face.
            new Vector2(tileSize * uvSet.PY_X + tileSize, tileSize * uvSet.PY_Y),
            new Vector2(tileSize * uvSet.PY_X + tileSize, tileSize * uvSet.PY_Y + tileSize),
            new Vector2(tileSize * uvSet.PY_X, tileSize * uvSet.PY_Y + tileSize),
            new Vector2(tileSize * uvSet.PY_X, tileSize * uvSet.PY_Y),

            // NY Face.
            new Vector2(tileSize * uvSet.NY_X + tileSize, tileSize * uvSet.NY_Y),
            new Vector2(tileSize * uvSet.NY_X + tileSize, tileSize * uvSet.NY_Y + tileSize),
            new Vector2(tileSize * uvSet.NY_X, tileSize * uvSet.NY_Y + tileSize),
            new Vector2(tileSize * uvSet.NY_X, tileSize * uvSet.NY_Y),

            // PZ Face.
            new Vector2(tileSize * uvSet.PZ_X + tileSize, tileSize * uvSet.PZ_Y),
            new Vector2(tileSize * uvSet.PZ_X + tileSize, tileSize * uvSet.PZ_Y + tileSize),
            new Vector2(tileSize * uvSet.PZ_X, tileSize * uvSet.PZ_Y + tileSize),
            new Vector2(tileSize * uvSet.PZ_X, tileSize * uvSet.PZ_Y),

            // NZ Face.
            new Vector2(tileSize * uvSet.NZ_X + tileSize, tileSize * uvSet.NZ_Y),
            new Vector2(tileSize * uvSet.NZ_X + tileSize, tileSize * uvSet.NZ_Y + tileSize),
            new Vector2(tileSize * uvSet.NZ_X, tileSize * uvSet.NZ_Y + tileSize),
            new Vector2(tileSize * uvSet.NZ_X, tileSize * uvSet.NZ_Y),
        };
    }
}
