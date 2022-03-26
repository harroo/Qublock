
using UnityEngine;

using Qublock.Core;
using Qublock.Meshing;

public static partial class BlockId {

    public const ushort Air = 0;
}

public class Air : BlockData {

    public override ushort Id => BlockId.Air;

    public override string Name => "Air Block";

    public override bool Erodable => false;
    public override bool Flamable => false;
    public override bool Solid => false;
    public override bool Selectable => false;

    public override MeshUVSet UVSet => new MeshUVSet {
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

    public override void Draw (Chunk chunk, int x, int y, int z) {

        return;
    }
}
