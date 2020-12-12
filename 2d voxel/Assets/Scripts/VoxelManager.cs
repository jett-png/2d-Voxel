using UnityEngine;
using UnityEngine.Tilemaps;

public class VoxelManager : MonoBehaviour
{
    private Chunk[,] chunks;

    public Tilemap baseMap;

    private Transform player;

    [System.NonSerialized]
    public Tile[] materials;

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

    [System.NonSerialized]
    public Vector2Int chunkSize, worldSize;
    private Vector2Int curChunk;
    private Vector2Int lastChunk;

    private bool init;

    private void Start()
    {
        WorldManager.instance.OnInitialize += Initialize;
        WorldManager.instance.Initialize();
    }

    private void Update()
    {
        if (init == false)
            return;

        ChunkManager();
    }


    //creates world from new or save files
    public void Initialize()
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
                chunks[x, y].Initialize(this, new Vector2Int(x, y), null);
            }
        }

        init = true;
    }


    //loads and unloads chunks as needed
    private void ChunkManager()
    {
        if (player == null)
        {
            player = WorldManager.instance.player;
            return;
        }
        
        curChunk = PosToChunk(player.position);

        if(lastChunk != curChunk)
        {
            baseMap.ClearAllTiles();

            if (curChunk.x < 0 || curChunk.x >= worldSize.x
            || curChunk.y < 0 || curChunk.y >= worldSize.y)
                chunks[curChunk.x, curChunk.y].DrawChunk();

            for (int p = 0; p < 8; p++)
            {
                Vector2Int pChunk = curChunk + touchingPos[p];

                if (pChunk.x < 0 || pChunk.x >= worldSize.x
                || pChunk.y < 0 || pChunk.y >= worldSize.y)
                    break;

                chunks[pChunk.x, pChunk.y].DrawChunk();
            }

            lastChunk = curChunk;
        }
        
    }


    //converts a real world position to chunk cords
    public Vector2Int PosToChunk(Vector2 pos)
    {
        pos += (worldSize / 2) * chunkSize;
        pos = new Vector2(pos.x / chunkSize.x, pos.y / chunkSize.y);
        Vector2Int cords = new Vector2Int((int)pos.x, (int)pos.y);

        return cords;
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