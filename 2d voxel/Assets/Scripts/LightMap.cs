using System.Collections;
using UnityEngine;

public class LightMap
{
    public Texture2D texture;

    private void UpdateTexture(Vector2Int size)
    {
        texture = new Texture2D(size.x, size.y);
    }

    /* 9 by 9 pixels in each block of the chunk
     * the pixel in the center of each block is the base
     * every pixel that isn't the base fades fades between bases
     * 
     */
}
