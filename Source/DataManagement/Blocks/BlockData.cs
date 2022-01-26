
using UnityEngine;

using Qublock.Core;
using Qublock.Meshing;

public static partial class BlockId {

    public const ushort Unknown = 404;
}

public class BlockData {

    public virtual ushort Id => BlockId.Unknown;

    public virtual string Name => "Unknown Block";

    public virtual bool Erodable => false;
    public virtual bool Flamable => false;
    public virtual bool Solid => true;
    public virtual bool Selectable => true;

    public virtual MeshUVSet UVSet => new MeshUVSet {
        PX_X = 0,
        PX_Y = 0,

        NX_X = 0,
        NX_Y = 0,

        PY_X = 0,
        PY_Y = 0,

        NY_X = 0,
        NY_Y = 0,

        PZ_X = 0,
        PZ_Y = 0,

        NZ_X = 0,
        NZ_Y = 0,
    };

    public virtual void Draw (Chunk chunk, int x, int y, int z) {

        BaseMeshing.Block(chunk, x, y, z, UVSet);
    }

    public virtual void SingleVoxelDraw (Mesh mesh) {

        SVBaseMeshing.Block(mesh, UVSet);
    }
}
