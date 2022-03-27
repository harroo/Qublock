
# Qublock

A masterful Voxel-Engine that is both Cubic and Linear.

The Documentation is rather minimal..

## Data Structures

`Qublock.Data.Storage.Structures`

### `ChunkLoc`
Constructed from an `int, int, int`, (x, y, z).

Contains `ChunkLoc.FromWorldPos(int x, int y, int z)` which return a `ChunkLoc`
that is calculated from the world position provided within it.

__Usage__: `ChunkLoc` is used to identify and store positions of Chunks.

### `GridCell`
Constructed from `int, int, int, ushort`, (x, y, z, id).

Contains `GridCell.Zero` which is a `static GridCell` that is equivalent to 0.

__Usage__: `GridCell` is used to store a world position and the `BlockId` of that
position.

### `GridPos`
The same as `GridCell` however just without the `BlockId` or the `.Zero`.

__Usage__: Used to store a "World-Position". Such as, I am at `GridPos(x, y, z)`.

### `RegionLoc`
Exactly the same as `ChunkLoc` just used for Regions, which contain Chunks just as
Chunks contain Blocks.

## Entity Blocks

Create a Tag called "Entity Block" and apply it to all your Entity Game-Objects.

Your __Game-Object__ should have both the `SyncToOrigin` Mono-Behaviour and a Behaviour
inheriting from the `EntityBlockBehaviour` class, which inherits from `MonoBehaviour`
and adds only `OnLeftClick()` and `OnRightClick()` functions. Which are called via
the Entity-Block system itself.
See [Examples/EntityBlockExample.cs](https://github.com/harroo/Qublock/tree/main/Examples/EntityBlockExample.cs)

The __Block-Data__ should inherit from `EntityBlockBase`, which inherits from `BlockData`
and contains pre-configured `Draw` and `SingleVoxelDraw` method overrides suited
for Entity-Blocks.

The `EntityBlockManager` behaviour should be present in the scene somewhere. It
should have in its lists a value containing the `BlockId` of the Entity-Block's
Id, and a Game-Object(prefab) reference.

## Dynamic Lighting

The Dynamic Lighting-System uses real-time lighting.

## More ..

If you wish to know more, simply read the specific files within [Source/](https://github.com/harroo/Qublock/tree/main/Source/),
or review the files in [Examples/](https://github.com/harroo/Qublock/tree/main/Examples/)

---

Spelling and Orthography correction: [Kieralia](https://github.com/kieralia)
