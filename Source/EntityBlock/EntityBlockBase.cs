
using UnityEngine;

using Qublock.Core;
using Qublock.Meshing;

public static partial class BlockId {

    public const ushort UnknownEntityBlock = 4042;
}

public class EntityBlockBase : BlockData {

    public override ushort Id => BlockId.UnknownEntityBlock;

    public override string Name => "Unknown Entity Block";

    public override void Draw (Chunk chunk, int x, int y, int z) {

        EntityBlockManager.AddEntityBlock(
        	chunk.position.x + x,
        	chunk.position.y + y,
        	chunk.position.z + z,
        	Id
        );
    }

    public override void SingleVoxelDraw (Mesh mesh) {

        return;
    }
}
