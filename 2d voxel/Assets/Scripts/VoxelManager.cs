using UnityEngine;
using UnityEngine.Tilemaps;

public class VoxelManager : MonoBehaviour
{
    #region Instancing

    public static VoxelManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object");
            Destroy(this);
        }
    }

    #endregion

    public Transform testPos;

    private Chunk[,] chunks;

    public Tilemap baseMap;
    public Transform lightMap;

    private Transform player;

    public Texture2D lightMapTexture, voxelRenderTexture;

    [System.NonSerialized]
    public Tile[] materials;

    [System.NonSerialized]
    public Vector2Int chunkSize, worldSize;
    private Vector2Int curChunk;

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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector2[] pos = new Vector2[1];
            pos[0] = testPos.position;

            ClearTile(pos);
        }

        ChunkManager();
    }


    //creates world from new or save files
    public void Initialize()
    {
        //ref assignment
        worldSize = WorldManager.instance.worldSize;
        chunkSize = WorldManager.instance.chunkSize;
        materials = WorldManager.instance.materials;

        //create arrays
        for (int p = 0; p < 9; p++)
        {
            GameRef.chunkID[p] *= chunkSize;
        }
        LightMap.Generate(lightMapTexture, voxelRenderTexture);
        chunks = new Chunk[worldSize.x, worldSize.y];

        //init chunks
        for (int y = 0; y < worldSize.y; y++)
        {
            for (int x = 0; x < worldSize.x; x++)
            {
                chunks[x, y] = new Chunk();

                //checks for save data and creates chunk
                chunks[x, y].Initialize(this, new Vector2Int(x, y), null);
            }
        }

        InvokeRepeating("Tick", 0.25f, 0.25f);
        init = true;
    }


    #region Edit Voxels

    public void ClearTile(Vector2[] pos)
    {
        foreach(Vector2 p in pos)
        {
            Vector2Int CC = PosToChunk(p);
            Vector2Int VC = PosToVoxel(p);

            chunks[CC.x, CC.y].voxels[VC.x, VC.y] = 0;
        }
    }

    public void SetTile(Vector2[] pos, byte mat)
    {
        foreach (Vector2 p in pos)
        {
            Vector2Int CC = PosToChunk(p);
            Vector2Int VC = PosToVoxel(p);

            chunks[CC.x, CC.y].voxels[VC.x, VC.y] = mat;
        }
    }

    #endregion


    #region Chunks

    //loads and unloads chunks as needed
    private void ChunkManager()
    {
        if (player == null)
        {
            player = WorldManager.instance.player;
            return;
        }
    }


    private void Tick()
    {
        #region Chunk Updates

        curChunk = PosToChunk(player.position);
        lightMap.position = ChunkToPos(curChunk);
        baseMap.ClearAllTiles();

        if (curChunk.x >= 0 && curChunk.x < worldSize.x
        && curChunk.y >= 0 && curChunk.y < worldSize.y)
            chunks[curChunk.x, curChunk.y].DrawChunk(8);

        for (int p = 0; p < 8; p++)
        {
            Vector2Int pChunk = curChunk + GameRef.neighborIndex[p];

            if (pChunk.x < 0 || pChunk.x >= worldSize.x
            || pChunk.y < 0 || pChunk.y >= worldSize.y)
                break;

            chunks[pChunk.x, pChunk.y].DrawChunk(p);
        }

        #endregion
    }

    #endregion


    #region Position Conversions

    //converts a real world position to chunk cords
    public Vector2Int PosToChunk(Vector2 pos)
    {
        pos += (worldSize / 2) * chunkSize;
        pos = new Vector2(pos.x / chunkSize.x, pos.y / chunkSize.y);
        Vector2Int cords = new Vector2Int((int)pos.x, (int)pos.y);

        return cords;
    }


    //converts a real world position to chunk cords
    public Vector2Int PosToVoxel(Vector2 pos)
    {
        pos += (worldSize / 2) * chunkSize;
        pos = new Vector2(pos.x / chunkSize.x, pos.y / chunkSize.y);
        pos -= new Vector2((int)pos.x, (int)pos.y);
        pos *= chunkSize;

        Vector2Int cords = new Vector2Int(chunkSize.x - 1 - (int)pos.x, chunkSize.y - 1 - (int)pos.y);

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

    #endregion
    

    #region Terrain Generation

    //world generation algorithm
    public byte AssignMat(Vector2Int pos)
    {
        byte mat = 0;

        int terrainHeight = Mathf.FloorToInt(6 * GetPerlin(new Vector2(pos.x, 0), 0, 1f)) - 3;

        if (pos.y <= terrainHeight)
            mat = 1;

        return mat;
    }


    //perlin noise generator
    public float GetPerlin(Vector2 position, float offset, float scale)
    {
        return Mathf.PerlinNoise((position.x + 0.1f) / chunkSize.x * scale + offset, (position.y + 0.1f) / chunkSize.x * scale + offset);
    }

    #endregion
}