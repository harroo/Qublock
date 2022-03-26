
using System;
using System.Collections.Generic;

using UnityEngine;

using Qublock.Core;
using Qublock.Data.Blocks;
using Qublock.Data.Storage.Structures;
using Qublock.FloatingOrigin;

public class EntityBlockManager : MonoBehaviour {

    public static EntityBlockManager instance;
    private void Awake () { instance = this; }

    public Dictionary<GridPos, EntityBlockNode> entityNodes
        = new Dictionary<GridPos, EntityBlockNode>();

    public Queue<GridPos> entityBlockRemovalQueue = new Queue<GridPos>();
    public Queue<GridCell> entityBlockCreationQueue = new Queue<GridCell>();

    public static void RemoveEntityBlock (int x, int y, int z) {

        instance.entityBlockRemovalQueue.Enqueue(new GridPos(x, y, z));
    }

    public static void AddEntityBlock (int x, int y, int z, ushort id) {

        instance.entityBlockCreationQueue.Enqueue(new GridCell(x, y, z, id));
    }

    private int timer;
    private void Update () {

        if (timer == 24) { timer = 0;

            CheckForIdChanges(); return;

        } else timer++;

        RemoveEntityBlocks();
        CreateEntityBlocks();
    }

    private void CheckForIdChanges () {

        foreach (EntityBlockNode node in entityNodes.Values) {

            if (World.data[node.position.x, node.position.y, node.position.z] == node.blockId)
                continue;

            entityBlockRemovalQueue.Enqueue(node.position);
        }
    }

    private void RemoveEntityBlocks () {

        while (entityBlockRemovalQueue.Count != 0) {

            GridPos removalPos = entityBlockRemovalQueue.Dequeue();

            if (!entityNodes.ContainsKey(removalPos)) continue;

            Destroy(entityNodes[removalPos].entityObject);
            entityNodes.Remove(removalPos);
        }
    }

    public EntityBlockEntry[] entityBlockEntries;

    private void CreateEntityBlocks () {

        while (entityBlockCreationQueue.Count != 0) {

            GridCell creationCell = entityBlockCreationQueue.Dequeue();
            GridPos pos = new GridPos(creationCell.x, creationCell.y, creationCell.z);

            if (entityNodes.ContainsKey(pos)) continue;

            EntityBlockEntry entityObject = Array.Find(entityBlockEntries, e => e.blockIdKey == creationCell.id);
            if (entityObject == null) continue;

            EntityBlockNode node = new EntityBlockNode();
            node.blockId = creationCell.id;
            node.position = pos;
            node.entityObject = Instantiate(
                entityObject.entityPrefab,
                Origin.OffsetToUnity(new Vector3(creationCell.x, creationCell.y, creationCell.z)),
                Quaternion.identity
            );

            entityNodes.Add(pos, node);
        }
    }
}

public class EntityBlockNode {

    public ushort blockId;

    public GridPos position;

    public GameObject entityObject;
}

[Serializable]
public class EntityBlockEntry {

    public ushort blockIdKey;

    public GameObject entityPrefab;
}
