using UnityEngine;
using System.Threading;

public static class WorldCreation
{
    #region Data
    public static BlockChunk[,] tempChunks;

    private static Vector2Int worldSize;
    private static Vector2Int chunkSize;
    #endregion


    #region Behavior
    public static void New(Vector2Int ws, Vector2Int cs)
    {
        worldSize = ws;
        chunkSize = cs;

        new Thread(CreateChunks).Start();
    }

    private static void CreateChunks()
    {
        tempChunks = new BlockChunk[worldSize.x, worldSize.y];

        for (int y = 0; y < worldSize.y; y++)
        {
            for (int x = 0; x < worldSize.x; x++)
            {
                BlockChunk c = new BlockChunk();
                c.x = x;
                c.y = y;
                c.blocks = new int[chunkSize.x, chunkSize.y];
                GenerateBlocks(c);

                tempChunks[x, y] = c;
            }
        }
    }

    private static void GenerateBlocks(BlockChunk c)
    {
        for (int y = 0; y < chunkSize.y; y++)
        {
            for (int x = 0; x < chunkSize.x; x++)
            {
                if(y == 0)
                    c.blocks[x, y] = 1;
            }
        }
    }
    #endregion
}

//  create world 50 50 32 32