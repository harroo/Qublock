
using UnityEngine;

using System;
using System.Reflection;
using System.Collections.Generic;

namespace Qublock.Data.Blocks {

    public static class Argosy {

        [RuntimeInitializeOnLoadMethod]
        public static void InitializeBlockData () {

            List<BlockData> blockList = new List<BlockData>();

            blockList.Add(new BlockData());

            foreach (Type type in Assembly.GetAssembly(typeof(BlockData)).GetTypes()) {

        		if (type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(BlockData))) {

                    blockList.Add((BlockData)Activator.CreateInstance(type));
                }
            }

            blockDataBuffer = blockList.ToArray();

            Debug.Log("Total Blocks Loaded: " + blockList.Count.ToString());
        }

        public static BlockData Get (ushort blockID) {

            if (blockData.ContainsKey(blockID))
                return blockData[blockID];
            else
                return new BlockData();
        }

        public static BlockData[] blockDataBuffer;

        public static void InitializeBlocks () {

            if (blockData.Count != 0) return;

            for (int i = 0; i < blockDataBuffer.Length; ++i) {

                blockData.Add(blockDataBuffer[i].Id, blockDataBuffer[i]);
            }

            blockDataBuffer = new BlockData[0];
        }

        public static Dictionary<ushort, BlockData> blockData
            = new Dictionary<ushort, BlockData>();
    }
}
