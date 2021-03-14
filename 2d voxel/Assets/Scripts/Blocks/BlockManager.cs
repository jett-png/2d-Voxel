using System.Collections.Generic;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockManager : MonoBehaviour
{
    #region Set Up
    public static BlockManager instance;

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


    #region Data
    private BlockChunk[,] chunks;
    private Color32[] visibleMap;
    private int[,] lightData;


    public Tilemap baseMap;
    public Transform VMObj;

    [Space]
    public Texture2D visibleMapTexture;
    public Texture2D blockRenderTexture;

    private List<Vector2Int> neighborIndex = new List<Vector2Int>(8)
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
    private List<Vector2Int> chunkID = new List<Vector2Int>(9)
    {
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(1, 2),
            new Vector2Int(2, 1),
            new Vector2Int(2, 0),
            new Vector2Int(0, 0),
            new Vector2Int(0, 2),
            new Vector2Int(2, 2),
            new Vector2Int(1, 1)
    };

    [Space]
    public List<BlockType> materials;

    [SerializeField]
    public Vector2Int worldSize, chunkSize;
    private Vector2Int curChunk, VMSize;

    private string worldName;
    #endregion


    #region Behavior

    #region Initialization
    public void Initialize()
    {
        //load the worlds information
        LoadSave();

        //create light map
        for (int c = 0; c < chunkID.Count; c++) { chunkID[c] *= chunkSize; };
        VMSize = new Vector2Int(chunkSize.x * 3, chunkSize.y * 3);
        visibleMapTexture.Resize(VMSize.x, VMSize.y);
        blockRenderTexture.Resize(VMSize.x, VMSize.y);
        visibleMap = new Color32[VMSize.x * VMSize.y];
        lightData = new int[VMSize.x, VMSize.y];

        //begin tick updates
        InvokeRepeating("Tick", 0.1f, 0.1f);
        StartCoroutine("DrawVM");
    }
    #endregion


    #region Save Data
    public void Save()
    {
        ES3.Save<BlockChunk[,]>($"{worldName} chunk cashe", chunks);
    }

    public void LoadSave()
    {
        //load chunks
        if (ES3.KeyExists($"{worldName} chunk cashe"))
        {
            ES3.LoadInto($"{worldName} chunk cashe", chunks);
            worldSize = new Vector2Int(chunks.GetLength(0), chunks.GetLength(1));
            chunkSize = new Vector2Int(chunks[0,0].blocks.GetLength(0), chunks[0, 0].blocks.GetLength(1));
        }
        else if (WorldCreation.tempChunks != null)
        {
            chunks = WorldCreation.tempChunks;
            worldSize = new Vector2Int(chunks.GetLength(0), chunks.GetLength(1));
            chunkSize = new Vector2Int(chunks[0, 0].blocks.GetLength(0), chunks[0, 0].blocks.GetLength(1));
        }
        else
        {
            Debug.LogError($"could not load {WorldManager.instance.worldName}");
            CancelInvoke();
            //display error
            //exit world
        }
    }
    #endregion


    #region Tick
    //tick thread for rendering
    private void Tick()
    {
        lightData = new int[VMSize.x, VMSize.y];
        curChunk = PosToChunk(GameRef.player.position);
        baseMap.ClearAllTiles();

        if (InWorld(curChunk.x, curChunk.y))
            DrawChunk(chunks[curChunk.x, curChunk.y], 8);

        for (int p = 0; p < 8; p++)
        {
            int px = curChunk.x + neighborIndex[p].x;
            int py = curChunk.y + neighborIndex[p].y;

            if (!InWorld(px, py))
                break;

            DrawChunk(chunks[px, py], p);
        }

        StartCoroutine("DrawVM");
    }
    #endregion


    #region Chunks

    #region Render
    //converts the chunk data into a physical chunk
    private void DrawChunk(BlockChunk c, int chunkNum)
    {
        int mat;

        for (int y = 0; y < chunkSize.y; y++)
        {
            for (int x = 0; x < chunkSize.x; x++)
            {
                mat = c.blocks[chunkSize.x - 1 - x, chunkSize.x - 1 - y];

                switch (materials[mat].type)
                {
                    case Material.transparent:
                        //checks if the block is visible before drawing it
                        if (VisibleCheck(c.x, c.y, x, y))
                            baseMap.SetTile(CordsToPos(c.x, c.y, x, y), materials[mat].tile);
                        break;
                    case Material.visible:
                        lightData[VMSize.x - x - chunkID[chunkNum].x - 1, VMSize.y - y - chunkID[chunkNum].y - 1] = 10;
                        //checks if the block is visible before drawing it
                        if (VisibleCheck(c.x, c.y, x, y))
                            baseMap.SetTile(CordsToPos(c.x, c.y, x, y), materials[mat].tile);
                        break;
                    default:
                        break;
                }
            }
        }
    }
    #endregion


    #region Visible Texture
    private Color lightLevel = Color.black;
    private bool VMGenerated;

    private void GenerateVM()
    {
        for (int y = 0; y < VMSize.y; y++)
        {
            for (int x = 0; x < VMSize.x; x++)
            {
                lightLevel.a = lightData[x, y] / 10f;

                visibleMap[y * VMSize.x + x] = lightLevel;
            }
        }

        VMGenerated = true;
    }

    private IEnumerator DrawVM()
    {
        new Thread(GenerateVM).Start();

        yield return new WaitUntil(() => VMGenerated);

        visibleMapTexture.SetPixels32(0, 0, VMSize.x, VMSize.y, visibleMap);
        visibleMapTexture.Apply();
        blockRenderTexture.SetPixels32(0, 0, VMSize.x, VMSize.y, visibleMap);
        blockRenderTexture.Apply();

        VMObj.position = ChunkToPos(curChunk.x, curChunk.y);
        VMGenerated = false;
    }
    #endregion


    #region Block Calculations
    //converts a block cord to real world position
    private Vector3Int CordsToPos(int cx, int cy, int x, int y)
    {
        int px = (cx - worldSize.x / 2) * chunkSize.x - (x - chunkSize.x + 1);
        int py = (cy - worldSize.y / 2) * chunkSize.y - (y - chunkSize.y + 1);

        return new Vector3Int(px, py, 0);
    }


    //checks if a called block is visible and should be drawn
    private bool VisibleCheck(int cx, int cy, int x, int y)
    {
        if (ChunkEdge(x, y))
            return true;

        //call all this block's surrounding blocks
        foreach (Vector2Int neighbor in neighborIndex)
        {
            int nx = x + neighbor.x;
            int ny = y + neighbor.y;

            //if block at pPos is air, this block is visible
            if (chunks[cx, cy].blocks[nx, ny] == 0) return true;
        }

        return false;
    }

    //checks if a cordinate is at the edge of it's chunk
    private bool ChunkEdge(int x, int y)
    {
        return (x == 0 || x == chunkSize.x - 1) || (y == 0 || y == chunkSize.y - 1);
    }
    #endregion

    #endregion


    #region Edit Blocks
    public int GetTile(Vector2 pos)
    {
        Vector2Int CC = PosToChunk(pos);
        Vector2Int VC = PosToBlock(pos);

        return chunks[CC.x, CC.y].blocks[VC.x, VC.y];
    }

    public int[] GetTiles(Vector2[] pos)
    {
        int[] mats = new int[pos.Length];

        for (int i = 0; i < pos.Length; i++)
        {
            Vector2Int CC = PosToChunk(pos[i]);
            Vector2Int VC = PosToBlock(pos[i]);

            mats[i] = chunks[CC.x, CC.y].blocks[VC.x, VC.y];
        }

        return mats;
    }


    public void SetTile(Vector2 pos, int mat)
    {
        Vector2Int CC = PosToChunk(pos);
        Vector2Int VC = PosToBlock(pos);

        chunks[CC.x, CC.y].blocks[VC.x, VC.y] = mat;
    }

    public void SetTiles(Vector2[,] pos, int mat)
    {
        foreach (Vector2 p in pos)
        {
            Vector2Int CC = PosToChunk(p);
            Vector2Int VC = PosToBlock(p);

            chunks[CC.x, CC.y].blocks[VC.x, VC.y] = mat;
        }
    }
    #endregion


    #region Position Calculations
    //converts a real world position to chunk cords
    public Vector2Int PosToChunk(Vector2 pos)
    {
        pos += (worldSize / 2) * chunkSize;
        pos.x /= chunkSize.x;
        pos.y /= chunkSize.y;

        return new Vector2Int((int)pos.x, (int)pos.y); ;
    }


    //converts a real world position to chunk cords
    public Vector2Int PosToBlock(Vector2 pos)
    {
        pos += (worldSize / 2) * chunkSize;
        pos.x /= chunkSize.x;
        pos.y /= chunkSize.y;
        pos.x -= (int)pos.x;
        pos.y -= (int)pos.y;
        pos *= chunkSize;

        return new Vector2Int((int)pos.x, (int)pos.y);
    }


    //converts chunk cords to a real world position
    public Vector2 ChunkToPos(int x, int y)
    {
        x -= worldSize.x / 2;
        x *= chunkSize.x;
        x += chunkSize.x / 2;

        y -= worldSize.y / 2;
        y *= chunkSize.x;
        y += chunkSize.y / 2;

        return new Vector2(x, y);
    }


    public bool InWorld(int w, int h)
    {
        return (w >= 0 && w < worldSize.x
        && h >= 0 && h < worldSize.y);
    }
    #endregion

    #endregion
}

#region Custom Types
public struct BlockChunk
{
    public int x;
    public int y;

    public int[,] blocks;
}

[System.Serializable]
public struct BlockType
{
    public string name;
    public Tile tile;
    public Material type;
}

public enum Material
{
    invisible = 0,
    transparent = 1,
    visible = 2
}
#endregion