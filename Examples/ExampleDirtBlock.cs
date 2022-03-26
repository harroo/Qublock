
using UnityEngine;

using Qublock.Core;
using Qublock.Meshing;

public static partial class BlockId {

    public const ushort Dirt = 7;
}

public class Dirt : BlockData {

    public override ushort Id => BlockId.Dirt;

    public override string Name => "Dirt Block";

    public override bool Erodable => false;
    public override bool Flamable => false;
    public override bool Solid => true;

    public override MeshUVSet UVSet => new MeshUVSet {
        PX_X = 1,
        PX_Y = 1,

        NX_X = 1,
        NX_Y = 1,

        PY_X = 1,
        PY_Y = 1,

        NY_X = 1,
        NY_Y = 1,

        PZ_X = 1,
        PZ_Y = 1,

        NZ_X = 1,
        NZ_Y = 1,
    };

    public override void Draw (Chunk chunk, int x, int y, int z) {

        BaseMeshing.Block(chunk, x, y, z, UVSet);
    }
}
