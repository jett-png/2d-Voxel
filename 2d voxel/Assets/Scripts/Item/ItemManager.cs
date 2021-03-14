using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private Ichunk[,] Ichunks;
    public IEchunk[] IEchunks = new IEchunk[9];
}

public struct Ichunk
{
    Vector2[] IP;
    int[] IT;
}