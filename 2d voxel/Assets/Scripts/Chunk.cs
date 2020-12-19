using UnityEngine;

public class Chunk
{
    VoxelManager VM;

    public int[,] voxels;

    Vector2Int chunkCords;


    #region Chunk Creation

    //creates new chunk from new or save files
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


    //creates a new hypathetical chunk only visible in an array of ints
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


    //converts the chunk data into a physical chunk
    public void DrawChunk(int chunkNum)
    {
        int mat = 0;
        int[,] lightData = new int[VM.chunkSize.x, VM.chunkSize.y];

        for (int y = 0; y < VM.chunkSize.y; y++)
        {
            for (int x = 0; x < VM.chunkSize.x; x++)
            {
                mat = voxels[x, y];

                if (mat != 0)
                {
                    lightData[x, y] = 10;

                    //checks if the voxel is visible before drawing it
                    if (VisibleCheck(new Vector2Int(x, y)))
                    {
                        VM.baseMap.SetTile((Vector3Int)CordsToPos(new Vector2Int(x, y)), VM.materials[mat]);
                    }
                }
            }
        }

        //updates the lighting for the chunk
        LightMap.Update(VM.chunkID[chunkNum], lightData);
    }

    #endregion


    #region Voxel Calculations

    //converts a voxel cord to real world position
    private Vector2Int CordsToPos(Vector2Int cords)
    {
        Vector2Int pos = -cords + VM.chunkSize - new Vector2Int(1, 1);
        pos += chunkCords * VM.chunkSize;
        pos -= (VM.worldSize / 2) * VM.chunkSize;

        return pos;
    }


    //checks if a called voxel is visible and should be drawn
    private bool VisibleCheck(Vector2Int cords)
    {
        bool chunkEdge = (cords.x == 0 || cords.x == VM.chunkSize.x - 1) || (cords.y == 0 || cords.y == VM.chunkSize.y - 1);

        //if this voxel is on the edge of the chunk, draw it
        if (chunkEdge)
            return true;

        //call all this voxel's surrounding voxels
        for (int p = 0; p < 8; p++)
        {
            Vector2Int pPos = cords + VM.neighborIndex[p];

            //if voxel at pPos is air, this voxel is visible
            if (voxels[pPos.x, pPos.y] == 0) return true;
        }

        //this code is only run if the voxel is not visible );
        return false;
    }

    #endregion
}

#region Chunk Data

//data for the save files
public struct ChunkData
{
    public int[,] voxels;
}

#endregion