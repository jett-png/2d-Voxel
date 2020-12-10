using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk
{
    VoxelManager VM;

    public int[,] voxels;

    Vector2Int chunkCords;


    public void Initialize(VoxelManager _VM, Vector2Int _chunkCords, int[,] _voxels)
    {
        VM = _VM;

        chunkCords = _chunkCords;

        if (_voxels != null)
            voxels = _voxels;
        else
        {
            voxels = new int[VM.chunkSize.x, VM.chunkSize.y];
            GenerateChunk();
        }
    }


    private Vector2Int CordsToPos(Vector2Int cords)
    {
        Vector2Int pos = -cords + VM.chunkSize - new Vector2Int(1, 1);
        pos += chunkCords * VM.chunkSize;
        pos -= (VM.worldSize / 2) * VM.chunkSize;

        return pos;
    }


    public void GenerateChunk()
    {
        for (int y = 0; y < VM.chunkSize.y; y++)
        {
            for (int x = 0; x < VM.chunkSize.x; x++)
            {
                voxels[x, y] = VM.AssignMat(CordsToPos(new Vector2Int(x, y)));
            }
        }
    }


    public void DrawChunk()
    {
        int mat = 0;

        for (int y = 0; y < VM.chunkSize.y; y++)
        {
            for (int x = 0; x < VM.chunkSize.x; x++)
            {
                mat = voxels[x, y];

                if (mat != 0)
                {
                    if (VisibleCheck(new Vector2Int(x, y)))
                    {
                        VM.baseMap.SetTile((Vector3Int)CordsToPos(new Vector2Int(x, y)), VM.materials[mat]);
                    }
                }
            }
        }
    }


    //checks if a called voxel is visible and should be drawn
    private bool VisibleCheck(Vector2Int cords)
    {
        bool chunkEdge = (cords.x == 0 || cords.x == VM.chunkSize.x - 1) || (cords.y == 0 || cords.y == VM.chunkSize.y - 1);

        //if this voxel is on the edge of the chunk, draw it
        if (chunkEdge)
        {
            return true;
            //the voxel is drawn in these situations because drawing 100
            //spair voxels is less proccessing power than communicating
            //between chunks to find out if it is visible
        }

        //call all this voxel's surrounding voxels
        for (int p = 0; p < 8; p++)
        {
            Vector2Int pPos = cords + VM.touchingPos[p];

            //if voxel at pPos is air, this voxel is visible
            if (voxels[pPos.x, pPos.y] == 0) return true;
        }

        //this code is only run if the voxel is not visible );
        return false;
    }
}

//data for the save files
public struct ChunkData
{
    public int[,] voxels;
}