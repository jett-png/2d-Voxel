using UnityEngine;

public static class GameRef
{
    public static Vector2Int[] neighborIndex = new Vector2Int[8]
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
    public static Vector2Int[] chunkID = new Vector2Int[9]
    {
            new Vector2Int(1, 2),
            new Vector2Int(2, 1),
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, 2),
            new Vector2Int(2, 2),
            new Vector2Int(2, 0),
            new Vector2Int(0, 0),
            new Vector2Int(1, 1)
    };

    public static Vector2 cursorPos;
}
