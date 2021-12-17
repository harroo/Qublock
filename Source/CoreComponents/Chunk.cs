
using UnityEngine;

using System;
using System.Threading;
using System.Collections.Generic;

using Qublock.Data.Storage.Structures;
using Qublock.Data.Storage.Containers;
using Qublock.Data.Blocks;
using Qublock.FloatingOrigin;

namespace Qublock.Core {

    public class Chunk {

        private ushort[] values = new ushort[32 * 32 * 32];

        public ushort this[int x, int y, int z] {

            get {

                return values[x * 32 * 32 + y * 32 + z];
            }

    	    set {

        		values[x * 32 * 32 + y * 32 + z] = value;
    	    }
        }

    	public ushort[] GetValues ()
    		=> values;

    	public bool valuesSet = false;

    	public void SetValues (ushort[] newValues) {

    		values = newValues;

    		valuesSet = true;
    	}

        public ChunkData GetChunkData ()
            => new ChunkData(values);

        public void SetChunkData (ChunkData chunkData) {

            values = chunkData.GetValues();
        }

    	public bool Contains (ushort blockId)
            => Array.IndexOf(values, blockId) > -1;

        public Chunk (ChunkLoc loc, Material material, Material fadeMaterial) {

            location = loc;
            position = new GridPos(loc.X * 32, loc.Y * 32, loc.Z * 32);

            // chunkObject = new GameObject();
            // chunkObject = new GameObject("Chunk " + loc.X + ":" + loc.Y + ":" + loc.Z);
            chunkObject = new GameObject(loc.X + ":" + loc.Y + ":" + loc.Z);
            chunkObject.transform.position = Origin.OffsetToUnity(new Vector3(
                position.x, position.y, position.z
            ));

            visuMesh = new Mesh();
            collMesh = new Mesh();
            fadeMesh = new Mesh();

            visuMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            collMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

            chunkObject.AddComponent<MeshFilter>().mesh = visuMesh;
            chunkObject.AddComponent<MeshRenderer>().material = material;

            meshCollider = chunkObject.AddComponent<MeshCollider>();

            fadeChunkObject = new GameObject();
            fadeChunkObject.transform.position = chunkObject.transform.position;
            fadeChunkObject.transform.SetParent(chunkObject.transform);

            fadeChunkObject.AddComponent<MeshFilter>().mesh = fadeMesh;
            fadeChunkObject.AddComponent<MeshRenderer>().material = fadeMaterial;
        }

        public ChunkLoc location;
        public GridPos position;

        public GameObject chunkObject;
        public GameObject fadeChunkObject;

        private MeshCollider meshCollider;

        private Mesh visuMesh;
        private Mesh collMesh;
        private Mesh fadeMesh;

        public List<Vector3> visuVertices = new List<Vector3>();
        public List<int> visuTriangles = new List<int>();
        public List<Vector2> visuUVs = new List<Vector2>();

        public List<Vector3> collVertices = new List<Vector3>();
        public List<int> collTriangles = new List<int>();

        public List<Vector3> fadeVertices = new List<Vector3>();
        public List<int> fadeTriangles = new List<int>();
        public List<Vector2> fadeUVs = new List<Vector2>();

        public void CalculateTriangles () {

            visuTriangles.Add(visuVertices.Count - 4);
            visuTriangles.Add(visuVertices.Count - 3);
            visuTriangles.Add(visuVertices.Count - 2);
            visuTriangles.Add(visuVertices.Count - 4);
            visuTriangles.Add(visuVertices.Count - 2);
            visuTriangles.Add(visuVertices.Count - 1);
        }

        public void CalculateCollTriangles () {

            collTriangles.Add(collVertices.Count - 4);
            collTriangles.Add(collVertices.Count - 3);
            collTriangles.Add(collVertices.Count - 2);
            collTriangles.Add(collVertices.Count - 4);
            collTriangles.Add(collVertices.Count - 2);
            collTriangles.Add(collVertices.Count - 1);
        }

        public void CalculateFadeTriangles () {

            fadeTriangles.Add(fadeVertices.Count - 4);
            fadeTriangles.Add(fadeVertices.Count - 3);
            fadeTriangles.Add(fadeVertices.Count - 2);
            fadeTriangles.Add(fadeVertices.Count - 4);
            fadeTriangles.Add(fadeVertices.Count - 2);
            fadeTriangles.Add(fadeVertices.Count - 1);
        }

        public void Clear () {

            visuVertices.Clear();
            visuTriangles.Clear();
            visuUVs.Clear();

            collVertices.Clear();
            collTriangles.Clear();

            fadeVertices.Clear();
            fadeTriangles.Clear();
            fadeUVs.Clear();
        }

        public void Erase () {

            Clear();

            visuMesh.Clear();
            collMesh.Clear();
            fadeMesh.Clear();

            UnityEngine.Object.Destroy(chunkObject);
            UnityEngine.Object.Destroy(fadeChunkObject);

            values = new ushort[0];
        }

        public bool rendered = false;

        private Mutex mutex = new Mutex();

        public void Render () {

        	mutex.WaitOne(); try {

    		    Clear();

    		    for (int x = 0; x < 32; ++x) {

    		        for (int y = 0; y < 32; ++y) {

    		            for (int z = 0; z < 32; ++z) {

    		                Argosy.Get(this[x, y, z]).Draw(this, x, y, z);
    		            }
    		        }
    		    }

            } finally { mutex.ReleaseMutex(); }
        }

        public void Asign () {

            mutex.WaitOne(); try {

    		    visuMesh.Clear();
    		    visuMesh.vertices = visuVertices.ToArray();
    		    visuMesh.triangles = visuTriangles.ToArray();
    		    visuMesh.uv = visuUVs.ToArray();
    		    visuMesh.RecalculateNormals();

    		    collMesh.Clear();
    		    collMesh.vertices = collVertices.ToArray();
    		    collMesh.triangles = collTriangles.ToArray();
    		    meshCollider.sharedMesh = collMesh;

                fadeMesh.Clear();
                fadeMesh.vertices = fadeVertices.ToArray();
                fadeMesh.triangles = fadeTriangles.ToArray();
                fadeMesh.uv = fadeUVs.ToArray();
                fadeMesh.RecalculateNormals();
                fadeChunkObject.SetActive(fadeVertices.Count != 0);

    		    rendered = true;

            } finally { mutex.ReleaseMutex(); }
        }

        public bool IsSolid (int x, int y, int z) {

            if (x >= 0 && x < 32 && y >= 0 && y < 32 && z >= 0 && z < 32) {

                return Argosy.Get(this[x, y, z]).Solid;

            } else {

                return Argosy.Get(World.data[position.x + x, position.y + y, position.z + z]).Solid;
            }
        }

        public bool IsId (int x, int y, int z, int id) {

            if (x >= 0 && x < 32 && y >= 0 && y < 32 && z >= 0 && z < 32) {

                return this[x, y, z] == id;

            } else {

                return World.data[position.x + x, position.y + y, position.z + z] == id;
            }
        }
    }
}
