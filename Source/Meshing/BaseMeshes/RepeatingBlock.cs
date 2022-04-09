
using UnityEngine;

using Qublock.Core;
using Qublock.Meshing;

public static partial class BaseMeshing {

    public static void RepeatingBlock (Chunk chunk, int x, int y, int z, MeshUVSet uvSet) {
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
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.PX_X + (-z & 0x7)) + tileSize, tileSize * (uvSet.PX_Y + (y & 0x7))));
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.PX_X + (-z & 0x7)) + tileSize, tileSize * (uvSet.PX_Y + (y & 0x7)) + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.PX_X + (-z & 0x7)), tileSize * (uvSet.PX_Y + (y & 0x7)) + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.PX_X + (-z & 0x7)), tileSize * (uvSet.PX_Y + (y & 0x7))));
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
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.NX_X + (z & 0x7)) + tileSize, tileSize * (uvSet.NX_Y + (y & 0x7))));
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.NX_X + (z & 0x7)) + tileSize, tileSize * (uvSet.NX_Y + (y & 0x7)) + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.NX_X + (z & 0x7)), tileSize * (uvSet.NX_Y + (y & 0x7)) + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.NX_X + (z & 0x7)), tileSize * (uvSet.NX_Y + (y & 0x7))));
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
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.PY_X + (z & 0x7)) + tileSize, tileSize * (uvSet.PY_Y + (x & 0x7))));
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.PY_X + (z & 0x7)) + tileSize, tileSize * (uvSet.PY_Y + (x & 0x7)) + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.PY_X + (z & 0x7)), tileSize * (uvSet.PY_Y + (x & 0x7)) + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.PY_X + (z & 0x7)), tileSize * (uvSet.PY_Y + (x & 0x7))));
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
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.NY_X + (-z & 0x7)) + tileSize, tileSize * (uvSet.NY_Y + (x & 0x7))));
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.NY_X + (-z & 0x7)) + tileSize, tileSize * (uvSet.NY_Y + (x & 0x7)) + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.NY_X + (-z & 0x7)), tileSize * (uvSet.NY_Y + (x & 0x7)) + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.NY_X + (-z & 0x7)), tileSize * (uvSet.NY_Y + (x & 0x7))));
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
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.PZ_X + (x & 0x7)) + tileSize, tileSize * (uvSet.PZ_Y + (y & 0x7))));
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.PZ_X + (x & 0x7)) + tileSize, tileSize * (uvSet.PZ_Y + (y & 0x7)) + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.PZ_X + (x & 0x7)), tileSize * (uvSet.PZ_Y + (y & 0x7)) + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.PZ_X + (x & 0x7)), tileSize * (uvSet.PZ_Y + (y & 0x7))));
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
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.NZ_X + (-x & 0x7)) + tileSize, tileSize * (uvSet.NZ_Y + (y & 0x7))));
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.NZ_X + (-x & 0x7)) + tileSize, tileSize * (uvSet.NZ_Y + (y & 0x7)) + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.NZ_X + (-x & 0x7)), tileSize * (uvSet.NZ_Y + (y & 0x7)) + tileSize));
            chunk.visuUVs.Add(new Vector2(tileSize * (uvSet.NZ_X + (-x & 0x7)), tileSize * (uvSet.NZ_Y + (y & 0x7))));
        }
    }
}
