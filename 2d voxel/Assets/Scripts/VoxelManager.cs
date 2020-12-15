using UnityEngine;
using UnityEngine.Tilemaps;

public class VoxelManager : MonoBehaviour
{
    private Chunk[,] chunks;

    public Tilemap baseMap;
    public Transform lightMap;

    private Transform player;

    public Texture2D lightMapTexture;
    public Texture2D VoxelRenderTexture;

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
    public Vector2Int[] chunkID = new Vector2Int[9]
    {
        new Vector2Int(30, 60),
        new Vector2Int(60, 30),
        new Vector2Int(30, 0),
        new Vector2Int(0, 30),
        new Vector2Int(0, 60),
        new Vector2Int(60, 60),
        new Vector2Int(60, 0),
        new Vector2Int(0, 0),
        new Vector2Int(30, 30)
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

        LightMap.Generate(lightMapTexture, VoxelRenderTexture);

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
            lightMap.position = ChunkToPos(curChunk);

            if (curChunk.x >= 0 && curChunk.x < worldSize.x
            && curChunk.y >= 0 && curChunk.y < worldSize.y)
                chunks[curChunk.x, curChunk.y].DrawChunk(8);

            for (int p = 0; p < 8; p++)
            {
                Vector2Int pChunk = curChunk + touchingPos[p];

                if (pChunk.x < 0 || pChunk.x >= worldSize.x
                || pChunk.y < 0 || pChunk.y >= worldSize.y)
                    break;

                chunks[pChunk.x, pChunk.y].DrawChunk(p);
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


    //converts chunk cords to a real world position
    public Vector2 ChunkToPos(Vector2Int CC)
    {
        CC -= worldSize / 2;
        CC *= chunkSize;
        CC += chunkSize / 2;

        return CC;
    }


    //world generation algorithm
    public int AssignMat(Vector2Int pos)
    {
        int mat = 0;

        if (pos.y < Random.Range(-5,0))
            mat = 1;

        return mat;
    }
}