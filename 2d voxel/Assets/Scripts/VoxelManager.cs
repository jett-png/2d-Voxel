using UnityEngine.Tilemaps;
using UnityEngine;

public class VoxelManager : MonoBehaviour
{
    private Chunk[,] chunks;

    public Tilemap baseMap;

    [System.NonSerialized]
    public Tile[] materials;

    [System.NonSerialized]
    public Vector2Int chunkSize, worldSize;

    [System.NonSerialized]
    public Vector2Int[] touchingPos = new Vector2Int[8]
    {
        new Vector2Int(0, 1),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),
        new Vector2Int(1, 1),
        new Vector2Int(1, -1),
        new Vector2Int(-1, -1)
    };


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            Initialize(null);
    }

    public void Initialize(ChunkData[,] _chunks)
    {
        worldSize = WorldManager.instance.worldSize;
        chunkSize = WorldManager.instance.chunkSize;
        materials = WorldManager.instance.materials;
        chunks = new Chunk[worldSize.x, worldSize.y];

        //go through all the chunks
        for (int y = 0; y < worldSize.y; y++)
        {
            for (int x = 0; x < worldSize.x; x++)
            {
                chunks[x, y] = new Chunk();

                //checks for save data and creates chunk
                if (_chunks != null)
                    chunks[x, y].Initialize(this, new Vector2Int(x, y), _chunks[x, y].voxels);
                else
                    chunks[x, y].Initialize(this, new Vector2Int(x, y), null);
            }
        }

        for (int y = 0; y < worldSize.y; y++)
        {
            for (int x = 0; x < worldSize.x; x++)
            {
                chunks[x, y].DrawChunk();
            }
        }
    }


    //world generation algorithm
    public int AssignMat(Vector2Int pos)
    {
        int mat = 0;

        if (pos.y < 1)
            mat = 1;

        return mat;
    }
}