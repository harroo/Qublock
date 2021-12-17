
using UnityEngine;

using System.Collections.Generic;

using Qublock.Data.Storage.Structures;
using Qublock.FloatingOrigin;
using Qublock.ChunkLoading;
using Qublock.Core;

public class LinearChunkLoader : MonoBehaviour {

    public int renderDistance, stackHeight;

    private List<ChunkLoc> buildList = new List<ChunkLoc>();
    private List<ChunkLoc> renderList = new List<ChunkLoc>();
    private List<ChunkLoc> destroyList = new List<ChunkLoc>();

    private List<ChunkLoc> activeList = new List<ChunkLoc>();

    private ChunkLoc[] chunkPositions;

    private void Start () {

        GenerateChunkPositions();
    }

    public void GenerateChunkPositions () {

        int x = 0, z = 0, dx = 0, dz = -1;
        int maxI = (renderDistance * 10) + 1;

        chunkPositions = new ChunkLoc[maxI];

        for (i = 0; i < maxI; i++) {

            if ((-renderDistance/2 <= x) && (x <= renderDistance/2) && (-renderDistance/2 <= z) && (z <= renderDistance/2)) {

                chunkPositions[i] = new ChunkLoc(x, 0, z);
            }

            if ((x == z) || ((x < 0) && (x == -z)) || ((x > 0) && (x == 1-z))) {

                int tmp = dx;
                dx = -dz;
                dz = tmp;
            }

            x += dx;
            z += dz;
        }
    }

    private int timer = 0;
    private int i, j;

    private void Update () {

        if (timer == 10) {

            ProximityCheck();
            timer = 0;
            return;
        }
        ++timer;

        if (BuildAndRender())
            return;

        LookForChunksToBuild();
    }

    private void BuildStack (ChunkLoc loc) {

        activeList.Add(loc);

        ChunkLoc nloc;

        for (i = 0; i < stackHeight; ++i) {

            nloc = new ChunkLoc(loc.X, i, loc.Z);

            World.CreateChunk(nloc);
            ChunkFetcher.FetchChunk(nloc);
        }
    }

    private void RenderStack (ChunkLoc loc) {

        ChunkLoc nloc;

        for (i = 0; i < stackHeight; ++i) {

            nloc = new ChunkLoc(loc.X, i, loc.Z);

            World.RenderChunk(nloc);
        }
    }

    private void DestroyStack (ChunkLoc loc) {

        activeList.Remove(loc);

        ChunkLoc nloc;

        for (i = 0; i < stackHeight; ++i) {

            nloc = new ChunkLoc(loc.X, i, loc.Z);

            World.DestroyChunk(nloc);
        }
    }

    private void ProximityCheck () {

        Vector3 playerPos = new Vector3(
            (transform.position.x / 32) - Origin.chunkX, 0,
            (transform.position.z / 32) - Origin.chunkZ
        );

        destroyList.Clear();

        foreach (ChunkLoc loc in activeList) {

            if ((playerPos - new Vector3(loc.X, 0, loc.Z)).magnitude > renderDistance) {

                destroyList.Add(loc);
            }
        }

        while (destroyList.Count != 0) {

            DestroyStack(destroyList[0]);
            destroyList.RemoveAt(0);
        }
    }

    private bool BuildAndRender () {

        if (buildList.Count != 0) {

            for (i = 0; i < buildList.Count && i < 4; ++i) {

                if (World.ChunkLoaded(buildList[0])) {

                    buildList.RemoveAt(0); continue;
                }

                BuildStack(buildList[0]);
                buildList.RemoveAt(0);
            }

            return true;
        }

        if (ChunkFetcher.FetchedCount() != 0) {

            for (i = 0; i < ChunkFetcher.FetchedCount(); ++i) {

                FetchedChunk fetchedChunk = ChunkFetcher.PopFetchedChunk();

                if (!World.ChunkLoaded(fetchedChunk.loc)) continue;

                World.data.chunks[fetchedChunk.loc].SetValues(fetchedChunk.values);
            }

            return true;
        }

        if (renderList.Count != 0) {

            for (i = 0; i < renderList.Count; ++i) {

                if (!World.ChunkLoaded(renderList[0])) {

                    renderList.RemoveAt(0); continue;
                }

                RenderStack(renderList[0]);
                renderList.RemoveAt(0);
            }

            return true;
        }

        return false;
    }

    private void LookForChunksToBuild () {

        int playerX = (int)transform.position.x >> 5;
        int playerZ = (int)transform.position.z >> 5;

        playerX -= Origin.chunkX;
        playerZ -= Origin.chunkZ;

        ChunkLoc loc;

        for (i = 0; i < chunkPositions.Length; ++i) {

            loc = new ChunkLoc(
                chunkPositions[i].X + playerX, stackHeight - 1,
                chunkPositions[i].Z + playerZ
            );

            if (!activeList.Contains(loc)) {

                buildList.Add(loc);
                return;

            } else if (World.ChunkFilled(loc) && !World.ChunkRendered(loc)) {

                if (!ChunkIsSurrounded(loc.X, loc.Y, loc.Z)) continue;

                renderList.Add(loc);
                return;
            }
        }
    }

    private bool ChunkIsSurrounded (int xPos, int yPos, int zPos)
        => (World.ChunkFilled(new ChunkLoc(xPos + 1, yPos, zPos)) &&
            World.ChunkFilled(new ChunkLoc(xPos - 1, yPos, zPos)) &&
            World.ChunkFilled(new ChunkLoc(xPos, yPos, zPos + 1)) &&
            World.ChunkFilled(new ChunkLoc(xPos, yPos, zPos - 1)));
}
